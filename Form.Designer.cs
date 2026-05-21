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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            picEdge = new PictureBox();
            panel1 = new Panel();
            listImages = new ListBox();
            btnPlay = new Button();
            btn_delete = new Button();
            btn_openfolder = new Button();
            btn_stop = new Button();
            btn_play_slider = new Button();
            text_angle = new TextBox();
            pictureBox2 = new PictureBox();
            text_throttle = new TextBox();
            btn_imgnext = new Button();
            btn_changquality = new Button();
            btn_before = new Button();
            trackBar_frame = new TrackBar();
            picImage = new PictureBox();
            tabPage2 = new TabPage();
            panel2 = new Panel();
            btn_learingstop = new Button();
            chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            btn_train = new Button();
            combo_model = new ComboBox();
            list_log = new ListBox();
            timer1 = new System.Windows.Forms.Timer(components);
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picEdge).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar_frame).BeginInit();
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
            tabPage1.Controls.Add(btn_stop);
            tabPage1.Controls.Add(btn_play_slider);
            tabPage1.Controls.Add(text_angle);
            tabPage1.Controls.Add(pictureBox2);
            tabPage1.Controls.Add(text_throttle);
            tabPage1.Controls.Add(btn_imgnext);
            tabPage1.Controls.Add(btn_changquality);
            tabPage1.Controls.Add(btn_before);
            tabPage1.Controls.Add(trackBar_frame);
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
            picEdge.Location = new Point(345, 20);
            picEdge.Margin = new Padding(2);
            picEdge.Name = "picEdge";
            picEdge.Size = new Size(308, 389);
            picEdge.SizeMode = PictureBoxSizeMode.Zoom;
            picEdge.TabIndex = 12;
            picEdge.TabStop = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(listImages);
            panel1.Controls.Add(btnPlay);
            panel1.Controls.Add(btn_delete);
            panel1.Controls.Add(btn_openfolder);
            panel1.Location = new Point(680, 279);
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
            // btn_delete
            // 
            btn_delete.BackColor = Color.Gray;
            btn_delete.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn_delete.Location = new Point(22, 214);
            btn_delete.Margin = new Padding(2);
            btn_delete.Name = "btn_delete";
            btn_delete.Size = new Size(125, 45);
            btn_delete.TabIndex = 3;
            btn_delete.Text = "프레임 삭제";
            btn_delete.UseVisualStyleBackColor = false;
            btn_delete.Click += button2_Click;
            // 
            // btn_openfolder
            // 
            btn_openfolder.BackColor = Color.Gray;
            btn_openfolder.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn_openfolder.Location = new Point(151, 214);
            btn_openfolder.Margin = new Padding(2);
            btn_openfolder.Name = "btn_openfolder";
            btn_openfolder.Size = new Size(106, 45);
            btn_openfolder.TabIndex = 2;
            btn_openfolder.Text = "폴더 열기";
            btn_openfolder.UseVisualStyleBackColor = false;
            // 
            // btn_stop
            // 
            btn_stop.BackColor = Color.FromArgb(64, 64, 64);
            btn_stop.BackgroundImage = (Image)resources.GetObject("btn_stop.BackgroundImage");
            btn_stop.BackgroundImageLayout = ImageLayout.Stretch;
            btn_stop.Location = new Point(72, 413);
            btn_stop.Margin = new Padding(2);
            btn_stop.Name = "btn_stop";
            btn_stop.Size = new Size(53, 45);
            btn_stop.TabIndex = 10;
            btn_stop.UseVisualStyleBackColor = false;
            // 
            // btn_play_slider
            // 
            btn_play_slider.BackColor = Color.FromArgb(64, 64, 64);
            btn_play_slider.BackgroundImage = (Image)resources.GetObject("btn_play_slider.BackgroundImage");
            btn_play_slider.BackgroundImageLayout = ImageLayout.Stretch;
            btn_play_slider.Location = new Point(19, 413);
            btn_play_slider.Margin = new Padding(2);
            btn_play_slider.Name = "btn_play_slider";
            btn_play_slider.Size = new Size(49, 45);
            btn_play_slider.TabIndex = 9;
            btn_play_slider.UseVisualStyleBackColor = false;
            // 
            // text_angle
            // 
            text_angle.BackColor = Color.Gray;
            text_angle.Font = new Font("맑은 고딕", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 129);
            text_angle.Location = new Point(882, 231);
            text_angle.Margin = new Padding(2);
            text_angle.Name = "text_angle";
            text_angle.Size = new Size(84, 32);
            text_angle.TabIndex = 6;
            text_angle.Text = " 앵글:";
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
            // text_throttle
            // 
            text_throttle.BackColor = Color.Gray;
            text_throttle.Font = new Font("맑은 고딕", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 129);
            text_throttle.Location = new Point(702, 231);
            text_throttle.Margin = new Padding(2);
            text_throttle.Name = "text_throttle";
            text_throttle.Size = new Size(92, 32);
            text_throttle.TabIndex = 7;
            text_throttle.Text = " 속도:";
            text_throttle.TextChanged += textBox2_TextChanged;
            // 
            // btn_imgnext
            // 
            btn_imgnext.BackColor = Color.Gray;
            btn_imgnext.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn_imgnext.Location = new Point(252, 473);
            btn_imgnext.Margin = new Padding(2);
            btn_imgnext.Name = "btn_imgnext";
            btn_imgnext.Size = new Size(89, 45);
            btn_imgnext.TabIndex = 5;
            btn_imgnext.Text = "프레임 ▶";
            btn_imgnext.UseVisualStyleBackColor = false;
            btn_imgnext.Click += btn_imgnext_Click;
            // 
            // btn_changquality
            // 
            btn_changquality.BackColor = Color.Gray;
            btn_changquality.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn_changquality.Location = new Point(354, 473);
            btn_changquality.Margin = new Padding(2);
            btn_changquality.Name = "btn_changquality";
            btn_changquality.Size = new Size(154, 45);
            btn_changquality.TabIndex = 4;
            btn_changquality.Text = "프레임 화질 조정";
            btn_changquality.UseVisualStyleBackColor = false;
            // 
            // btn_before
            // 
            btn_before.BackColor = Color.Gray;
            btn_before.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn_before.Location = new Point(148, 473);
            btn_before.Margin = new Padding(2);
            btn_before.Name = "btn_before";
            btn_before.Size = new Size(89, 45);
            btn_before.TabIndex = 4;
            btn_before.Text = "프레임 ◀";
            btn_before.UseVisualStyleBackColor = false;
            // 
            // trackBar_frame
            // 
            trackBar_frame.BackColor = Color.Gray;
            trackBar_frame.Location = new Point(129, 413);
            trackBar_frame.Margin = new Padding(2);
            trackBar_frame.Name = "trackBar_frame";
            trackBar_frame.Size = new Size(524, 45);
            trackBar_frame.TabIndex = 1;
            trackBar_frame.Scroll += trackBar_frame_Scroll;
            // 
            // picImage
            // 
            picImage.BackColor = Color.White;
            picImage.Location = new Point(19, 20);
            picImage.Margin = new Padding(2);
            picImage.Name = "picImage";
            picImage.Size = new Size(322, 389);
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
            tabPage2.Controls.Add(btn_train);
            tabPage2.Controls.Add(combo_model);
            tabPage2.Controls.Add(list_log);
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
            panel2.Controls.Add(btn_learingstop);
            panel2.Location = new Point(784, 45);
            panel2.Margin = new Padding(2);
            panel2.Name = "panel2";
            panel2.Size = new Size(276, 244);
            panel2.TabIndex = 8;
            // 
            // btn_learingstop
            // 
            btn_learingstop.BackColor = Color.Gray;
            btn_learingstop.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn_learingstop.Location = new Point(18, 33);
            btn_learingstop.Margin = new Padding(2);
            btn_learingstop.Name = "btn_learingstop";
            btn_learingstop.Size = new Size(87, 34);
            btn_learingstop.TabIndex = 5;
            btn_learingstop.Text = "학습중단";
            btn_learingstop.UseVisualStyleBackColor = false;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chart1.Legends.Add(legend1);
            chart1.Location = new Point(437, 46);
            chart1.Margin = new Padding(2);
            chart1.Name = "chart1";
            chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Excel;
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chart1.Series.Add(series1);
            chart1.Size = new Size(218, 243);
            chart1.TabIndex = 4;
            chart1.Text = "chart1";
            chart1.Click += chart1_Click;
            // 
            // btn_train
            // 
            btn_train.BackColor = Color.Gray;
            btn_train.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn_train.Location = new Point(239, 12);
            btn_train.Margin = new Padding(2);
            btn_train.Name = "btn_train";
            btn_train.Size = new Size(90, 29);
            btn_train.TabIndex = 3;
            btn_train.Text = "train";
            btn_train.UseVisualStyleBackColor = false;
            // 
            // combo_model
            // 
            combo_model.FormattingEnabled = true;
            combo_model.Location = new Point(13, 12);
            combo_model.Margin = new Padding(2);
            combo_model.Name = "combo_model";
            combo_model.Size = new Size(118, 23);
            combo_model.TabIndex = 2;
            // 
            // list_log
            // 
            list_log.FormattingEnabled = true;
            list_log.Location = new Point(13, 45);
            list_log.Margin = new Padding(2);
            list_log.Name = "list_log";
            list_log.Size = new Size(363, 469);
            list_log.TabIndex = 1;
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
            ((System.ComponentModel.ISupportInitialize)trackBar_frame).EndInit();
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
        private Button btn_imgnext;
        private Button btn_before;
        private Button btn_delete;
        private Button btn_openfolder;
        private TrackBar trackBar_frame;
        private PictureBox picImage;
        private TextBox text_throttle;
        private TextBox text_angle;
        private Button btn_train;
        private ComboBox combo_model;
        private ListBox list_log;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private Button btn_learingstop;
        private PictureBox pictureBox2;
        private Button btn_play_slider;
        private Button btn_stop;
        private Panel panel1;
        private Panel panel2;
        private PictureBox picEdge;
        private Button btnPlay;
        private System.Windows.Forms.Timer timer1;
        private ListBox listImages;
        private Button btn_changquality;
    }
}
