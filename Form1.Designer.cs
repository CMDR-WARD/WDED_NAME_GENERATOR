namespace WDED_NAME_GENERATOR
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            button1 = new Button();
            label1 = new Label();
            button2 = new Button();
            button3 = new Button();
            label2 = new Label();
            textBox1 = new TextBox();
            button4 = new Button();
            label3 = new Label();
            pictureBox1 = new PictureBox();
            button5 = new Button();
            timer1 = new System.Windows.Forms.Timer(components);
            label5 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.DimGray;
            button1.FlatStyle = FlatStyle.Popup;
            button1.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            button1.ForeColor = Color.OrangeRed;
            button1.Location = new Point(12, 12);
            button1.Name = "button1";
            button1.Size = new Size(271, 45);
            button1.TabIndex = 0;
            button1.Text = "Select OCR recognition area";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 134);
            label1.ForeColor = Color.OrangeRed;
            label1.Location = new Point(12, 60);
            label1.Name = "label1";
            label1.Size = new Size(90, 17);
            label1.TabIndex = 1;
            label1.Text = "Area selected";
            // 
            // button2
            // 
            button2.BackColor = Color.DimGray;
            button2.FlatStyle = FlatStyle.Popup;
            button2.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            button2.ForeColor = Color.OrangeRed;
            button2.Location = new Point(12, 80);
            button2.Name = "button2";
            button2.Size = new Size(225, 45);
            button2.TabIndex = 2;
            button2.Text = "Select font color";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.DimGray;
            button3.FlatStyle = FlatStyle.Popup;
            button3.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            button3.ForeColor = Color.OrangeRed;
            button3.Location = new Point(236, 80);
            button3.Name = "button3";
            button3.Size = new Size(47, 45);
            button3.TabIndex = 3;
            button3.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            label2.ForeColor = Color.OrangeRed;
            label2.Location = new Point(12, 128);
            label2.Name = "label2";
            label2.Size = new Size(95, 17);
            label2.TabIndex = 4;
            label2.Text = "Color selected";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 283);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(271, 23);
            textBox1.TabIndex = 5;
            textBox1.Text = "ENTERPRISE";
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // button4
            // 
            button4.BackColor = Color.DimGray;
            button4.FlatStyle = FlatStyle.Popup;
            button4.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            button4.ForeColor = Color.OrangeRed;
            button4.Location = new Point(12, 312);
            button4.Name = "button4";
            button4.Size = new Size(271, 73);
            button4.TabIndex = 6;
            button4.Text = "START";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            label3.ForeColor = Color.OrangeRed;
            label3.Location = new Point(12, 263);
            label3.Name = "label3";
            label3.Size = new Size(75, 17);
            label3.TabIndex = 7;
            label3.Text = "KEYWORD:";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.FromArgb(50, 50, 50);
            pictureBox1.Location = new Point(12, 148);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(271, 112);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 8;
            pictureBox1.TabStop = false;
            // 
            // button5
            // 
            button5.BackColor = Color.DimGray;
            button5.FlatStyle = FlatStyle.Popup;
            button5.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            button5.ForeColor = Color.OrangeRed;
            button5.Location = new Point(183, 225);
            button5.Name = "button5";
            button5.Size = new Size(100, 35);
            button5.TabIndex = 10;
            button5.Text = "Preview";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
            label5.ForeColor = Color.OrangeRed;
            label5.Location = new Point(12, 388);
            label5.Name = "label5";
            label5.Size = new Size(190, 17);
            label5.TabIndex = 11;
            label5.Text = "Press TAB to Stop in any case";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(295, 414);
            Controls.Add(label5);
            Controls.Add(button5);
            Controls.Add(pictureBox1);
            Controls.Add(label3);
            Controls.Add(button4);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(label1);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "WDED_NAME_GENERATOR";
            TopMost = true;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Label label1;
        private Button button2;
        private Button button3;
        private Label label2;
        private TextBox textBox1;
        private Button button4;
        private Label label3;
        private PictureBox pictureBox1;
        private Button button5;
        private System.Windows.Forms.Timer timer1;
        private Label label5;
    }
}
