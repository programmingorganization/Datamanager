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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            tabControl = new TabControl();
            tab_data = new TabPage();
            panelTrackBarProgress = new Panel();
            trackBar_frame = new TrackBar();
            picEdge = new PictureBox();
            panel1 = new Panel();
            label1 = new Label();
            btn_restore = new Button();
            btnSetEnd = new Button();
            btnSetStart = new Button();
            btn_delete = new Button();
            listImages = new ListBox();
            btn_openfolder = new Button();
            text_throttle = new TextBox();
            btnPlay = new Button();
            text_angle = new TextBox();
            picture_Gage = new PictureBox();
            btn_imgnext = new Button();
            btn_changquality = new Button();
            btn_before = new Button();
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
            panelTrackBarProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar_frame).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picEdge).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picture_Gage).BeginInit();
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
            tabControl.ItemSize = new Size(120, 32);
            tabControl.Location = new Point(1, -3);
            tabControl.Margin = new Padding(2);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1248, 777);
            tabControl.TabIndex = 0;
            // 
            // tab_data
            // 
            tab_data.BackColor = Color.FromArgb(64, 64, 64);
            tab_data.Controls.Add(panelTrackBarProgress);
            tab_data.Controls.Add(picEdge);
            tab_data.Controls.Add(panel1);
            tab_data.Controls.Add(btn_openfolder);
            tab_data.Controls.Add(text_throttle);
            tab_data.Controls.Add(btnPlay);
            tab_data.Controls.Add(text_angle);
            tab_data.Controls.Add(picture_Gage);
            tab_data.Controls.Add(btn_imgnext);
            tab_data.Controls.Add(btn_changquality);
            tab_data.Controls.Add(btn_before);
            tab_data.Controls.Add(picImage);
            tab_data.Font = new Font("맑은 고딕", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 129);
            tab_data.ForeColor = SystemColors.ControlText;
            tab_data.Location = new Point(4, 36);
            tab_data.Margin = new Padding(2);
            tab_data.Name = "tab_data";
            tab_data.Padding = new Padding(2);
            tab_data.Size = new Size(1240, 737);
            tab_data.TabIndex = 0;
            tab_data.Text = "데이터 매니저";
            // 
            // panelTrackBarProgress
            // 
            panelTrackBarProgress.BackColor = Color.FromArgb(50, 50, 50);
            panelTrackBarProgress.Controls.Add(trackBar_frame);
            panelTrackBarProgress.Location = new Point(148, 442);
            panelTrackBarProgress.Name = "panelTrackBarProgress";
            panelTrackBarProgress.Size = new Size(524, 37);
            panelTrackBarProgress.TabIndex = 13;
            // 
            // trackBar_frame
            // 
            trackBar_frame.BackColor = Color.FromArgb(79, 195, 247);
            trackBar_frame.Dock = DockStyle.Left;
            trackBar_frame.Location = new Point(0, 0);
            trackBar_frame.Margin = new Padding(2);
            trackBar_frame.Name = "trackBar_frame";
            trackBar_frame.Size = new Size(524, 37);
            trackBar_frame.TabIndex = 1;
            trackBar_frame.Scroll += trackBar_frame_Scroll;
            // 
            // picEdge
            // 
            picEdge.BackColor = Color.White;
            picEdge.Location = new Point(372, 29);
            picEdge.Margin = new Padding(2);
            picEdge.Name = "picEdge";
            picEdge.Size = new Size(308, 394);
            picEdge.SizeMode = PictureBoxSizeMode.Zoom;
            picEdge.TabIndex = 12;
            picEdge.TabStop = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(label1);
            panel1.Controls.Add(btn_restore);
            panel1.Controls.Add(btnSetEnd);
            panel1.Controls.Add(btnSetStart);
            panel1.Controls.Add(btn_delete);
            panel1.Controls.Add(listImages);
            panel1.Location = new Point(700, 325);
            panel1.Margin = new Padding(2);
            panel1.Name = "panel1";
            panel1.Size = new Size(510, 402);
            panel1.TabIndex = 11;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(24, 6);
            label1.Name = "label1";
            label1.Size = new Size(114, 25);
            label1.TabIndex = 18;
            label1.Text = "프레임 목록";
            // 
            // btn_restore
            // 
            btn_restore.BackColor = Color.Gray;
            btn_restore.FlatStyle = FlatStyle.Flat;
            btn_restore.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn_restore.Location = new Point(96, 361);
            btn_restore.Margin = new Padding(2);
            btn_restore.Name = "btn_restore";
            btn_restore.Size = new Size(142, 36);
            btn_restore.TabIndex = 17;
            btn_restore.Text = "프레임 복구";
            btn_restore.UseVisualStyleBackColor = false;
            // 
            // btnSetEnd
            // 
            btnSetEnd.BackColor = Color.Gray;
            btnSetEnd.FlatStyle = FlatStyle.Flat;
            btnSetEnd.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnSetEnd.Location = new Point(260, 316);
            btnSetEnd.Margin = new Padding(2);
            btnSetEnd.Name = "btnSetEnd";
            btnSetEnd.Size = new Size(141, 39);
            btnSetEnd.TabIndex = 16;
            btnSetEnd.Text = "끝 프레임";
            btnSetEnd.UseVisualStyleBackColor = false;
            // 
            // btnSetStart
            // 
            btnSetStart.BackColor = Color.Gray;
            btnSetStart.FlatStyle = FlatStyle.Flat;
            btnSetStart.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnSetStart.Location = new Point(96, 316);
            btnSetStart.Margin = new Padding(2);
            btnSetStart.Name = "btnSetStart";
            btnSetStart.Size = new Size(142, 39);
            btnSetStart.TabIndex = 15;
            btnSetStart.Text = "시작 프레임\r\n";
            btnSetStart.UseVisualStyleBackColor = false;
            // 
            // btn_delete
            // 
            btn_delete.BackColor = Color.Gray;
            btn_delete.FlatStyle = FlatStyle.Flat;
            btn_delete.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn_delete.Location = new Point(260, 360);
            btn_delete.Margin = new Padding(2);
            btn_delete.Name = "btn_delete";
            btn_delete.Size = new Size(141, 39);
            btn_delete.TabIndex = 3;
            btn_delete.Text = "프레임 삭제";
            btn_delete.UseVisualStyleBackColor = false;
            btn_delete.Click += button2_Click;
            // 
            // listImages
            // 
            listImages.BackColor = Color.FromArgb(22, 27, 3);
            listImages.BorderStyle = BorderStyle.FixedSingle;
            listImages.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point, 129);
            listImages.FormattingEnabled = true;
            listImages.Location = new Point(17, 36);
            listImages.Name = "listImages";
            listImages.Size = new Size(481, 272);
            listImages.TabIndex = 14;
            listImages.SelectedIndexChanged += listImages_SelectedIndexChanged;
            // 
            // btn_openfolder
            // 
            btn_openfolder.BackColor = Color.Gray;
            btn_openfolder.FlatStyle = FlatStyle.Flat;
            btn_openfolder.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn_openfolder.Location = new Point(481, 513);
            btn_openfolder.Margin = new Padding(2);
            btn_openfolder.Name = "btn_openfolder";
            btn_openfolder.Size = new Size(129, 50);
            btn_openfolder.TabIndex = 2;
            btn_openfolder.Text = "폴더 열기";
            btn_openfolder.UseVisualStyleBackColor = false;
            // 
            // text_throttle
            // 
            text_throttle.BackColor = Color.FromArgb(13, 13, 24);
            text_throttle.BorderStyle = BorderStyle.None;
            text_throttle.Font = new Font("맑은 고딕", 20.25F, FontStyle.Bold);
            text_throttle.Location = new Point(816, 273);
            text_throttle.Margin = new Padding(2);
            text_throttle.Name = "text_throttle";
            text_throttle.Size = new Size(92, 36);
            text_throttle.TabIndex = 7;
            text_throttle.TextChanged += textBox2_TextChanged;
            // 
            // btnPlay
            // 
            btnPlay.BackColor = Color.Gray;
            btnPlay.FlatStyle = FlatStyle.Flat;
            btnPlay.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnPlay.Location = new Point(36, 436);
            btnPlay.Margin = new Padding(2);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(108, 34);
            btnPlay.TabIndex = 13;
            btnPlay.Text = "▶";
            btnPlay.UseVisualStyleBackColor = false;
            btnPlay.Click += btnPlay_Click;
            // 
            // text_angle
            // 
            text_angle.BackColor = Color.FromArgb(13, 13, 24);
            text_angle.BorderStyle = BorderStyle.None;
            text_angle.Font = new Font("맑은 고딕", 20.25F, FontStyle.Bold);
            text_angle.Location = new Point(1037, 273);
            text_angle.Margin = new Padding(2);
            text_angle.Name = "text_angle";
            text_angle.Size = new Size(101, 36);
            text_angle.TabIndex = 6;
            // 
            // picture_Gage
            // 
            picture_Gage.Image = Properties.Resources._5번;
            picture_Gage.Location = new Point(700, 28);
            picture_Gage.Margin = new Padding(2);
            picture_Gage.Name = "picture_Gage";
            picture_Gage.Size = new Size(531, 293);
            picture_Gage.SizeMode = PictureBoxSizeMode.StretchImage;
            picture_Gage.TabIndex = 8;
            picture_Gage.TabStop = false;
            // 
            // btn_imgnext
            // 
            btn_imgnext.BackColor = Color.Gray;
            btn_imgnext.FlatStyle = FlatStyle.Flat;
            btn_imgnext.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn_imgnext.Location = new Point(164, 513);
            btn_imgnext.Margin = new Padding(2);
            btn_imgnext.Name = "btn_imgnext";
            btn_imgnext.Size = new Size(105, 50);
            btn_imgnext.TabIndex = 5;
            btn_imgnext.Text = "프레임 ▶";
            btn_imgnext.UseVisualStyleBackColor = false;
            btn_imgnext.Click += btn_imgnext_Click;
            // 
            // btn_changquality
            // 
            btn_changquality.BackColor = Color.Gray;
            btn_changquality.FlatStyle = FlatStyle.Flat;
            btn_changquality.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn_changquality.Location = new Point(312, 513);
            btn_changquality.Margin = new Padding(2);
            btn_changquality.Name = "btn_changquality";
            btn_changquality.Size = new Size(154, 50);
            btn_changquality.TabIndex = 4;
            btn_changquality.Text = "프레임 화질 조정";
            btn_changquality.UseVisualStyleBackColor = false;
            // 
            // btn_before
            // 
            btn_before.BackColor = Color.Gray;
            btn_before.FlatStyle = FlatStyle.Flat;
            btn_before.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn_before.Location = new Point(43, 513);
            btn_before.Margin = new Padding(2);
            btn_before.Name = "btn_before";
            btn_before.Size = new Size(99, 50);
            btn_before.TabIndex = 4;
            btn_before.Text = "프레임 ◀";
            btn_before.UseVisualStyleBackColor = false;
            // 
            // picImage
            // 
            picImage.BackColor = Color.White;
            picImage.Location = new Point(31, 29);
            picImage.Margin = new Padding(2);
            picImage.Name = "picImage";
            picImage.Size = new Size(322, 394);
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
            tab_train.Location = new Point(4, 36);
            tab_train.Margin = new Padding(2);
            tab_train.Name = "tab_train";
            tab_train.Padding = new Padding(2);
            tab_train.Size = new Size(1240, 737);
            tab_train.TabIndex = 1;
            tab_train.Text = "학습";
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            panel2.Controls.Add(btn_stopTrain);
            panel2.Location = new Point(815, 65);
            panel2.Margin = new Padding(2);
            panel2.Name = "panel2";
            panel2.Size = new Size(276, 244);
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
            chart1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            chartArea1.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chart1.Legends.Add(legend1);
            chart1.Location = new Point(468, 66);
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
            btn_train.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btn_train.BackColor = Color.Gray;
            btn_train.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn_train.Location = new Point(270, 32);
            btn_train.Margin = new Padding(2);
            btn_train.Name = "btn_train";
            btn_train.Size = new Size(90, 29);
            btn_train.TabIndex = 3;
            btn_train.Text = "train";
            btn_train.UseVisualStyleBackColor = false;
            btn_train.Click += btn_train_Click;
            // 
            // combo_model
            // 
            combo_model.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            combo_model.FormattingEnabled = true;
            combo_model.Location = new Point(44, 32);
            combo_model.Margin = new Padding(2);
            combo_model.Name = "combo_model";
            combo_model.Size = new Size(118, 23);
            combo_model.TabIndex = 2;
            // 
            // list_log
            // 
            list_log.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            list_log.FormattingEnabled = true;
            list_log.Location = new Point(44, 65);
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
            BackColor = Color.White;
            ClientSize = new Size(1247, 771);
            Controls.Add(tabControl);
            Margin = new Padding(2);
            Name = "Form1";
            Text = "데이터 매니저v1.0";
            tabControl.ResumeLayout(false);
            tab_data.ResumeLayout(false);
            tab_data.PerformLayout();
            panelTrackBarProgress.ResumeLayout(false);
            panelTrackBarProgress.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar_frame).EndInit();
            ((System.ComponentModel.ISupportInitialize)picEdge).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picture_Gage).EndInit();
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
        private Panel panelTrackBarProgress;
        private Label label1;
    }
}
