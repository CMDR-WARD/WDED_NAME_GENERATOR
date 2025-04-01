using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;


namespace WDED_NAME_GENERATOR
{
    public partial class Form2 : Form
    {
        private const int WH_MOUSE_LL = 14; 
        private const int WM_MOUSEMOVE = 0x0200; 
        private const int WM_LBUTTONDOWN = 0x0201; 
        private const int WM_RBUTTONDOWN = 0x0202; 

        private IntPtr _hookID = IntPtr.Zero; 
        private LowLevelMouseProc _proc;
        public Color SelectedColor { get; private set; } 

        public Form2()
        {
            InitializeComponent();



            label1.Left = 60;
            label2.Left = 60;
            label3.Left = 60;
            label1.Top = 1;
            label2.Top = 16;
            label3.Top = 31;
            label1.Text = "Coordinate：";
            this.FormBorderStyle = FormBorderStyle.None;
            this.Size = new Size(150, 50);
            this.TopMost = true;
            this.ShowInTaskbar = false;

            var colorButton = new Button
            {
                Size = new Size(48, 48),
                Location = new Point(1, 1),
                BackColor = Color.White,
                FlatStyle = FlatStyle.Popup
            };
            this.Controls.Add(colorButton);

            _proc = HookFunc;
            _hookID = SetHook(_proc);
        }
        private IntPtr SetHook(LowLevelMouseProc proc)
        {
            using (var curProcess = System.Diagnostics.Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private IntPtr HookFunc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                if (wParam == (IntPtr)WM_MOUSEMOVE)
                {
                    var mousePoint = Cursor.Position;
                    UpdateWindowPosition(mousePoint.X, mousePoint.Y);

                    using (var bitmap = new Bitmap(1, 1))
                    using (var graphics = Graphics.FromImage(bitmap))
                    {
                        graphics.CopyFromScreen(mousePoint.X, mousePoint.Y, 0, 0, new Size(1, 1));
                        SelectedColor = bitmap.GetPixel(0, 0);
                        this.Controls[3].BackColor = SelectedColor;
                    }
                }
                else if (wParam == (IntPtr)WM_LBUTTONDOWN)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else if (wParam == (IntPtr)WM_RBUTTONDOWN)
                {                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }
        private void UpdateWindowPosition(int x, int y)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateWindowPosition(x, y)));
            }
            else
            {
                Screen screen = Screen.PrimaryScreen;
                Rectangle workingArea = screen.WorkingArea;

                if (x + this.Width > workingArea.Width)
                    this.Left = x - 10 - this.Width;
                else
                    this.Left = x + 10;

                if (y + this.Height > workingArea.Height)
                    this.Top = y - 10 - this.Height;
                else
                    this.Top = y + 10;

                label2.Text = $"X: {x}";
                label3.Text = $"Y: {y}";
            }
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            UnhookWindowsHookEx(_hookID);
            base.OnFormClosed(e);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        private BG BGF;
        private void Form2_Load(object sender, EventArgs e)
        {
            BGF = new BG();
            BGF.Show();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (BGF != null && !BGF.IsDisposed)
            {
                BGF.Close();
            }
        }

        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

    }
}
