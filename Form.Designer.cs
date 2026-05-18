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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            pictureBox2 = new PictureBox();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            button4 = new Button();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            trackBar1 = new TrackBar();
            pictureBox1 = new PictureBox();
            tabPage2 = new TabPage();
            button8 = new Button();
            button7 = new Button();
            button6 = new Button();
            chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            button5 = new Button();
            comboBox1 = new ComboBox();
            listBox1 = new ListBox();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chart1).BeginInit();
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
            tabPage1.Controls.Add(pictureBox2);
            tabPage1.Controls.Add(textBox1);
            tabPage1.Controls.Add(textBox2);
            tabPage1.Controls.Add(button4);
            tabPage1.Controls.Add(button3);
            tabPage1.Controls.Add(button2);
            tabPage1.Controls.Add(button1);
            tabPage1.Controls.Add(trackBar1);
            tabPage1.Controls.Add(pictureBox1);
            tabPage1.ForeColor = SystemColors.ControlText;
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(794, 422);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Data Manager";
            tabPage1.Click += tabPage1_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = TeamApp.Properties.Resources.bf656515_bae2_4248_aa12_ec77ba243498__1_;
            pictureBox2.Location = new Point(356, 192);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(180, 110);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 8;
            pictureBox2.TabStop = false;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.Gray;
            textBox1.Font = new Font("맑은 고딕", 18F, FontStyle.Bold, GraphicsUnit.Point, 129);
            textBox1.Location = new Point(24, 366);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(85, 47);
            textBox1.TabIndex = 6;
            textBox1.Text = " 앵글";
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.Gray;
            textBox2.Font = new Font("맑은 고딕", 18F, FontStyle.Bold, GraphicsUnit.Point, 129);
            textBox2.Location = new Point(255, 369);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(88, 47);
            textBox2.TabIndex = 7;
            textBox2.Text = " 속도";
            // 
            // button4
            // 
            button4.BackColor = Color.Gray;
            button4.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            button4.Location = new Point(676, 304);
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
            button3.Location = new Point(542, 304);
            button3.Name = "button3";
            button3.Size = new Size(115, 60);
            button3.TabIndex = 4;
            button3.Text = "프레임 ◀";
            button3.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            button2.BackColor = Color.Gray;
            button2.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            button2.Location = new Point(542, 148);
            button2.Name = "button2";
            button2.Size = new Size(158, 49);
            button2.TabIndex = 3;
            button2.Text = "프레임 삭제";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.Gray;
            button1.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            button1.Location = new Point(542, 232);
            button1.Name = "button1";
            button1.Size = new Size(158, 50);
            button1.TabIndex = 2;
            button1.Text = "폴더 열기";
            button1.UseVisualStyleBackColor = false;
            // 
            // trackBar1
            // 
            trackBar1.BackColor = Color.Gray;
            trackBar1.Location = new Point(24, 308);
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(512, 56);
            trackBar1.TabIndex = 1;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.White;
            pictureBox1.Location = new Point(24, 13);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(512, 289);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // tabPage2
            // 
            tabPage2.BackColor = Color.FromArgb(64, 64, 64);
            tabPage2.Controls.Add(button8);
            tabPage2.Controls.Add(button7);
            tabPage2.Controls.Add(button6);
            tabPage2.Controls.Add(chart1);
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
            // button8
            // 
            button8.BackColor = Color.Gray;
            button8.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            button8.Location = new Point(321, 331);
            button8.Name = "button8";
            button8.Size = new Size(194, 46);
            button8.TabIndex = 7;
            button8.Text = "프레임 화질 조정";
            button8.UseVisualStyleBackColor = false;
            // 
            // button7
            // 
            button7.BackColor = Color.Gray;
            button7.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            button7.Location = new Point(185, 331);
            button7.Name = "button7";
            button7.Size = new Size(116, 46);
            button7.TabIndex = 6;
            button7.Text = "화질 조절";
            button7.UseVisualStyleBackColor = false;
            // 
            // button6
            // 
            button6.BackColor = Color.Gray;
            button6.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            button6.Location = new Point(17, 331);
            button6.Name = "button6";
            button6.Size = new Size(113, 46);
            button6.TabIndex = 5;
            button6.Text = "학습중단";
            button6.UseVisualStyleBackColor = false;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chart1.Legends.Add(legend1);
            chart1.Location = new Point(321, 60);
            chart1.Name = "chart1";
            chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Excel;
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chart1.Series.Add(series1);
            chart1.Size = new Size(328, 244);
            chart1.TabIndex = 4;
            chart1.Text = "chart1";
            chart1.Click += chart1_Click;
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
            listBox1.Size = new Size(284, 244);
            listBox1.TabIndex = 1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tabControl1);
            Name = "Form1";
            Text = "Form1";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tabPage2.ResumeLayout(false);
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
    }
}
