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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            tabControl = new TabControl();
            tab_data = new TabPage();
            splitContainer_allwindow = new SplitContainer();
            splitContainer_up = new SplitContainer();
            lblTotalFrame = new Label();
            panel1 = new Panel();
            checkBox3 = new CheckBox();
            label1 = new Label();
            btnSetEnd = new Button();
            btnSetStart = new Button();
            btn_delete = new Button();
            listImages = new ListBox();
            lblCurrentFrame = new Label();
            flowPanel_thumbnails = new FlowLayoutPanel();
            picEdge = new PictureBox();
            picImage = new PictureBox();
            btn_openfolder = new Button();
            btn_imgnext = new Button();
            btnPlay = new Button();
            btn_changquality = new Button();
            checkBox2 = new CheckBox();
            btn_before = new Button();
            panelTrackBarProgress = new Panel();
            trackBar_frame = new TrackBar();
            checkBox_filter = new CheckBox();
            checkBox1 = new CheckBox();
            split_graph = new SplitContainer();
            picture_Gage = new PictureBox();
            label_throttle = new Label();
            label_angle = new Label();
            picNeedleSpeed = new PictureBox();
            picNeedleAngle = new PictureBox();
            checkBox_angle = new CheckBox();
            checkBox_throttle = new CheckBox();
            chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            splitContainer2 = new SplitContainer();
            label2 = new Label();
            listBox1 = new ListBox();
            lblTrash = new Label();
            btn_restore = new Button();
            cmbTrashList = new ComboBox();
            tab_train = new TabPage();
            splitContainer1 = new SplitContainer();
            combo_model = new ComboBox();
            progressBar_learn = new ProgressBar();
            btn_train = new Button();
            list_log = new ListBox();
            btn_stopTrain = new Button();
            chart_loss = new System.Windows.Forms.DataVisualization.Charting.Chart();
            timer1 = new System.Windows.Forms.Timer(components);
            progressDelete = new ProgressBar();
            lblDelete = new Label();
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
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picEdge).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picImage).BeginInit();
            panelTrackBarProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar_frame).BeginInit();
            ((System.ComponentModel.ISupportInitialize)split_graph).BeginInit();
            split_graph.Panel1.SuspendLayout();
            split_graph.Panel2.SuspendLayout();
            split_graph.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picture_Gage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picNeedleSpeed).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picNeedleAngle).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chart1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            tab_train.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
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
            tabControl.Size = new Size(1590, 922);
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
            tab_data.Size = new Size(1582, 882);
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
            splitContainer_allwindow.Panel2.Controls.Add(splitContainer2);
            splitContainer_allwindow.Panel2.Controls.Add(panel1);
            splitContainer_allwindow.Panel2.ForeColor = Color.White;
            splitContainer_allwindow.Size = new Size(1588, 888);
            splitContainer_allwindow.SplitterDistance = 615;
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
            splitContainer_up.Panel1.Controls.Add(lblTotalFrame);
            splitContainer_up.Panel1.Controls.Add(lblCurrentFrame);
            splitContainer_up.Panel1.Controls.Add(flowPanel_thumbnails);
            splitContainer_up.Panel1.Controls.Add(picEdge);
            splitContainer_up.Panel1.Controls.Add(picImage);
            splitContainer_up.Panel1.Controls.Add(btn_openfolder);
            splitContainer_up.Panel1.Controls.Add(btn_imgnext);
            splitContainer_up.Panel1.Controls.Add(btnPlay);
            splitContainer_up.Panel1.Controls.Add(btn_changquality);
            splitContainer_up.Panel1.Controls.Add(checkBox2);
            splitContainer_up.Panel1.Controls.Add(btn_before);
            splitContainer_up.Panel1.Controls.Add(panelTrackBarProgress);
            splitContainer_up.Panel1.Controls.Add(checkBox_filter);
            splitContainer_up.Panel1.Controls.Add(checkBox1);
            // 
            // splitContainer_up.Panel2
            // 
            splitContainer_up.Panel2.Controls.Add(split_graph);
            splitContainer_up.Size = new Size(1581, 605);
            splitContainer_up.SplitterDistance = 987;
            splitContainer_up.TabIndex = 20;
            // 
            // lblTotalFrame
            // 
            lblTotalFrame.AutoSize = true;
            lblTotalFrame.BackColor = Color.Gray;
            lblTotalFrame.ForeColor = Color.Black;
            lblTotalFrame.Location = new Point(791, 248);
            lblTotalFrame.Margin = new Padding(2, 0, 2, 0);
            lblTotalFrame.Name = "lblTotalFrame";
            lblTotalFrame.Size = new Size(119, 25);
            lblTotalFrame.TabIndex = 21;
            lblTotalFrame.Text = "전체 프레임:";
            // 
            // panel1
            // 
            panel1.Controls.Add(lblDelete);
            panel1.Controls.Add(progressDelete);
            panel1.Controls.Add(checkBox3);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(btnSetEnd);
            panel1.Controls.Add(btnSetStart);
            panel1.Controls.Add(btn_delete);
            panel1.Controls.Add(listImages);
            panel1.ForeColor = Color.Black;
            panel1.Location = new Point(104, 40);
            panel1.Margin = new Padding(2);
            panel1.Name = "panel1";
            panel1.Size = new Size(556, 494);
            panel1.TabIndex = 11;
            panel1.Paint += panel1_Paint;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.BackColor = Color.Gray;
            checkBox3.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            checkBox3.Location = new Point(322, 217);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(137, 25);
            checkBox3.TabIndex = 21;
            checkBox3.Text = "이상 탐지 기능";
            checkBox3.UseVisualStyleBackColor = false;
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
            btnSetEnd.Location = new Point(322, 94);
            btnSetEnd.Margin = new Padding(2);
            btnSetEnd.Name = "btnSetEnd";
            btnSetEnd.Size = new Size(140, 40);
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
            btnSetStart.Location = new Point(322, 36);
            btnSetStart.Margin = new Padding(2);
            btnSetStart.Name = "btnSetStart";
            btnSetStart.Size = new Size(140, 40);
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
            btn_delete.Location = new Point(322, 159);
            btn_delete.Margin = new Padding(2);
            btn_delete.Name = "btn_delete";
            btn_delete.Size = new Size(140, 40);
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
            listImages.Size = new Size(288, 362);
            listImages.TabIndex = 14;
            listImages.SelectedIndexChanged += listImages_SelectedIndexChanged;
            // 
            // lblCurrentFrame
            // 
            lblCurrentFrame.AutoSize = true;
            lblCurrentFrame.BackColor = Color.Gray;
            lblCurrentFrame.ForeColor = Color.Black;
            lblCurrentFrame.Location = new Point(791, 155);
            lblCurrentFrame.Margin = new Padding(2, 0, 2, 0);
            lblCurrentFrame.Name = "lblCurrentFrame";
            lblCurrentFrame.Size = new Size(119, 25);
            lblCurrentFrame.TabIndex = 19;
            lblCurrentFrame.Text = "현재 프레임:";
            // 
            // flowPanel_thumbnails
            // 
            flowPanel_thumbnails.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flowPanel_thumbnails.AutoScroll = true;
            flowPanel_thumbnails.BackColor = Color.FromArgb(7, 7, 15);
            flowPanel_thumbnails.Location = new Point(2, 499);
            flowPanel_thumbnails.Name = "flowPanel_thumbnails";
            flowPanel_thumbnails.Size = new Size(969, 99);
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
            checkBox2.Location = new Point(791, 82);
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
            panelTrackBarProgress.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelTrackBarProgress.BackColor = Color.FromArgb(50, 50, 50);
            panelTrackBarProgress.Controls.Add(trackBar_frame);
            panelTrackBarProgress.Location = new Point(157, 393);
            panelTrackBarProgress.Name = "panelTrackBarProgress";
            panelTrackBarProgress.Size = new Size(611, 37);
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
            // checkBox_filter
            // 
            checkBox_filter.AutoSize = true;
            checkBox_filter.Font = new Font("맑은 고딕", 13F, FontStyle.Bold);
            checkBox_filter.ForeColor = SystemColors.ButtonFace;
            checkBox_filter.Location = new Point(791, 11);
            checkBox_filter.Name = "checkBox_filter";
            checkBox_filter.Size = new Size(85, 29);
            checkBox_filter.TabIndex = 17;
            checkBox_filter.Text = "필터링";
            checkBox_filter.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Font = new Font("맑은 고딕", 13F, FontStyle.Bold);
            checkBox1.ForeColor = SystemColors.ButtonFace;
            checkBox1.Location = new Point(791, 46);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(109, 29);
            checkBox1.TabIndex = 17;
            checkBox1.Text = "라인 표시";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // split_graph
            // 
            split_graph.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            split_graph.Location = new Point(3, 0);
            split_graph.Name = "split_graph";
            split_graph.Orientation = Orientation.Horizontal;
            // 
            // split_graph.Panel1
            // 
            split_graph.Panel1.Controls.Add(picture_Gage);
            split_graph.Panel1.Controls.Add(label_throttle);
            split_graph.Panel1.Controls.Add(label_angle);
            split_graph.Panel1.Controls.Add(picNeedleSpeed);
            split_graph.Panel1.Controls.Add(picNeedleAngle);
            // 
            // split_graph.Panel2
            // 
            split_graph.Panel2.Controls.Add(checkBox_angle);
            split_graph.Panel2.Controls.Add(checkBox_throttle);
            split_graph.Panel2.Controls.Add(chart1);
            split_graph.Size = new Size(583, 605);
            split_graph.SplitterDistance = 295;
            split_graph.TabIndex = 17;
            // 
            // picture_Gage
            // 
            picture_Gage.Image = Properties.Resources.최종;
            picture_Gage.Location = new Point(16, -1);
            picture_Gage.Margin = new Padding(2);
            picture_Gage.Name = "picture_Gage";
            picture_Gage.Size = new Size(531, 293);
            picture_Gage.SizeMode = PictureBoxSizeMode.StretchImage;
            picture_Gage.TabIndex = 8;
            picture_Gage.TabStop = false;
            // 
            // label_throttle
            // 
            label_throttle.AutoSize = true;
            label_throttle.Location = new Point(146, 249);
            label_throttle.Name = "label_throttle";
            label_throttle.Size = new Size(61, 25);
            label_throttle.TabIndex = 15;
            label_throttle.Text = "0.000";
            // 
            // label_angle
            // 
            label_angle.AutoSize = true;
            label_angle.Location = new Point(364, 248);
            label_angle.Name = "label_angle";
            label_angle.Size = new Size(61, 25);
            label_angle.TabIndex = 16;
            label_angle.Text = "0.000";
            // 
            // picNeedleSpeed
            // 
            picNeedleSpeed.BackColor = Color.Transparent;
            picNeedleSpeed.Location = new Point(128, 97);
            picNeedleSpeed.Name = "picNeedleSpeed";
            picNeedleSpeed.Size = new Size(100, 100);
            picNeedleSpeed.TabIndex = 14;
            picNeedleSpeed.TabStop = false;
            // 
            // picNeedleAngle
            // 
            picNeedleAngle.BackColor = Color.Transparent;
            picNeedleAngle.Location = new Point(355, 97);
            picNeedleAngle.Name = "picNeedleAngle";
            picNeedleAngle.Size = new Size(100, 100);
            picNeedleAngle.TabIndex = 14;
            picNeedleAngle.TabStop = false;
            // 
            // checkBox_angle
            // 
            checkBox_angle.AutoSize = true;
            checkBox_angle.ForeColor = SystemColors.ButtonHighlight;
            checkBox_angle.Location = new Point(116, 19);
            checkBox_angle.Name = "checkBox_angle";
            checkBox_angle.Size = new Size(69, 29);
            checkBox_angle.TabIndex = 1;
            checkBox_angle.Text = "앵글";
            checkBox_angle.UseVisualStyleBackColor = true;
            // 
            // checkBox_throttle
            // 
            checkBox_throttle.AutoSize = true;
            checkBox_throttle.ForeColor = SystemColors.ButtonHighlight;
            checkBox_throttle.Location = new Point(16, 19);
            checkBox_throttle.Name = "checkBox_throttle";
            checkBox_throttle.Size = new Size(69, 29);
            checkBox_throttle.TabIndex = 1;
            checkBox_throttle.Text = "속도";
            checkBox_throttle.UseVisualStyleBackColor = true;
            // 
            // chart1
            // 
            chart1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            chartArea3.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            chart1.Legends.Add(legend3);
            chart1.Location = new Point(0, 59);
            chart1.Name = "chart1";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            chart1.Series.Add(series3);
            chart1.Size = new Size(577, 243);
            chart1.TabIndex = 2;
            chart1.Text = "chart1";
            // 
            // splitContainer2
            // 
            splitContainer2.Location = new Point(774, 40);
            splitContainer2.Margin = new Padding(5);
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(label2);
            splitContainer2.Panel1.Controls.Add(listBox1);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(lblTrash);
            splitContainer2.Panel2.Controls.Add(btn_restore);
            splitContainer2.Panel2.Controls.Add(cmbTrashList);
            splitContainer2.Size = new Size(699, 581);
            splitContainer2.SplitterDistance = 281;
            splitContainer2.SplitterWidth = 6;
            splitContainer2.TabIndex = 12;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(15, 11);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(159, 25);
            label2.TabIndex = 18;
            label2.Text = "삭제 프레임 목록";
            // 
            // listBox1
            // 
            listBox1.BackColor = Color.FromArgb(22, 27, 3);
            listBox1.BorderStyle = BorderStyle.FixedSingle;
            listBox1.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point, 129);
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(15, 55);
            listBox1.Margin = new Padding(4);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(246, 572);
            listBox1.TabIndex = 14;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // lblTrash
            // 
            lblTrash.AutoSize = true;
            lblTrash.BackColor = Color.Gray;
            lblTrash.ForeColor = Color.Black;
            lblTrash.Location = new Point(274, 34);
            lblTrash.Name = "lblTrash";
            lblTrash.Size = new Size(69, 25);
            lblTrash.TabIndex = 23;
            lblTrash.Text = "휴지통";
            // 
            // btn_restore
            // 
            btn_restore.BackColor = Color.Gray;
            btn_restore.FlatStyle = FlatStyle.Flat;
            btn_restore.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn_restore.ForeColor = Color.Black;
            btn_restore.Location = new Point(19, 55);
            btn_restore.Name = "btn_restore";
            btn_restore.Size = new Size(183, 48);
            btn_restore.TabIndex = 17;
            btn_restore.Text = "프레임 복구";
            btn_restore.UseVisualStyleBackColor = false;
            btn_restore.Click += btn_restore_Click;
            // 
            // cmbTrashList
            // 
            cmbTrashList.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTrashList.FormattingEnabled = true;
            cmbTrashList.Location = new Point(234, 62);
            cmbTrashList.Name = "cmbTrashList";
            cmbTrashList.Size = new Size(147, 33);
            cmbTrashList.TabIndex = 22;
            // 
            // tab_train
            // 
            tab_train.BackColor = Color.FromArgb(64, 64, 64);
            tab_train.Controls.Add(splitContainer1);
            tab_train.Location = new Point(4, 36);
            tab_train.Margin = new Padding(2);
            tab_train.Name = "tab_train";
            tab_train.Padding = new Padding(2);
            tab_train.Size = new Size(1582, 882);
            tab_train.TabIndex = 1;
            tab_train.Text = "학습";
            // 
            // splitContainer1
            // 
            splitContainer1.Location = new Point(-4, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(combo_model);
            splitContainer1.Panel1.Controls.Add(progressBar_learn);
            splitContainer1.Panel1.Controls.Add(btn_train);
            splitContainer1.Panel1.Controls.Add(list_log);
            splitContainer1.Panel1.Controls.Add(btn_stopTrain);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(chart_loss);
            splitContainer1.Size = new Size(1592, 886);
            splitContainer1.SplitterDistance = 561;
            splitContainer1.TabIndex = 8;
            // 
            // combo_model
            // 
            combo_model.FormattingEnabled = true;
            combo_model.Location = new Point(28, 101);
            combo_model.Margin = new Padding(2);
            combo_model.Name = "combo_model";
            combo_model.Size = new Size(317, 23);
            combo_model.TabIndex = 2;
            // 
            // progressBar_learn
            // 
            progressBar_learn.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            progressBar_learn.Location = new Point(14, 212);
            progressBar_learn.Name = "progressBar_learn";
            progressBar_learn.Size = new Size(537, 52);
            progressBar_learn.TabIndex = 7;
            // 
            // btn_train
            // 
            btn_train.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btn_train.BackColor = Color.Gray;
            btn_train.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn_train.Location = new Point(374, 56);
            btn_train.Margin = new Padding(2);
            btn_train.Name = "btn_train";
            btn_train.Size = new Size(136, 40);
            btn_train.TabIndex = 3;
            btn_train.Text = "학습";
            btn_train.UseVisualStyleBackColor = false;
            btn_train.Click += btn_train_Click;
            // 
            // list_log
            // 
            list_log.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            list_log.FormattingEnabled = true;
            list_log.Location = new Point(14, 280);
            list_log.Margin = new Padding(2);
            list_log.Name = "list_log";
            list_log.Size = new Size(537, 589);
            list_log.TabIndex = 1;
            // 
            // btn_stopTrain
            // 
            btn_stopTrain.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btn_stopTrain.BackColor = Color.Gray;
            btn_stopTrain.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn_stopTrain.Location = new Point(374, 132);
            btn_stopTrain.Margin = new Padding(2);
            btn_stopTrain.Name = "btn_stopTrain";
            btn_stopTrain.Size = new Size(136, 40);
            btn_stopTrain.TabIndex = 5;
            btn_stopTrain.Text = "학습 중단";
            btn_stopTrain.UseVisualStyleBackColor = false;
            // 
            // chart_loss
            // 
            chart_loss.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            chartArea4.Name = "ChartArea1";
            chart_loss.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            chart_loss.Legends.Add(legend4);
            chart_loss.Location = new Point(2, 4);
            chart_loss.Margin = new Padding(2);
            chart_loss.Name = "chart_loss";
            chart_loss.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Excel;
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            chart_loss.Series.Add(series4);
            chart_loss.Size = new Size(1028, 882);
            chart_loss.TabIndex = 4;
            chart_loss.Text = "chart1";
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // progressDelete
            // 
            progressDelete.Location = new Point(148, 419);
            progressDelete.Name = "progressDelete";
            progressDelete.Size = new Size(389, 23);
            progressDelete.TabIndex = 24;
            progressDelete.Click += progressBar1_Click;
            // 
            // lblDelete
            // 
            lblDelete.AutoSize = true;
            lblDelete.BackColor = Color.Gray;
            lblDelete.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblDelete.Location = new Point(17, 417);
            lblDelete.Name = "lblDelete";
            lblDelete.Size = new Size(96, 21);
            lblDelete.TabIndex = 25;
            lblDelete.Text = "삭제 진행률";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1490, 791);
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
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picEdge).EndInit();
            ((System.ComponentModel.ISupportInitialize)picImage).EndInit();
            panelTrackBarProgress.ResumeLayout(false);
            panelTrackBarProgress.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar_frame).EndInit();
            split_graph.Panel1.ResumeLayout(false);
            split_graph.Panel1.PerformLayout();
            split_graph.Panel2.ResumeLayout(false);
            split_graph.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)split_graph).EndInit();
            split_graph.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picture_Gage).EndInit();
            ((System.ComponentModel.ISupportInitialize)picNeedleSpeed).EndInit();
            ((System.ComponentModel.ISupportInitialize)picNeedleAngle).EndInit();
            ((System.ComponentModel.ISupportInitialize)chart1).EndInit();
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel1.PerformLayout();
            splitContainer2.Panel2.ResumeLayout(false);
            splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            tab_train.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
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
        private Label label2;
        private ListBox listBox1;
        private CheckBox checkBox2;
        private CheckBox checkBox1;
        private ProgressBar progressBar_learn;
        private SplitContainer splitContainer_up;
        private SplitContainer splitContainer_allwindow;
        private Label label_angle;
        private Label label_throttle;
        private CheckBox checkBox_angle;
        private CheckBox checkBox_throttle;
        private FlowLayoutPanel flowPanel_thumbnails;
        private SplitContainer split_graph;
        private CheckBox checkBox_filter;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private SplitContainer splitContainer1;
        private Label lblCurrentFrame;
        private Label lblTotalFrame;
        private SplitContainer splitContainer2;
        private ComboBox cmbTrashList;
        private Label lblTrash;
        private CheckBox checkBox3;
        private ProgressBar progressDelete;
        private Label lblDelete;
    }
}
