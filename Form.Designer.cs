namespace Datamanager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            panel1 = new Panel();
            button2 = new Button();
            button1 = new Button();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            button10 = new Button();
            button9 = new Button();
            pictureBox2 = new PictureBox();
            button4 = new Button();
            button3 = new Button();
            trackBar1 = new TrackBar();
            pictureBox1 = new PictureBox();
            tabPage2 = new TabPage();
            panel2 = new Panel();
            button6 = new Button();
            button8 = new Button();
            button7 = new Button();
            button5 = new Button();
            comboBox1 = new ComboBox();
            listBox1 = new ListBox();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tabPage2.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(1, -4);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(802, 455);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.FromArgb(64, 64, 64);
            tabPage1.Controls.Add(panel1);
            tabPage1.Controls.Add(button10);
            tabPage1.Controls.Add(button9);
            tabPage1.Controls.Add(pictureBox2);
            tabPage1.Controls.Add(button4);
            tabPage1.Controls.Add(button3);
            tabPage1.Controls.Add(trackBar1);
            tabPage1.Controls.Add(pictureBox1);
            tabPage1.Font = new Font("맑은 고딕", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 129);
            tabPage1.ForeColor = SystemColors.ControlText;
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(794, 422);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Data Manager";
            tabPage1.Click += tabPage1_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(textBox2);
            panel1.Location = new Point(542, 93);
            panel1.Name = "panel1";
            panel1.Size = new Size(250, 202);
            panel1.TabIndex = 11;
            // 
            // button2
            // 
            button2.BackColor = Color.Gray;
            button2.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            button2.Location = new Point(3, 3);
            button2.Name = "button2";
            button2.Size = new Size(133, 51);
            button2.TabIndex = 3;
            button2.Text = "프레임 삭제";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.Gray;
            button1.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            button1.Location = new Point(3, 55);
            button1.Name = "button1";
            button1.Size = new Size(133, 50);
            button1.TabIndex = 2;
            button1.Text = "폴더 열기";
            button1.UseVisualStyleBackColor = false;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.Gray;
            textBox1.Font = new Font("맑은 고딕", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 129);
            textBox1.Location = new Point(3, 111);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(69, 38);
            textBox1.TabIndex = 6;
            textBox1.Text = " 앵글";
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.Gray;
            textBox2.Font = new Font("맑은 고딕", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 129);
            textBox2.Location = new Point(3, 155);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(69, 38);
            textBox2.TabIndex = 7;
            textBox2.Text = " 속도";
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // button10
            // 
            button10.BackColor = Color.FromArgb(64, 64, 64);
            button10.BackgroundImage = (Image)resources.GetObject("button10.BackgroundImage");
            button10.BackgroundImageLayout = ImageLayout.Stretch;
            button10.Location = new Point(83, 353);
            button10.Name = "button10";
            button10.Size = new Size(60, 60);
            button10.TabIndex = 10;
            button10.UseVisualStyleBackColor = false;
            // 
            // button9
            // 
            button9.BackColor = Color.FromArgb(64, 64, 64);
            button9.BackgroundImage = (Image)resources.GetObject("button9.BackgroundImage");
            button9.BackgroundImageLayout = ImageLayout.Stretch;
            button9.Location = new Point(24, 353);
            button9.Name = "button9";
            button9.Size = new Size(63, 60);
            button9.TabIndex = 9;
            button9.UseVisualStyleBackColor = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(344, 236);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(192, 115);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 8;
            pictureBox2.TabStop = false;
            // 
            // button4
            // 
            button4.BackColor = Color.Gray;
            button4.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            button4.Location = new Point(676, 353);
            button4.Name = "button4";
            button4.Size = new Size(115, 60);
            button4.TabIndex = 5;
            button4.Text = "프레임 ▶";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.Gray;
            button3.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            button3.Location = new Point(542, 353);
            button3.Name = "button3";
            button3.Size = new Size(115, 60);
            button3.TabIndex = 4;
            button3.Text = "프레임 ◀";
            button3.UseVisualStyleBackColor = false;
            // 
            // trackBar1
            // 
            trackBar1.BackColor = Color.Gray;
            trackBar1.Location = new Point(149, 357);
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(387, 56);
            trackBar1.TabIndex = 1;
            trackBar1.Scroll += trackBar1_Scroll;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.White;
            pictureBox1.Location = new Point(24, 13);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(512, 338);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // tabPage2
            // 
            tabPage2.BackColor = Color.FromArgb(64, 64, 64);
            tabPage2.Controls.Add(panel2);
            tabPage2.Controls.Add(button5);
            tabPage2.Controls.Add(comboBox1);
            tabPage2.Controls.Add(listBox1);
            tabPage2.Location = new Point(4, 29);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(794, 422);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "training";
            // 
            // panel2
            // 
            panel2.Controls.Add(button6);
            panel2.Controls.Add(button8);
            panel2.Controls.Add(button7);
            panel2.Location = new Point(607, 60);
            panel2.Name = "panel2";
            panel2.Size = new Size(191, 201);
            panel2.TabIndex = 8;
            // 
            // button6
            // 
            button6.BackColor = Color.Gray;
            button6.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            button6.Location = new Point(3, 44);
            button6.Name = "button6";
            button6.Size = new Size(112, 46);
            button6.TabIndex = 5;
            button6.Text = "학습중단";
            button6.UseVisualStyleBackColor = false;
            // 
            // button8
            // 
            button8.BackColor = Color.Gray;
            button8.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            button8.Location = new Point(0, 151);
            button8.Name = "button8";
            button8.Size = new Size(181, 46);
            button8.TabIndex = 7;
            button8.Text = "프레임 화질 조정";
            button8.UseVisualStyleBackColor = false;
            // 
            // button7
            // 
            button7.BackColor = Color.Gray;
            button7.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            button7.Location = new Point(-1, 99);
            button7.Name = "button7";
            button7.Size = new Size(116, 46);
            button7.TabIndex = 6;
            button7.Text = "화질 조절";
            button7.UseVisualStyleBackColor = false;
            button7.Click += button7_Click;
            // 
            // button5
            // 
            button5.BackColor = Color.Gray;
            button5.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            button5.Location = new Point(185, 15);
            button5.Name = "button5";
            button5.Size = new Size(116, 39);
            button5.TabIndex = 3;
            button5.Text = "train";
            button5.UseVisualStyleBackColor = false;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(17, 16);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(151, 28);
            comboBox1.TabIndex = 2;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(17, 60);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(284, 324);
            listBox1.TabIndex = 1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(801, 447);
            Controls.Add(tabControl1);
            Name = "Form1";
            Text = "Form1";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tabPage2.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Button button4;
        private Button button3;
        private Button button2;
        private Button button1;
        private TrackBar trackBar1;
        private PictureBox pictureBox1;
        private TextBox textBox2;
        private TextBox textBox1;
        private Button button5;
        private ComboBox comboBox1;
        private ListBox listBox1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private Button button8;
        private Button button7;
        private Button button6;
        private PictureBox pictureBox2;
        private Button button9;
        private Button button10;
        private Panel panel1;
        private Panel panel2;
    }
}
