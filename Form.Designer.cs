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
            tabControl = new TabControl();
            tab_data = new TabPage();
            picEdge = new PictureBox();
            btnSetEnd = new Button();
            panel1 = new Panel();
            btn_restore = new Button();
            listImages = new ListBox();
            btnPlay = new Button();
            btn_delete = new Button();
            btn_openfolder = new Button();
            btnSetStart = new Button();
            btn_stopSlider = new Button();
            btn_playSlider = new Button();
            text_angle = new TextBox();
            picture_Gage = new PictureBox();
            text_throttle = new TextBox();
            btn_imgnext = new Button();
            btn_changquality = new Button();
            btn_before = new Button();
            trackBar_frame = new TrackBar();
            picImage = new PictureBox();
            tab_train = new TabPage();
            panel2 = new Panel();
            btn_stopTrain = new Button();
            chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            btn_train = new Button();
            combo_model = new ComboBox();
            list_log = new ListBox();
            timer1 = new System.Windows.Forms.Timer(components);
            tabControl.SuspendLayout();
            tab_data.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picEdge).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picture_Gage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar_frame).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picImage).BeginInit();
            tab_train.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chart1).BeginInit();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl.Controls.Add(tab_data);
            tabControl.Controls.Add(tab_train);
            tabControl.Location = new Point(1, -3);
            tabControl.Margin = new Padding(2);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1133, 594);
            tabControl.TabIndex = 0;
            // 
            // tab_data
            // 
            tab_data.BackColor = Color.FromArgb(64, 64, 64);
            tab_data.Controls.Add(picEdge);
            tab_data.Controls.Add(btnSetEnd);
            tab_data.Controls.Add(panel1);
            tab_data.Controls.Add(btnSetStart);
            tab_data.Controls.Add(btn_stopSlider);
            tab_data.Controls.Add(btn_playSlider);
            tab_data.Controls.Add(text_angle);
            tab_data.Controls.Add(picture_Gage);
            tab_data.Controls.Add(text_throttle);
            tab_data.Controls.Add(btn_imgnext);
            tab_data.Controls.Add(btn_changquality);
            tab_data.Controls.Add(btn_before);
            tab_data.Controls.Add(trackBar_frame);
            tab_data.Controls.Add(picImage);
            tab_data.Font = new Font("맑은 고딕", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 129);
            tab_data.ForeColor = SystemColors.ControlText;
            tab_data.Location = new Point(4, 24);
            tab_data.Margin = new Padding(2);
            tab_data.Name = "tab_data";
            tab_data.Padding = new Padding(2);
            tab_data.Size = new Size(1125, 566);
            tab_data.TabIndex = 0;
            tab_data.Text = "데이터 매니저";
            // 
            // picEdge
            // 
            picEdge.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            picEdge.BackColor = Color.White;
            picEdge.Location = new Point(336, 6);
            picEdge.Margin = new Padding(2);
            picEdge.Name = "picEdge";
            picEdge.Size = new Size(308, 392);
            picEdge.SizeMode = PictureBoxSizeMode.Zoom;
            picEdge.TabIndex = 12;
            picEdge.TabStop = false;
            // 
            // btnSetEnd
            // 
            btnSetEnd.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSetEnd.BackColor = Color.Gray;
            btnSetEnd.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnSetEnd.Location = new Point(235, 509);
            btnSetEnd.Margin = new Padding(2);
            btnSetEnd.Name = "btnSetEnd";
            btnSetEnd.Size = new Size(141, 37);
            btnSetEnd.TabIndex = 16;
            btnSetEnd.Text = "끝 프레임 설정";
            btnSetEnd.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            panel1.Controls.Add(btn_restore);
            panel1.Controls.Add(listImages);
            panel1.Controls.Add(btnPlay);
            panel1.Controls.Add(btn_delete);
            panel1.Controls.Add(btn_openfolder);
            panel1.Location = new Point(671, 265);
            panel1.Margin = new Padding(2);
            panel1.Name = "panel1";
            panel1.Size = new Size(378, 281);
            panel1.TabIndex = 11;
            // 
            // btn_restore
            // 
            btn_restore.BackColor = Color.Gray;
            btn_restore.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn_restore.Location = new Point(22, 242);
            btn_restore.Margin = new Padding(2);
            btn_restore.Name = "btn_restore";
            btn_restore.Size = new Size(106, 34);
            btn_restore.TabIndex = 17;
            btn_restore.Text = "프레임 복구";
            btn_restore.UseVisualStyleBackColor = false;
            // 
            // listImages
            // 
            listImages.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point, 129);
            listImages.FormattingEnabled = true;
            listImages.Location = new Point(4, 4);
            listImages.Margin = new Padding(4);
            listImages.Name = "listImages";
            listImages.Size = new Size(369, 199);
            listImages.TabIndex = 14;
            listImages.SelectedIndexChanged += listImages_SelectedIndexChanged;
            // 
            // btnPlay
            // 
            btnPlay.BackColor = Color.Gray;
            btnPlay.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnPlay.Location = new Point(197, 206);
            btnPlay.Margin = new Padding(2);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(123, 34);
            btnPlay.TabIndex = 13;
            btnPlay.Text = "재생";
            btnPlay.UseVisualStyleBackColor = false;
            btnPlay.Click += btnPlay_Click;
            // 
            // btn_delete
            // 
            btn_delete.BackColor = Color.Gray;
            btn_delete.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn_delete.Location = new Point(2, 206);
            btn_delete.Margin = new Padding(2);
            btn_delete.Name = "btn_delete";
            btn_delete.Size = new Size(142, 34);
            btn_delete.TabIndex = 3;
            btn_delete.Text = "프레임 삭제";
            btn_delete.UseVisualStyleBackColor = false;
            btn_delete.Click += button2_Click;
            // 
            // btn_openfolder
            // 
            btn_openfolder.BackColor = Color.Gray;
            btn_openfolder.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn_openfolder.Location = new Point(240, 239);
            btn_openfolder.Margin = new Padding(2);
            btn_openfolder.Name = "btn_openfolder";
            btn_openfolder.Size = new Size(106, 34);
            btn_openfolder.TabIndex = 2;
            btn_openfolder.Text = "폴더 열기";
            btn_openfolder.UseVisualStyleBackColor = false;
            // 
            // btnSetStart
            // 
            btnSetStart.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSetStart.BackColor = Color.Gray;
            btnSetStart.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnSetStart.Location = new Point(72, 509);
            btnSetStart.Margin = new Padding(2);
            btnSetStart.Name = "btnSetStart";
            btnSetStart.Size = new Size(142, 37);
            btnSetStart.TabIndex = 15;
            btnSetStart.Text = "시작 프레임 설정";
            btnSetStart.UseVisualStyleBackColor = false;
            // 
            // btn_stopSlider
            // 
            btn_stopSlider.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btn_stopSlider.BackColor = Color.FromArgb(64, 64, 64);
            btn_stopSlider.BackgroundImage = (Image)resources.GetObject("btn_stopSlider.BackgroundImage");
            btn_stopSlider.BackgroundImageLayout = ImageLayout.Stretch;
            btn_stopSlider.Location = new Point(63, 399);
            btn_stopSlider.Margin = new Padding(2);
            btn_stopSlider.Name = "btn_stopSlider";
            btn_stopSlider.Size = new Size(53, 48);
            btn_stopSlider.TabIndex = 10;
            btn_stopSlider.UseVisualStyleBackColor = false;
            // 
            // btn_playSlider
            // 
            btn_playSlider.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btn_playSlider.BackColor = Color.FromArgb(64, 64, 64);
            btn_playSlider.BackgroundImage = (Image)resources.GetObject("btn_playSlider.BackgroundImage");
            btn_playSlider.BackgroundImageLayout = ImageLayout.Stretch;
            btn_playSlider.Location = new Point(10, 399);
            btn_playSlider.Margin = new Padding(2);
            btn_playSlider.Name = "btn_playSlider";
            btn_playSlider.Size = new Size(49, 48);
            btn_playSlider.TabIndex = 9;
            btn_playSlider.UseVisualStyleBackColor = false;
            // 
            // text_angle
            // 
            text_angle.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            text_angle.BackColor = Color.Gray;
            text_angle.Font = new Font("맑은 고딕", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 129);
            text_angle.Location = new Point(873, 217);
            text_angle.Margin = new Padding(2);
            text_angle.Name = "text_angle";
            text_angle.Size = new Size(151, 38);
            text_angle.TabIndex = 6;
            text_angle.Text = " 앵글:";
            // 
            // picture_Gage
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(874, 27);
            pictureBox2.Margin = new Padding(3, 2, 3, 2);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(465, 261);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 8;
            pictureBox2.TabStop = false;
            // 
            // text_throttle
            // 
            text_throttle.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            text_throttle.BackColor = Color.Gray;
            text_throttle.Font = new Font("맑은 고딕", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 129);
            text_throttle.Location = new Point(693, 217);
            text_throttle.Margin = new Padding(2);
            text_throttle.Name = "text_throttle";
            text_throttle.Size = new Size(142, 38);
            text_throttle.TabIndex = 7;
            text_throttle.Text = " 속도:";
            text_throttle.TextChanged += textBox2_TextChanged;
            // 
            // btn_imgnext
            // 
            btn_imgnext.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btn_imgnext.BackColor = Color.Gray;
            btn_imgnext.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn_imgnext.Location = new Point(358, 459);
            btn_imgnext.Margin = new Padding(2);
            btn_imgnext.Name = "btn_imgnext";
            btn_imgnext.Size = new Size(89, 48);
            btn_imgnext.TabIndex = 5;
            btn_imgnext.Text = "프레임 ▶";
            btn_imgnext.UseVisualStyleBackColor = false;
            btn_imgnext.Click += btn_imgnext_Click;
            // 
            // btn_changquality
            // 
            btn_changquality.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btn_changquality.BackColor = Color.Gray;
            btn_changquality.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn_changquality.Location = new Point(490, 459);
            btn_changquality.Margin = new Padding(2);
            btn_changquality.Name = "btn_changquality";
            btn_changquality.Size = new Size(154, 48);
            btn_changquality.TabIndex = 4;
            btn_changquality.Text = "프레임 화질 조정";
            btn_changquality.UseVisualStyleBackColor = false;
            // 
            // btn_before
            // 
            btn_before.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btn_before.BackColor = Color.Gray;
            btn_before.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn_before.Location = new Point(207, 448);
            btn_before.Margin = new Padding(2);
            btn_before.Name = "btn_before";
            btn_before.Size = new Size(89, 48);
            btn_before.TabIndex = 4;
            btn_before.Text = "프레임 ◀";
            btn_before.UseVisualStyleBackColor = false;
            // 
            // trackBar_frame
            // 
            trackBar_frame.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            trackBar_frame.BackColor = Color.Gray;
            trackBar_frame.Location = new Point(120, 399);
            trackBar_frame.Margin = new Padding(2);
            trackBar_frame.Name = "trackBar_frame";
            trackBar_frame.Size = new Size(674, 56);
            trackBar_frame.TabIndex = 1;
            trackBar_frame.Scroll += trackBar_frame_Scroll;
            // 
            // picImage
            // 
            picImage.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            picImage.BackColor = Color.White;
            picImage.Location = new Point(10, 6);
            picImage.Margin = new Padding(2);
            picImage.Name = "picImage";
            picImage.Size = new Size(322, 392);
            picImage.SizeMode = PictureBoxSizeMode.Zoom;
            picImage.TabIndex = 0;
            picImage.TabStop = false;
            picImage.Click += pictureBox1_Click;
            // 
            // tab_train
            // 
            tab_train.BackColor = Color.FromArgb(64, 64, 64);
            tab_train.Controls.Add(panel2);
            tab_train.Controls.Add(chart1);
            tab_train.Controls.Add(btn_train);
            tab_train.Controls.Add(combo_model);
            tab_train.Controls.Add(list_log);
            tab_train.Location = new Point(4, 24);
            tab_train.Margin = new Padding(2);
            tab_train.Name = "tab_train";
            tab_train.Padding = new Padding(2);
            tab_train.Size = new Size(1125, 566);
            tab_train.TabIndex = 1;
            tab_train.Text = "학습";
            // 
            // panel2
            // 
            panel2.Controls.Add(btn_stopTrain);
            panel2.Location = new Point(784, 45);
            panel2.Margin = new Padding(2);
            panel2.Name = "panel2";
            panel2.Size = new Size(355, 326);
            panel2.TabIndex = 8;
            // 
            // btn_stopTrain
            // 
            btn_stopTrain.BackColor = Color.Gray;
            btn_stopTrain.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn_stopTrain.Location = new Point(18, 33);
            btn_stopTrain.Margin = new Padding(2);
            btn_stopTrain.Name = "btn_stopTrain";
            btn_stopTrain.Size = new Size(87, 34);
            btn_stopTrain.TabIndex = 5;
            btn_stopTrain.Text = "학습중단";
            btn_stopTrain.UseVisualStyleBackColor = false;
            // 
            // chart1
            // 
            chartArea2.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            chart1.Legends.Add(legend2);
            chart1.Location = new Point(562, 61);
            chart1.Margin = new Padding(3, 2, 3, 2);
            chart1.Name = "chart1";
            chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Excel;
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            chart1.Series.Add(series2);
            chart1.Size = new Size(280, 324);
            chart1.TabIndex = 4;
            chart1.Text = "chart1";
            chart1.Click += chart1_Click;
            // 
            // btn_train
            // 
            btn_train.BackColor = Color.Gray;
            btn_train.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn_train.Location = new Point(307, 16);
            btn_train.Margin = new Padding(3, 2, 3, 2);
            btn_train.Name = "btn_train";
            btn_train.Size = new Size(116, 39);
            btn_train.TabIndex = 3;
            btn_train.Text = "train";
            btn_train.UseVisualStyleBackColor = false;
            btn_train.Click += btn_train_Click;
            // 
            // combo_model
            // 
            combo_model.FormattingEnabled = true;
            combo_model.Location = new Point(17, 16);
            combo_model.Margin = new Padding(3, 2, 3, 2);
            combo_model.Name = "combo_model";
            combo_model.Size = new Size(151, 28);
            combo_model.TabIndex = 2;
            // 
            // list_log
            // 
            list_log.FormattingEnabled = true;
            list_log.Location = new Point(17, 60);
            list_log.Margin = new Padding(3, 2, 3, 2);
            list_log.Name = "list_log";
            list_log.Size = new Size(466, 624);
            list_log.TabIndex = 1;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // button1
            // 
            button1.BackColor = Color.Gray;
            button1.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            button1.Location = new Point(4, 323);
            button1.Margin = new Padding(3, 2, 3, 2);
            button1.Name = "button1";
            button1.Size = new Size(182, 45);
            button1.TabIndex = 15;
            button1.Text = "시작 프레임 설정";
            button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            button2.BackColor = Color.Gray;
            button2.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            button2.Location = new Point(325, 323);
            button2.Margin = new Padding(3, 2, 3, 2);
            button2.Name = "button2";
            button2.Size = new Size(158, 45);
            button2.TabIndex = 16;
            button2.Text = "끝 프레임 설정";
            button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            button3.BackColor = Color.Gray;
            button3.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            button3.Location = new Point(189, 323);
            button3.Margin = new Padding(3, 2, 3, 2);
            button3.Name = "button3";
            button3.Size = new Size(136, 45);
            button3.TabIndex = 17;
            button3.Text = "프레임 복구";
            button3.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1135, 585);
            Controls.Add(tabControl);
            Margin = new Padding(2);
            Name = "Form1";
            Text = "데이터 매니저v1.0";
            tabControl.ResumeLayout(false);
            tab_data.ResumeLayout(false);
            tab_data.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picEdge).EndInit();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picture_Gage).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar_frame).EndInit();
            ((System.ComponentModel.ISupportInitialize)picImage).EndInit();
            tab_train.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chart1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl;
        private TabPage tab_data;
        private TabPage tab_train;
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
        private Button btn_stopTrain;
        private PictureBox picture_Gage;
        private Button btn_playSlider;
        private Button btn_stopSlider;
        private Panel panel1;
        private Panel panel2;
        private PictureBox picEdge;
        private Button btnPlay;
        private System.Windows.Forms.Timer timer1;
        private ListBox listImages;
        private Button btn_changquality;
        private Button btnSetStart;
        private Button btn_restore;
        private Button btnSetEnd;
    }
}
