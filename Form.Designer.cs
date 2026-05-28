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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            tabControl = new TabControl();
            tab_data = new TabPage();
            splitContainer_allwindow = new SplitContainer();
            splitContainer_up = new SplitContainer();
            flowPanel_thumbnails = new FlowLayoutPanel();
            picEdge = new PictureBox();
            label5 = new Label();
            picImage = new PictureBox();
            btn_openfolder = new Button();
            label4 = new Label();
            btn_imgnext = new Button();
            btnPlay = new Button();
            btn_changquality = new Button();
            checkBox2 = new CheckBox();
            btn_before = new Button();
            panelTrackBarProgress = new Panel();
            trackBar_frame = new TrackBar();
            checkBox1 = new CheckBox();
            tabControl_graph = new TabControl();
            tabPage1 = new TabPage();
            picture_Gage = new PictureBox();
            label_angle = new Label();
            picNeedleAngle = new PictureBox();
            label_throttle = new Label();
            picNeedleSpeed = new PictureBox();
            tabPage2 = new TabPage();
            checkBox_angle = new CheckBox();
            checkBox_throttle = new CheckBox();
            chart_thr_ang = new System.Windows.Forms.DataVisualization.Charting.Chart();
            panel1 = new Panel();
            label1 = new Label();
            btnSetEnd = new Button();
            btnSetStart = new Button();
            btn_delete = new Button();
            listImages = new ListBox();
            panel2 = new Panel();
            label2 = new Label();
            btn_restore = new Button();
            listBox1 = new ListBox();
            tab_train = new TabPage();
            progressBar1 = new ProgressBar();
            listBox2 = new ListBox();
            chart_loss = new System.Windows.Forms.DataVisualization.Charting.Chart();
            btn_stopTrain = new Button();
            btn_train = new Button();
            combo_model = new ComboBox();
            list_log = new ListBox();
            timer1 = new System.Windows.Forms.Timer(components);
            tabControl.SuspendLayout();
            tab_data.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer_allwindow).BeginInit();
            splitContainer_allwindow.Panel1.SuspendLayout();
            splitContainer_allwindow.Panel2.SuspendLayout();
            splitContainer_allwindow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer_up).BeginInit();
            splitContainer_up.Panel1.SuspendLayout();
            splitContainer_up.Panel2.SuspendLayout();
            splitContainer_up.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picEdge).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picImage).BeginInit();
            panelTrackBarProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar_frame).BeginInit();
            tabControl_graph.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picture_Gage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picNeedleAngle).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picNeedleSpeed).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chart_thr_ang).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            tab_train.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chart_loss).BeginInit();
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
            tabControl.Size = new Size(1596, 922);
            tabControl.TabIndex = 0;
            // 
            // tab_data
            // 
            tab_data.BackColor = Color.FromArgb(64, 64, 64);
            tab_data.Controls.Add(splitContainer_allwindow);
            tab_data.Font = new Font("맑은 고딕", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 129);
            tab_data.ForeColor = SystemColors.ControlText;
            tab_data.Location = new Point(4, 36);
            tab_data.Margin = new Padding(2);
            tab_data.Name = "tab_data";
            tab_data.Padding = new Padding(2);
            tab_data.Size = new Size(1588, 882);
            tab_data.TabIndex = 0;
            tab_data.Text = "데이터 매니저";
            // 
            // splitContainer_allwindow
            // 
            splitContainer_allwindow.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer_allwindow.Location = new Point(-2, -2);
            splitContainer_allwindow.Name = "splitContainer_allwindow";
            splitContainer_allwindow.Orientation = Orientation.Horizontal;
            // 
            // splitContainer_allwindow.Panel1
            // 
            splitContainer_allwindow.Panel1.Controls.Add(splitContainer_up);
            // 
            // splitContainer_allwindow.Panel2
            // 
            splitContainer_allwindow.Panel2.Controls.Add(panel1);
            splitContainer_allwindow.Panel2.Controls.Add(panel2);
            splitContainer_allwindow.Size = new Size(1594, 888);
            splitContainer_allwindow.SplitterDistance = 616;
            splitContainer_allwindow.TabIndex = 21;
            // 
            // splitContainer_up
            // 
            splitContainer_up.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer_up.Location = new Point(3, 3);
            splitContainer_up.Name = "splitContainer_up";
            // 
            // splitContainer_up.Panel1
            // 
            splitContainer_up.Panel1.Controls.Add(flowPanel_thumbnails);
            splitContainer_up.Panel1.Controls.Add(picEdge);
            splitContainer_up.Panel1.Controls.Add(label5);
            splitContainer_up.Panel1.Controls.Add(picImage);
            splitContainer_up.Panel1.Controls.Add(btn_openfolder);
            splitContainer_up.Panel1.Controls.Add(label4);
            splitContainer_up.Panel1.Controls.Add(btn_imgnext);
            splitContainer_up.Panel1.Controls.Add(btnPlay);
            splitContainer_up.Panel1.Controls.Add(btn_changquality);
            splitContainer_up.Panel1.Controls.Add(checkBox2);
            splitContainer_up.Panel1.Controls.Add(btn_before);
            splitContainer_up.Panel1.Controls.Add(panelTrackBarProgress);
            splitContainer_up.Panel1.Controls.Add(checkBox1);
            // 
            // splitContainer_up.Panel2
            // 
            splitContainer_up.Panel2.Controls.Add(tabControl_graph);
            splitContainer_up.Size = new Size(1587, 606);
            splitContainer_up.SplitterDistance = 846;
            splitContainer_up.TabIndex = 20;
            // 
            // flowPanel_thumbnails
            // 
            flowPanel_thumbnails.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flowPanel_thumbnails.AutoScroll = true;
            flowPanel_thumbnails.BackColor = Color.FromArgb(7, 7, 15);
            flowPanel_thumbnails.Location = new Point(2, 499);
            flowPanel_thumbnails.Name = "flowPanel_thumbnails";
            flowPanel_thumbnails.Size = new Size(828, 100);
            flowPanel_thumbnails.TabIndex = 20;
            flowPanel_thumbnails.WrapContents = false;
            // 
            // picEdge
            // 
            picEdge.BackColor = Color.White;
            picEdge.Location = new Point(463, 11);
            picEdge.Margin = new Padding(2);
            picEdge.Name = "picEdge";
            picEdge.Size = new Size(323, 341);
            picEdge.SizeMode = PictureBoxSizeMode.Zoom;
            picEdge.TabIndex = 12;
            picEdge.TabStop = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(250, 360);
            label5.Name = "label5";
            label5.Size = new Size(114, 25);
            label5.TabIndex = 19;
            label5.Text = "프레임 시작";
            // 
            // picImage
            // 
            picImage.BackColor = Color.White;
            picImage.Location = new Point(137, 11);
            picImage.Margin = new Padding(2);
            picImage.Name = "picImage";
            picImage.Size = new Size(322, 341);
            picImage.SizeMode = PictureBoxSizeMode.Zoom;
            picImage.TabIndex = 0;
            picImage.TabStop = false;
            // 
            // btn_openfolder
            // 
            btn_openfolder.BackColor = Color.Gray;
            btn_openfolder.FlatStyle = FlatStyle.Flat;
            btn_openfolder.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn_openfolder.Location = new Point(611, 446);
            btn_openfolder.Margin = new Padding(2);
            btn_openfolder.Name = "btn_openfolder";
            btn_openfolder.Size = new Size(129, 42);
            btn_openfolder.TabIndex = 2;
            btn_openfolder.Text = "폴더 열기";
            btn_openfolder.UseVisualStyleBackColor = false;
            btn_openfolder.Click += btn_openfolder_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(581, 361);
            label4.Name = "label4";
            label4.Size = new Size(95, 25);
            label4.TabIndex = 18;
            label4.Text = "프레임 끝";
            // 
            // btn_imgnext
            // 
            btn_imgnext.BackColor = Color.Gray;
            btn_imgnext.FlatStyle = FlatStyle.Flat;
            btn_imgnext.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn_imgnext.Location = new Point(294, 446);
            btn_imgnext.Margin = new Padding(2);
            btn_imgnext.Name = "btn_imgnext";
            btn_imgnext.Size = new Size(105, 42);
            btn_imgnext.TabIndex = 5;
            btn_imgnext.Text = "프레임 ▶";
            btn_imgnext.UseVisualStyleBackColor = false;
            btn_imgnext.Click += btn_imgnext_Click;
            // 
            // btnPlay
            // 
            btnPlay.BackColor = Color.Gray;
            btnPlay.FlatStyle = FlatStyle.Flat;
            btnPlay.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnPlay.Location = new Point(41, 390);
            btnPlay.Margin = new Padding(2);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(108, 34);
            btnPlay.TabIndex = 13;
            btnPlay.Text = "▶";
            btnPlay.UseVisualStyleBackColor = false;
            btnPlay.Click += btnPlay_Click;
            // 
            // btn_changquality
            // 
            btn_changquality.BackColor = Color.Gray;
            btn_changquality.FlatStyle = FlatStyle.Flat;
            btn_changquality.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn_changquality.Location = new Point(442, 446);
            btn_changquality.Margin = new Padding(2);
            btn_changquality.Name = "btn_changquality";
            btn_changquality.Size = new Size(154, 42);
            btn_changquality.TabIndex = 4;
            btn_changquality.Text = "프레임 화질 조정";
            btn_changquality.UseVisualStyleBackColor = false;
            btn_changquality.Click += btn_changquality_Click;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Font = new Font("맑은 고딕", 13F, FontStyle.Bold);
            checkBox2.ForeColor = SystemColors.ButtonFace;
            checkBox2.Location = new Point(20, 272);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(109, 29);
            checkBox2.TabIndex = 17;
            checkBox2.Text = "예측 경로";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // btn_before
            // 
            btn_before.BackColor = Color.Gray;
            btn_before.FlatStyle = FlatStyle.Flat;
            btn_before.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn_before.Location = new Point(173, 446);
            btn_before.Margin = new Padding(2);
            btn_before.Name = "btn_before";
            btn_before.Size = new Size(99, 42);
            btn_before.TabIndex = 4;
            btn_before.Text = "프레임 ◀";
            btn_before.UseVisualStyleBackColor = false;
            btn_before.Click += btn_before_Click;
            // 
            // panelTrackBarProgress
            // 
            panelTrackBarProgress.BackColor = Color.FromArgb(50, 50, 50);
            panelTrackBarProgress.Controls.Add(trackBar_frame);
            panelTrackBarProgress.Location = new Point(157, 393);
            panelTrackBarProgress.Name = "panelTrackBarProgress";
            panelTrackBarProgress.Size = new Size(616, 37);
            panelTrackBarProgress.TabIndex = 13;
            // 
            // trackBar_frame
            // 
            trackBar_frame.BackColor = Color.FromArgb(79, 195, 247);
            trackBar_frame.Dock = DockStyle.Left;
            trackBar_frame.Location = new Point(0, 0);
            trackBar_frame.Margin = new Padding(2);
            trackBar_frame.Name = "trackBar_frame";
            trackBar_frame.Size = new Size(614, 37);
            trackBar_frame.TabIndex = 1;
            trackBar_frame.Scroll += trackBar_frame_Scroll;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Font = new Font("맑은 고딕", 13F, FontStyle.Bold);
            checkBox1.ForeColor = SystemColors.ButtonFace;
            checkBox1.Location = new Point(20, 237);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(109, 29);
            checkBox1.TabIndex = 17;
            checkBox1.Text = "라인 표시";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // tabControl_graph
            // 
            tabControl_graph.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl_graph.Controls.Add(tabPage1);
            tabControl_graph.Controls.Add(tabPage2);
            tabControl_graph.Location = new Point(12, 2);
            tabControl_graph.Name = "tabControl_graph";
            tabControl_graph.SelectedIndex = 0;
            tabControl_graph.Size = new Size(729, 601);
            tabControl_graph.TabIndex = 17;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(picture_Gage);
            tabPage1.Controls.Add(label_angle);
            tabPage1.Controls.Add(picNeedleAngle);
            tabPage1.Controls.Add(label_throttle);
            tabPage1.Controls.Add(picNeedleSpeed);
            tabPage1.Location = new Point(4, 34);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(721, 563);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "계기판";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // picture_Gage
            // 
            picture_Gage.Image = Properties.Resources._5번;
            picture_Gage.Location = new Point(50, 83);
            picture_Gage.Margin = new Padding(2);
            picture_Gage.Name = "picture_Gage";
            picture_Gage.Size = new Size(531, 293);
            picture_Gage.SizeMode = PictureBoxSizeMode.StretchImage;
            picture_Gage.TabIndex = 8;
            picture_Gage.TabStop = false;
            // 
            // label_angle
            // 
            label_angle.AutoSize = true;
            label_angle.Location = new Point(387, 329);
            label_angle.Name = "label_angle";
            label_angle.Size = new Size(61, 25);
            label_angle.TabIndex = 16;
            label_angle.Text = "0.000";
            // 
            // picNeedleAngle
            // 
            picNeedleAngle.BackColor = Color.Transparent;
            picNeedleAngle.Location = new Point(378, 178);
            picNeedleAngle.Name = "picNeedleAngle";
            picNeedleAngle.Size = new Size(100, 100);
            picNeedleAngle.TabIndex = 14;
            picNeedleAngle.TabStop = false;
            // 
            // label_throttle
            // 
            label_throttle.AutoSize = true;
            label_throttle.Location = new Point(169, 330);
            label_throttle.Name = "label_throttle";
            label_throttle.Size = new Size(61, 25);
            label_throttle.TabIndex = 15;
            label_throttle.Text = "0.000";
            // 
            // picNeedleSpeed
            // 
            picNeedleSpeed.BackColor = Color.Transparent;
            picNeedleSpeed.Location = new Point(151, 178);
            picNeedleSpeed.Name = "picNeedleSpeed";
            picNeedleSpeed.Size = new Size(100, 100);
            picNeedleSpeed.TabIndex = 14;
            picNeedleSpeed.TabStop = false;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(checkBox_angle);
            tabPage2.Controls.Add(checkBox_throttle);
            tabPage2.Controls.Add(chart_thr_ang);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(721, 573);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "그래프";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // checkBox_angle
            // 
            checkBox_angle.AutoSize = true;
            checkBox_angle.Location = new Point(126, 11);
            checkBox_angle.Name = "checkBox_angle";
            checkBox_angle.Size = new Size(69, 29);
            checkBox_angle.TabIndex = 1;
            checkBox_angle.Text = "앵글";
            checkBox_angle.UseVisualStyleBackColor = true;
            // 
            // checkBox_throttle
            // 
            checkBox_throttle.AutoSize = true;
            checkBox_throttle.Location = new Point(26, 11);
            checkBox_throttle.Name = "checkBox_throttle";
            checkBox_throttle.Size = new Size(69, 29);
            checkBox_throttle.TabIndex = 1;
            checkBox_throttle.Text = "속도";
            checkBox_throttle.UseVisualStyleBackColor = true;
            // 
            // chart_thr_ang
            // 
            chart_thr_ang.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            chartArea1.Name = "ChartArea1";
            chart_thr_ang.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chart_thr_ang.Legends.Add(legend1);
            chart_thr_ang.Location = new Point(0, 46);
            chart_thr_ang.Name = "chart_thr_ang";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chart_thr_ang.Series.Add(series1);
            chart_thr_ang.Size = new Size(721, 524);
            chart_thr_ang.TabIndex = 0;
            chart_thr_ang.Text = "chart1";
            // 
            // panel1
            // 
            panel1.Controls.Add(label1);
            panel1.Controls.Add(btnSetEnd);
            panel1.Controls.Add(btnSetStart);
            panel1.Controls.Add(btn_delete);
            panel1.Controls.Add(listImages);
            panel1.Location = new Point(89, 39);
            panel1.Margin = new Padding(2);
            panel1.Name = "panel1";
            panel1.Size = new Size(510, 297);
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
            // btnSetEnd
            // 
            btnSetEnd.BackColor = Color.Gray;
            btnSetEnd.FlatStyle = FlatStyle.Flat;
            btnSetEnd.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnSetEnd.Location = new Point(349, 112);
            btnSetEnd.Margin = new Padding(2);
            btnSetEnd.Name = "btnSetEnd";
            btnSetEnd.Size = new Size(141, 39);
            btnSetEnd.TabIndex = 16;
            btnSetEnd.Text = "끝 프레임";
            btnSetEnd.UseVisualStyleBackColor = false;
            btnSetEnd.Click += btnSetEnd_Click;
            // 
            // btnSetStart
            // 
            btnSetStart.BackColor = Color.Gray;
            btnSetStart.FlatStyle = FlatStyle.Flat;
            btnSetStart.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnSetStart.Location = new Point(348, 58);
            btnSetStart.Margin = new Padding(2);
            btnSetStart.Name = "btnSetStart";
            btnSetStart.Size = new Size(142, 39);
            btnSetStart.TabIndex = 15;
            btnSetStart.Text = "시작 프레임\r\n";
            btnSetStart.UseVisualStyleBackColor = false;
            btnSetStart.Click += btnSetStart_Click;
            // 
            // btn_delete
            // 
            btn_delete.BackColor = Color.Gray;
            btn_delete.FlatStyle = FlatStyle.Flat;
            btn_delete.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn_delete.Location = new Point(349, 171);
            btn_delete.Margin = new Padding(2);
            btn_delete.Name = "btn_delete";
            btn_delete.Size = new Size(141, 39);
            btn_delete.TabIndex = 3;
            btn_delete.Text = "프레임 삭제";
            btn_delete.UseVisualStyleBackColor = false;
            btn_delete.Click += btn_delete_Click;
            // 
            // listImages
            // 
            listImages.BackColor = Color.FromArgb(22, 27, 3);
            listImages.BorderStyle = BorderStyle.FixedSingle;
            listImages.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point, 129);
            listImages.FormattingEnabled = true;
            listImages.Location = new Point(17, 36);
            listImages.Name = "listImages";
            listImages.Size = new Size(288, 242);
            listImages.TabIndex = 14;
            listImages.SelectedIndexChanged += listImages_SelectedIndexChanged;
            // 
            // panel2
            // 
            panel2.Controls.Add(label2);
            panel2.Controls.Add(btn_restore);
            panel2.Controls.Add(listBox1);
            panel2.Location = new Point(730, 33);
            panel2.Margin = new Padding(2);
            panel2.Name = "panel2";
            panel2.Size = new Size(618, 365);
            panel2.TabIndex = 15;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(23, 6);
            label2.Name = "label2";
            label2.Size = new Size(159, 25);
            label2.TabIndex = 18;
            label2.Text = "삭제 프레임 목록";
            // 
            // btn_restore
            // 
            btn_restore.BackColor = Color.Gray;
            btn_restore.FlatStyle = FlatStyle.Flat;
            btn_restore.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn_restore.Location = new Point(412, 45);
            btn_restore.Margin = new Padding(2);
            btn_restore.Name = "btn_restore";
            btn_restore.Size = new Size(142, 36);
            btn_restore.TabIndex = 17;
            btn_restore.Text = "프레임 복구";
            btn_restore.UseVisualStyleBackColor = false;
            btn_restore.Click += btn_restore_Click;
            // 
            // listBox1
            // 
            listBox1.BackColor = Color.FromArgb(22, 27, 3);
            listBox1.BorderStyle = BorderStyle.FixedSingle;
            listBox1.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point, 129);
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(17, 36);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(294, 317);
            listBox1.TabIndex = 14;
            // 
            // tab_train
            // 
            tab_train.BackColor = Color.FromArgb(64, 64, 64);
            tab_train.Controls.Add(progressBar1);
            tab_train.Controls.Add(listBox2);
            tab_train.Controls.Add(chart_loss);
            tab_train.Controls.Add(btn_stopTrain);
            tab_train.Controls.Add(btn_train);
            tab_train.Controls.Add(combo_model);
            tab_train.Controls.Add(list_log);
            tab_train.Location = new Point(4, 36);
            tab_train.Margin = new Padding(2);
            tab_train.Name = "tab_train";
            tab_train.Padding = new Padding(2);
            tab_train.Size = new Size(1588, 882);
            tab_train.TabIndex = 1;
            tab_train.Text = "학습";
            // 
            // progressBar1
            // 
            progressBar1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            progressBar1.Location = new Point(1013, 243);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(521, 45);
            progressBar1.TabIndex = 7;
            // 
            // listBox2
            // 
            listBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            listBox2.FormattingEnabled = true;
            listBox2.Location = new Point(1013, 322);
            listBox2.Name = "listBox2";
            listBox2.Size = new Size(521, 229);
            listBox2.TabIndex = 6;
            // 
            // chart_loss
            // 
            chartArea2.Name = "ChartArea1";
            chart_loss.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            chart_loss.Legends.Add(legend2);
            chart_loss.Location = new Point(20, 243);
            chart_loss.Margin = new Padding(2);
            chart_loss.Name = "chart_loss";
            chart_loss.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Excel;
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            chart_loss.Series.Add(series2);
            chart_loss.Size = new Size(938, 643);
            chart_loss.TabIndex = 4;
            chart_loss.Text = "chart1";
            chart_loss.Click += chart1_Click;
            // 
            // btn_stopTrain
            // 
            btn_stopTrain.BackColor = Color.Gray;
            btn_stopTrain.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn_stopTrain.Location = new Point(44, 139);
            btn_stopTrain.Margin = new Padding(2);
            btn_stopTrain.Name = "btn_stopTrain";
            btn_stopTrain.Size = new Size(118, 40);
            btn_stopTrain.TabIndex = 5;
            btn_stopTrain.Text = "학습 중단";
            btn_stopTrain.UseVisualStyleBackColor = false;
            // 
            // btn_train
            // 
            btn_train.BackColor = Color.Gray;
            btn_train.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn_train.Location = new Point(44, 86);
            btn_train.Margin = new Padding(2);
            btn_train.Name = "btn_train";
            btn_train.Size = new Size(118, 40);
            btn_train.TabIndex = 3;
            btn_train.Text = "학습";
            btn_train.UseVisualStyleBackColor = false;
            btn_train.Click += btn_train_Click;
            // 
            // combo_model
            // 
            combo_model.FormattingEnabled = true;
            combo_model.Location = new Point(44, 32);
            combo_model.Margin = new Padding(2);
            combo_model.Name = "combo_model";
            combo_model.Size = new Size(118, 23);
            combo_model.TabIndex = 2;
            // 
            // list_log
            // 
            list_log.FormattingEnabled = true;
            list_log.Location = new Point(222, 32);
            list_log.Margin = new Padding(2);
            list_log.Name = "list_log";
            list_log.Size = new Size(1338, 184);
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
            ClientSize = new Size(1595, 915);
            Controls.Add(tabControl);
            Margin = new Padding(2);
            Name = "Form1";
            Text = "데이터 매니저v1.0";
            tabControl.ResumeLayout(false);
            tab_data.ResumeLayout(false);
            splitContainer_allwindow.Panel1.ResumeLayout(false);
            splitContainer_allwindow.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer_allwindow).EndInit();
            splitContainer_allwindow.ResumeLayout(false);
            splitContainer_up.Panel1.ResumeLayout(false);
            splitContainer_up.Panel1.PerformLayout();
            splitContainer_up.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer_up).EndInit();
            splitContainer_up.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picEdge).EndInit();
            ((System.ComponentModel.ISupportInitialize)picImage).EndInit();
            panelTrackBarProgress.ResumeLayout(false);
            panelTrackBarProgress.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar_frame).EndInit();
            tabControl_graph.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picture_Gage).EndInit();
            ((System.ComponentModel.ISupportInitialize)picNeedleAngle).EndInit();
            ((System.ComponentModel.ISupportInitialize)picNeedleSpeed).EndInit();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)chart_thr_ang).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            tab_train.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chart_loss).EndInit();
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
        private Button btn_train;
        private ComboBox combo_model;
        private ListBox list_log;
        private Button btn_stopTrain;
        private PictureBox picture_Gage;
        private Panel panel1;
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
        private PictureBox picNeedleAngle;
        private PictureBox picNeedleSpeed;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_loss;
        private Panel panel2;
        private Label label2;
        private ListBox listBox1;
        private CheckBox checkBox2;
        private CheckBox checkBox1;
        private Label label5;
        private Label label4;
        private ProgressBar progressBar1;
        private ListBox listBox2;
        private SplitContainer splitContainer_up;
        private SplitContainer splitContainer_allwindow;
        private Label label_angle;
        private Label label_throttle;
        private TabControl tabControl_graph;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_thr_ang;
        private CheckBox checkBox_angle;
        private CheckBox checkBox_throttle;
        private FlowLayoutPanel flowPanel_thumbnails;
    }
}
