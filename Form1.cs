using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Tesseract;

namespace WDED_NAME_GENERATOR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "ENTERPRISE";
            timer1.Interval = 400;
        }

        Rectangle selectedArea;
        Color selectedColor;
        int centerX;
        int centerY;

        private void button1_Click(object sender, EventArgs e)
        {
            using (Form overlayForm = new Form())
            {
                overlayForm.FormBorderStyle = FormBorderStyle.None;
                overlayForm.WindowState = FormWindowState.Maximized;
                overlayForm.BackColor = Color.Black;
                overlayForm.Opacity = 0.5;
                overlayForm.TopMost = true;
                Rectangle tempArea = Rectangle.Empty;
                overlayForm.MouseDown += (s, ev) =>
                {
                    tempArea = new Rectangle(ev.Location, new Size(0, 0));
                };
                overlayForm.MouseMove += (s, ev) =>
                {
                    if (ev.Button == MouseButtons.Left)
                    {
                        tempArea = new Rectangle(
                            Math.Min(tempArea.X, ev.X),
                            Math.Min(tempArea.Y, ev.Y),
                            Math.Abs(ev.X - tempArea.X),
                            Math.Abs(ev.Y - tempArea.Y)
                        );
                        overlayForm.Invalidate();
                    }
                };
                overlayForm.MouseUp += (s, ev) =>
                {
                    selectedArea = tempArea;
                    overlayForm.Close();
                };
                overlayForm.Paint += (s, ev) =>
                {
                    using (Pen pen = new Pen(Color.Red, 2))
                    {
                        ev.Graphics.DrawRectangle(pen, tempArea);
                    }
                };
                overlayForm.ShowDialog();
            }
            MessageBox.Show($"Area selected£º{selectedArea}", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            label1.Text = $"{selectedArea}";
            centerX = selectedArea.X + selectedArea.Width;
            centerY = selectedArea.Y + selectedArea.Height / 2;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (var pickColorForm = new Form2())
            {
                if (pickColorForm.ShowDialog() == DialogResult.OK)
                {
                    selectedColor = pickColorForm.SelectedColor;
                    button3.BackColor = selectedColor;
                    this.TopMost = true;
                    MessageBox.Show($"Color selected: {selectedColor}", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    label2.Text = $"{selectedColor}";
                    this.TopMost = false;
                }
            }
            this.Show();
        }

        public enum MouseEventFlags : uint
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010
        }

        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        private static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll", EntryPoint = "mouse_event")]
        private static extern void MouseEvent(MouseEventFlags dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        public static void MouseLeftClick(int xpos, int ypos)
        {
            SetCursorPos(xpos, ypos);
            MouseEvent(MouseEventFlags.LEFTDOWN, (uint)xpos, (uint)ypos, 0, UIntPtr.Zero);
            Thread.Sleep(20);
            MouseEvent(MouseEventFlags.LEFTUP, (uint)xpos, (uint)ypos, 0, UIntPtr.Zero);
            Thread.Sleep(5);
        }

        bool isMonitoring = false;

        private Bitmap FilterColor(Bitmap source, Color targetColor)
        {
            Bitmap resultBitmap = new Bitmap(source.Width, source.Height);
            BitmapData sourceData = source.LockBits(
                new Rectangle(0, 0, source.Width, source.Height),
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);

            BitmapData resultData = resultBitmap.LockBits(
                new Rectangle(0, 0, resultBitmap.Width, resultBitmap.Height),
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppArgb);

            try
            {
                int byteCount = sourceData.Stride * source.Height;
                byte[] sourcePixels = new byte[byteCount];
                Marshal.Copy(sourceData.Scan0, sourcePixels, 0, byteCount);

                byte[] resultPixels = new byte[byteCount];

                for (int y = 0; y < source.Height; y++)
                {
                    for (int x = 0; x < source.Width; x++)
                    {
                        int index = y * sourceData.Stride + x * 4;
                        byte b = sourcePixels[index];
                        byte g = sourcePixels[index + 1];
                        byte r = sourcePixels[index + 2];

                        if (Math.Abs(r - targetColor.R) < 80 &&
                            Math.Abs(g - targetColor.G) < 80 &&
                            Math.Abs(b - targetColor.B) < 80)
                        {
                            Array.Clear(resultPixels, index, 4);
                            resultPixels[index + 3] = 255;
                        }
                        else
                        {
                            Array.Fill(resultPixels, (byte)255, index, 4);
                        }
                    }
                }
                Marshal.Copy(resultPixels, 0, resultData.Scan0, byteCount);
            }
            finally
            {
                source.UnlockBits(sourceData);
                resultBitmap.UnlockBits(resultData);
            }
            return resultBitmap;
        }

        private Task<string> PerformOCRAsync(Rectangle area)
        {
            return Task.Run(() =>
            {
                try
                {
                    using (Bitmap bitmap = new Bitmap(area.Width, area.Height))
                    {
                        using (Graphics g = Graphics.FromImage(bitmap))
                        {
                            g.CopyFromScreen(area.Location, Point.Empty, area.Size);
                        }
                        using (Bitmap FilteredBitmap = FilterColor(bitmap, selectedColor))
                        {
                            pictureBox1.Image = FilteredBitmap;
                            using (var engine = new TesseractEngine("tessdata", "eng", EngineMode.Default))
                            {
                                using (var img = PixConverter.ToPix(FilteredBitmap))
                                {
                                    using (var page = engine.Process(img))
                                    {

                                        return page.GetText();
                                    }
                                }
                            }
                        }
                    }
                }
                catch
                {
                    return string.Empty;
                }
            });
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (selectedArea.IsEmpty || selectedColor.IsEmpty)
            {
                MessageBox.Show("You have to select something first");
                return;
            }

            using (Bitmap bitmap = new Bitmap(selectedArea.Width, selectedArea.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(selectedArea.Location, Point.Empty, selectedArea.Size);
                }
                pictureBox1.Image = FilterColor(bitmap, selectedColor);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            isMonitoring = !isMonitoring;
            button4.Text = isMonitoring ? "STOP" : "START";
            timer1.Enabled = isMonitoring;
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            if (selectedArea.IsEmpty || selectedColor.IsEmpty)
            {
                timer1.Stop();
                isMonitoring = false;
                button4.Text = "START";
                return;
            }

            string result = await Task.Run(() => PerformOCRAsync(selectedArea));
            string target = textBox1.Text.Trim().ToUpper();

            if (result.Contains(target))
            {
                timer1.Stop();
                isMonitoring = false;
                button4.Text = "START";
                SetForegroundWindow(this.Handle);
                MessageBox.Show($"KEYWORD DETECTED: {target}");
            }
            else
            {
                MouseLeftClick(centerX, centerY);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            label1.Text = "";
            label2.Text = "";
            if (!RegisterHotKey(this.Handle, HOTKEY_ID, MOD_NONE, VK_TAB))
            {
                MessageBox.Show("Failed to register hotkey (TAB)");
            }
        }

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private const int HOTKEY_ID = 1;
        private const uint MOD_NONE = 0x0000;
        private const uint VK_TAB = 0x09;


        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;

            if (m.Msg == WM_HOTKEY)
            {
                if (m.WParam.ToInt32() == HOTKEY_ID)
                {
                    if (isMonitoring)
                    {
                        button4.PerformClick();
                        SetForegroundWindow(this.Handle);
                        MessageBox.Show("The process has been stopped");
                    }
                    
                }
            }
            base.WndProc(ref m);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.Text = "ENTERPRISE";
                MessageBox.Show("You have to type something", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
