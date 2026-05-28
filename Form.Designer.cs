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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea7 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend7 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            tabControl = new TabControl();
            tab_data = new TabPage();
            checkBox2 = new CheckBox();
            checkBox1 = new CheckBox();
            label3 = new Label();
            panel2 = new Panel();
            label2 = new Label();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            listBox1 = new ListBox();
            picNeedleAngle = new PictureBox();
            picNeedleSpeed = new PictureBox();
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
            chart_loss = new System.Windows.Forms.DataVisualization.Charting.Chart();
            btn_stopTrain = new Button();
            btn_train = new Button();
            combo_model = new ComboBox();
            list_log = new ListBox();
            timer1 = new System.Windows.Forms.Timer(components);
            label4 = new Label();
            label5 = new Label();
            tabControl.SuspendLayout();
            tab_data.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picNeedleAngle).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picNeedleSpeed).BeginInit();
            panelTrackBarProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar_frame).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picEdge).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picture_Gage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picImage).BeginInit();
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
            tabControl.Size = new Size(1322, 967);
            tabControl.TabIndex = 0;
            // 
            // tab_data
            // 
            tab_data.BackColor = Color.FromArgb(64, 64, 64);
            tab_data.Controls.Add(label5);
            tab_data.Controls.Add(label4);
            tab_data.Controls.Add(checkBox2);
            tab_data.Controls.Add(checkBox1);
            tab_data.Controls.Add(label3);
            tab_data.Controls.Add(panel2);
            tab_data.Controls.Add(picNeedleAngle);
            tab_data.Controls.Add(picNeedleSpeed);
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
            tab_data.Size = new Size(1314, 927);
            tab_data.TabIndex = 0;
            tab_data.Text = "데이터 매니저";
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(36, 539);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(127, 29);
            checkBox2.TabIndex = 17;
            checkBox2.Text = "checkBox1";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(36, 497);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(127, 29);
            checkBox1.TabIndex = 17;
            checkBox1.Text = "checkBox1";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(289, 520);
            label3.Name = "label3";
            label3.Size = new Size(88, 25);
            label3.TabIndex = 16;
            label3.Text = "썸네ㅣ일";
            // 
            // panel2
            // 
            panel2.Controls.Add(label2);
            panel2.Controls.Add(button1);
            panel2.Controls.Add(button2);
            panel2.Controls.Add(button3);
            panel2.Controls.Add(button4);
            panel2.Controls.Add(listBox1);
            panel2.Location = new Point(687, 497);
            panel2.Margin = new Padding(2);
            panel2.Name = "panel2";
            panel2.Size = new Size(618, 402);
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
            // button1
            // 
            button1.BackColor = Color.Gray;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            button1.Location = new Point(375, 211);
            button1.Margin = new Padding(2);
            button1.Name = "button1";
            button1.Size = new Size(142, 36);
            button1.TabIndex = 17;
            button1.Text = "프레임 복구";
            button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            button2.BackColor = Color.Gray;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            button2.Location = new Point(376, 151);
            button2.Margin = new Padding(2);
            button2.Name = "button2";
            button2.Size = new Size(141, 39);
            button2.TabIndex = 16;
            button2.Text = "끝 프레임";
            button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            button3.BackColor = Color.Gray;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            button3.Location = new Point(375, 84);
            button3.Margin = new Padding(2);
            button3.Name = "button3";
            button3.Size = new Size(142, 39);
            button3.TabIndex = 15;
            button3.Text = "시작 프레임\r\n";
            button3.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            button4.BackColor = Color.Gray;
            button4.FlatStyle = FlatStyle.Flat;
            button4.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            button4.Location = new Point(375, 284);
            button4.Margin = new Padding(2);
            button4.Name = "button4";
            button4.Size = new Size(141, 39);
            button4.TabIndex = 3;
            button4.Text = "프레임 삭제";
            button4.UseVisualStyleBackColor = false;
            // 
            // listBox1
            // 
            listBox1.BackColor = Color.FromArgb(22, 27, 3);
            listBox1.BorderStyle = BorderStyle.FixedSingle;
            listBox1.Font = new Font("맑은 고딕", 9F, FontStyle.Bold, GraphicsUnit.Point, 129);
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(17, 36);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(337, 347);
            listBox1.TabIndex = 14;
            // 
            // picNeedleAngle
            // 
            picNeedleAngle.BackColor = Color.Transparent;
            picNeedleAngle.Location = new Point(1029, 123);
            picNeedleAngle.Name = "picNeedleAngle";
            picNeedleAngle.Size = new Size(100, 100);
            picNeedleAngle.TabIndex = 14;
            picNeedleAngle.TabStop = false;
            // 
            // picNeedleSpeed
            // 
            picNeedleSpeed.BackColor = Color.Transparent;
            picNeedleSpeed.Location = new Point(802, 123);
            picNeedleSpeed.Name = "picNeedleSpeed";
            picNeedleSpeed.Size = new Size(100, 100);
            picNeedleSpeed.TabIndex = 14;
            picNeedleSpeed.TabStop = false;
            // 
            // panelTrackBarProgress
            // 
            panelTrackBarProgress.BackColor = Color.FromArgb(50, 50, 50);
            panelTrackBarProgress.Controls.Add(trackBar_frame);
            panelTrackBarProgress.Location = new Point(148, 454);
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
            picEdge.Location = new Point(357, 29);
            picEdge.Margin = new Padding(2);
            picEdge.Name = "picEdge";
            picEdge.Size = new Size(323, 394);
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
            panel1.Location = new Point(42, 630);
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
            btn_restore.Click += btn_restore_Click;
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
            btnSetEnd.Click += btnSetEnd_Click;
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
            btnSetStart.Click += btnSetStart_Click;
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
            listImages.Size = new Size(481, 272);
            listImages.TabIndex = 14;
            listImages.SelectedIndexChanged += listImages_SelectedIndexChanged;
            // 
            // btn_openfolder
            // 
            btn_openfolder.BackColor = Color.Gray;
            btn_openfolder.FlatStyle = FlatStyle.Flat;
            btn_openfolder.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn_openfolder.Location = new Point(483, 570);
            btn_openfolder.Margin = new Padding(2);
            btn_openfolder.Name = "btn_openfolder";
            btn_openfolder.Size = new Size(129, 50);
            btn_openfolder.TabIndex = 2;
            btn_openfolder.Text = "폴더 열기";
            btn_openfolder.UseVisualStyleBackColor = false;
            btn_openfolder.Click += btn_openfolder_Click;
            // 
            // text_throttle
            // 
            text_throttle.BackColor = Color.FromArgb(13, 13, 24);
            text_throttle.BorderStyle = BorderStyle.None;
            text_throttle.Font = new Font("맑은 고딕", 20.25F, FontStyle.Bold);
            text_throttle.Location = new Point(820, 273);
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
            btnPlay.Location = new Point(36, 448);
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
            btn_imgnext.Location = new Point(166, 570);
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
            btn_changquality.Location = new Point(314, 570);
            btn_changquality.Margin = new Padding(2);
            btn_changquality.Name = "btn_changquality";
            btn_changquality.Size = new Size(154, 50);
            btn_changquality.TabIndex = 4;
            btn_changquality.Text = "프레임 화질 조정";
            btn_changquality.UseVisualStyleBackColor = false;
            btn_changquality.Click += btn_changquality_Click;
            // 
            // btn_before
            // 
            btn_before.BackColor = Color.Gray;
            btn_before.FlatStyle = FlatStyle.Flat;
            btn_before.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn_before.Location = new Point(45, 570);
            btn_before.Margin = new Padding(2);
            btn_before.Name = "btn_before";
            btn_before.Size = new Size(99, 50);
            btn_before.TabIndex = 4;
            btn_before.Text = "프레임 ◀";
            btn_before.UseVisualStyleBackColor = false;
            btn_before.Click += btn_before_Click;
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
            // 
            // tab_train
            // 
            tab_train.BackColor = Color.FromArgb(64, 64, 64);
            tab_train.Controls.Add(chart_loss);
            tab_train.Controls.Add(btn_stopTrain);
            tab_train.Controls.Add(btn_train);
            tab_train.Controls.Add(combo_model);
            tab_train.Controls.Add(list_log);
            tab_train.Location = new Point(4, 36);
            tab_train.Margin = new Padding(2);
            tab_train.Name = "tab_train";
            tab_train.Padding = new Padding(2);
            tab_train.Size = new Size(1314, 927);
            tab_train.TabIndex = 1;
            tab_train.Text = "학습";
            // 
            // chart_loss
            // 
            chart_loss.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            chartArea7.Name = "ChartArea1";
            chart_loss.ChartAreas.Add(chartArea7);
            legend7.Name = "Legend1";
            chart_loss.Legends.Add(legend7);
            chart_loss.Location = new Point(44, 349);
            chart_loss.Margin = new Padding(2);
            chart_loss.Name = "chart_loss";
            chart_loss.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Excel;
            series7.ChartArea = "ChartArea1";
            series7.Legend = "Legend1";
            series7.Name = "Series1";
            chart_loss.Series.Add(series7);
            chart_loss.Size = new Size(1109, 551);
            chart_loss.TabIndex = 4;
            chart_loss.Text = "chart1";
            chart_loss.Click += chart1_Click;
            // 
            // btn_stopTrain
            // 
            btn_stopTrain.BackColor = Color.Gray;
            btn_stopTrain.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn_stopTrain.Location = new Point(319, 21);
            btn_stopTrain.Margin = new Padding(2);
            btn_stopTrain.Name = "btn_stopTrain";
            btn_stopTrain.Size = new Size(101, 34);
            btn_stopTrain.TabIndex = 5;
            btn_stopTrain.Text = "학습 중단";
            btn_stopTrain.UseVisualStyleBackColor = false;
            // 
            // btn_train
            // 
            btn_train.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btn_train.BackColor = Color.Gray;
            btn_train.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btn_train.Location = new Point(192, 23);
            btn_train.Margin = new Padding(2);
            btn_train.Name = "btn_train";
            btn_train.Size = new Size(98, 34);
            btn_train.TabIndex = 3;
            btn_train.Text = "학습";
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
            list_log.Size = new Size(1109, 259);
            list_log.TabIndex = 1;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(487, 425);
            label4.Name = "label4";
            label4.Size = new Size(95, 25);
            label4.TabIndex = 18;
            label4.Text = "프레임 끝";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(182, 427);
            label5.Name = "label5";
            label5.Size = new Size(114, 25);
            label5.TabIndex = 19;
            label5.Text = "프레임 시작";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1321, 960);
            Controls.Add(tabControl);
            Margin = new Padding(2);
            Name = "Form1";
            Text = "데이터 매니저v1.0";
            tabControl.ResumeLayout(false);
            tab_data.ResumeLayout(false);
            tab_data.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picNeedleAngle).EndInit();
            ((System.ComponentModel.ISupportInitialize)picNeedleSpeed).EndInit();
            panelTrackBarProgress.ResumeLayout(false);
            panelTrackBarProgress.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar_frame).EndInit();
            ((System.ComponentModel.ISupportInitialize)picEdge).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picture_Gage).EndInit();
            ((System.ComponentModel.ISupportInitialize)picImage).EndInit();
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
        private TextBox text_throttle;
        private TextBox text_angle;
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
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private ListBox listBox1;
        private Label label3;
        private CheckBox checkBox2;
        private CheckBox checkBox1;
        private Label label5;
        private Label label4;
    }
}
