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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            picEdge = new PictureBox();
            panel1 = new Panel();
            listImages = new ListBox();
            btnPlay = new Button();
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
            picImage = new PictureBox();
            tabPage2 = new TabPage();
            panel2 = new Panel();
            button6 = new Button();
            button8 = new Button();
            button7 = new Button();
            chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            button5 = new Button();
            comboBox1 = new ComboBox();
            listBox1 = new ListBox();
            timer1 = new System.Windows.Forms.Timer(components);
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picEdge).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picImage).BeginInit();
            tabPage2.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chart1).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(1, -3);
            tabControl1.Margin = new Padding(2);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1089, 584);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.FromArgb(64, 64, 64);
            tabPage1.Controls.Add(picEdge);
            tabPage1.Controls.Add(panel1);
            tabPage1.Controls.Add(button10);
            tabPage1.Controls.Add(button9);
            tabPage1.Controls.Add(textBox1);
            tabPage1.Controls.Add(pictureBox2);
            tabPage1.Controls.Add(textBox2);
            tabPage1.Controls.Add(button4);
            tabPage1.Controls.Add(button3);
            tabPage1.Controls.Add(trackBar1);
            tabPage1.Controls.Add(picImage);
            tabPage1.Font = new Font("맑은 고딕", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 129);
            tabPage1.ForeColor = SystemColors.ControlText;
            tabPage1.Location = new Point(4, 24);
            tabPage1.Margin = new Padding(2);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(2);
            tabPage1.Size = new Size(1081, 556);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Data Manager";
            // 
            // picEdge
            // 
            picEdge.BackColor = Color.White;
            picEdge.Location = new Point(317, 20);
            picEdge.Margin = new Padding(2);
            picEdge.Name = "picEdge";
            picEdge.Size = new Size(268, 389);
            picEdge.SizeMode = PictureBoxSizeMode.Zoom;
            picEdge.TabIndex = 12;
            picEdge.TabStop = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(listImages);
            panel1.Controls.Add(btnPlay);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button1);
            panel1.Location = new Point(680, 231);
            panel1.Margin = new Padding(2);
            panel1.Name = "panel1";
            panel1.Size = new Size(362, 273);
            panel1.TabIndex = 11;
            // 
            // listImages
            // 
            listImages.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point, 129);
            listImages.FormattingEnabled = true;
            listImages.Location = new Point(3, 3);
            listImages.Name = "listImages";
            listImages.Size = new Size(356, 199);
            listImages.TabIndex = 14;
            listImages.SelectedIndexChanged += listImages_SelectedIndexChanged;
            // 
            // btnPlay
            // 
            btnPlay.BackColor = Color.Gray;
            btnPlay.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnPlay.Location = new Point(261, 214);
            btnPlay.Margin = new Padding(2);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(89, 45);
            btnPlay.TabIndex = 13;
            btnPlay.Text = "재생";
            btnPlay.UseVisualStyleBackColor = false;
            btnPlay.Click += btnPlay_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.Gray;
            button2.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            button2.Location = new Point(22, 214);
            button2.Margin = new Padding(2);
            button2.Name = "button2";
            button2.Size = new Size(125, 45);
            button2.TabIndex = 3;
            button2.Text = "프레임 삭제";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.Gray;
            button1.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            button1.Location = new Point(151, 214);
            button1.Margin = new Padding(2);
            button1.Name = "button1";
            button1.Size = new Size(106, 45);
            button1.TabIndex = 2;
            button1.Text = "폴더 열기";
            button1.UseVisualStyleBackColor = false;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.Gray;
            textBox1.Font = new Font("맑은 고딕", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 129);
            textBox1.Location = new Point(605, 129);
            textBox1.Margin = new Padding(2);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(55, 32);
            textBox1.TabIndex = 6;
            textBox1.Text = " 앵글";
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.Gray;
            textBox2.Font = new Font("맑은 고딕", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 129);
            textBox2.Location = new Point(605, 47);
            textBox2.Margin = new Padding(2);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(55, 32);
            textBox2.TabIndex = 7;
            textBox2.Text = " 속도";
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // button10
            // 
            button10.BackColor = Color.FromArgb(64, 64, 64);
            button10.BackgroundImage = (Image)resources.GetObject("button10.BackgroundImage");
            button10.BackgroundImageLayout = ImageLayout.Stretch;
            button10.Location = new Point(72, 413);
            button10.Margin = new Padding(2);
            button10.Name = "button10";
            button10.Size = new Size(53, 45);
            button10.TabIndex = 10;
            button10.UseVisualStyleBackColor = false;
            // 
            // button9
            // 
            button9.BackColor = Color.FromArgb(64, 64, 64);
            button9.BackgroundImage = (Image)resources.GetObject("button9.BackgroundImage");
            button9.BackgroundImageLayout = ImageLayout.Stretch;
            button9.Location = new Point(19, 413);
            button9.Margin = new Padding(2);
            button9.Name = "button9";
            button9.Size = new Size(49, 45);
            button9.TabIndex = 9;
            button9.UseVisualStyleBackColor = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.bf656515_bae2_4248_aa12_ec77ba243498__1_;
            pictureBox2.Location = new Point(680, 20);
            pictureBox2.Margin = new Padding(2);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(362, 196);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 8;
            pictureBox2.TabStop = false;
            // 
            // button4
            // 
            button4.BackColor = Color.Gray;
            button4.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            button4.Location = new Point(252, 473);
            button4.Margin = new Padding(2);
            button4.Name = "button4";
            button4.Size = new Size(89, 45);
            button4.TabIndex = 5;
            button4.Text = "프레임 ▶";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.Gray;
            button3.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            button3.Location = new Point(148, 473);
            button3.Margin = new Padding(2);
            button3.Name = "button3";
            button3.Size = new Size(89, 45);
            button3.TabIndex = 4;
            button3.Text = "프레임 ◀";
            button3.UseVisualStyleBackColor = false;
            // 
            // trackBar1
            // 
            trackBar1.BackColor = Color.Gray;
            trackBar1.Location = new Point(129, 413);
            trackBar1.Margin = new Padding(2);
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(456, 45);
            trackBar1.TabIndex = 1;
            trackBar1.Scroll += trackBar1_Scroll;
            // 
            // picImage
            // 
            picImage.BackColor = Color.White;
            picImage.Location = new Point(19, 20);
            picImage.Margin = new Padding(2);
            picImage.Name = "picImage";
            picImage.Size = new Size(280, 389);
            picImage.SizeMode = PictureBoxSizeMode.Zoom;
            picImage.TabIndex = 0;
            picImage.TabStop = false;
            picImage.Click += pictureBox1_Click;
            // 
            // tabPage2
            // 
            tabPage2.BackColor = Color.FromArgb(64, 64, 64);
            tabPage2.Controls.Add(panel2);
            tabPage2.Controls.Add(chart1);
            tabPage2.Controls.Add(button5);
            tabPage2.Controls.Add(comboBox1);
            tabPage2.Controls.Add(listBox1);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Margin = new Padding(2);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(2);
            tabPage2.Size = new Size(1081, 556);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "training";
            // 
            // panel2
            // 
            panel2.Controls.Add(button6);
            panel2.Controls.Add(button8);
            panel2.Controls.Add(button7);
            panel2.Location = new Point(784, 45);
            panel2.Margin = new Padding(2);
            panel2.Name = "panel2";
            panel2.Size = new Size(276, 244);
            panel2.TabIndex = 8;
            // 
            // button6
            // 
            button6.BackColor = Color.Gray;
            button6.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            button6.Location = new Point(18, 33);
            button6.Margin = new Padding(2);
            button6.Name = "button6";
            button6.Size = new Size(87, 34);
            button6.TabIndex = 5;
            button6.Text = "학습중단";
            button6.UseVisualStyleBackColor = false;
            // 
            // button8
            // 
            button8.BackColor = Color.Gray;
            button8.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            button8.Location = new Point(16, 113);
            button8.Margin = new Padding(2);
            button8.Name = "button8";
            button8.Size = new Size(157, 34);
            button8.TabIndex = 7;
            button8.Text = "프레임 화질 조정";
            button8.UseVisualStyleBackColor = false;
            // 
            // button7
            // 
            button7.BackColor = Color.Gray;
            button7.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            button7.Location = new Point(15, 74);
            button7.Margin = new Padding(2);
            button7.Name = "button7";
            button7.Size = new Size(90, 34);
            button7.TabIndex = 6;
            button7.Text = "화질 조절";
            button7.UseVisualStyleBackColor = false;
            button7.Click += button7_Click;
            // 
            // chart1
            // 
            chartArea2.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            chart1.Legends.Add(legend2);
            chart1.Location = new Point(437, 46);
            chart1.Margin = new Padding(2);
            chart1.Name = "chart1";
            chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Excel;
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            chart1.Series.Add(series2);
            chart1.Size = new Size(218, 243);
            chart1.TabIndex = 4;
            chart1.Text = "chart1";
            chart1.Click += chart1_Click;
            // 
            // button5
            // 
            button5.BackColor = Color.Gray;
            button5.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            button5.Location = new Point(239, 12);
            button5.Margin = new Padding(2);
            button5.Name = "button5";
            button5.Size = new Size(90, 29);
            button5.TabIndex = 3;
            button5.Text = "train";
            button5.UseVisualStyleBackColor = false;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(13, 12);
            comboBox1.Margin = new Padding(2);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(118, 23);
            comboBox1.TabIndex = 2;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(13, 45);
            listBox1.Margin = new Padding(2);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(363, 469);
            listBox1.TabIndex = 1;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1092, 592);
            Controls.Add(tabControl1);
            Margin = new Padding(2);
            Name = "Form1";
            Text = "Form1";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picEdge).EndInit();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ((System.ComponentModel.ISupportInitialize)picImage).EndInit();
            tabPage2.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chart1).EndInit();
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
        private PictureBox picImage;
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
        private PictureBox picEdge;
        private Button btnPlay;
        private System.Windows.Forms.Timer timer1;
        private ListBox listImages;
    }
}
