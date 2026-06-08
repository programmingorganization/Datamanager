using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Datamanager
{
    public partial class Form1 : Form
    {
        string baseDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."));   //  프로젝트 루트 경로
        private Process trainProcess = null;
        bool userScrollingLog = false;
        System.Windows.Forms.Timer scrollResumeTimer;
        int savedTopIndex = -1;

        string[] imageFiles;
        int currentIndex = 0;

        private List<PredictionRecord> predictions =
            new List<PredictionRecord>();

        private int currentFrame = 0;   //  파일럿 아레나에서 현재 보고 있는 프레임 인덱스

        string currentFolderPath = "";  // 현재 이미지 폴더 경로
        string trashFolderPath = "";    // 삭제된 이미지 보관 폴더 경로
        Stack<string> deletedFiles = new Stack<string>();   // 삭제된 파일 순서를 저장하는 스택
        List<string> deletedHistory = new List<string>();
        string backupFolderPath;    // 압축 전 원본 이미지를 보관하는 폴더 경로

        private string pythonPath;

        private bool isPlaying = false;

        string historyPath;
        string envName = "";

        private List<int> validIndices = new List<int>();   //  catalog에 존재하는 인덱스만 저장
        private List<int> deletedIndices = new List<int>(); //  삭제된 인덱스 저장 (복구 시 활용)
        private Dictionary<int, CatalogEntry> originalCatalogData; // 복원용 백업 딕셔너리 추가

        private int startFrameIndex = -1;   //  삭제할 첫 프레임 인덱스
        private int endFrameIndex = -1; //  삭제할 끝 프레임 인덱스

        bool isScrolling = false;   // 트랙바 스크롤 중인지 여부

        float leftX1Ema = 0;
        float leftX2Ema = 0;

        float rightX1Ema = 0;
        float rightX2Ema = 0;

        float alpha = 0.1f;

        int bottomWidth = 0; // 검출된 차선의 아랫부분 폭
        int topWidth = 0;    // 검출된 차선의 윗부분 폭

        int leftBottomX = 0;
        int leftTopX = 0;

        int rightBottomX = 0;
        int rightTopX = 0;

        bool leftDetected = false;
        bool rightDetected = false;

        private double actualAngle = 0; //  실제 앵글 값
        private double predictedAngle = 0;  //  AI가 예측한 앵글 값

        private double actualThrottle = 0;  //  실제 스로틀 값
        private double predictedThrottle = 0;   //  AI가 예측한 스로틀 값

        // catalog 데이터 저장할 딕셔너리
        Dictionary<int, CatalogEntry> catalogData = new Dictionary<int, CatalogEntry>();
        // 이상 탐지: 깨진 파일의 인덱스 저장
        HashSet<int> corruptedFileIndices = new HashSet<int>();

        public Form1()
        {
            InitializeComponent();

            combo_model.Items.Add("cnn");
            combo_model.Items.Add("lstm");
            combo_model.SelectedIndex = 0;

            historyPath = Path.Combine(baseDir, "deleted_history.json");

            if (File.Exists(historyPath))   //  삭제 기록이 존재하면 로드
            {
                string json = File.ReadAllText(historyPath);

                deletedHistory =
                    System.Text.Json.JsonSerializer.Deserialize<List<string>>(json);

                if (deletedHistory == null)
                    deletedHistory = new List<string>();
            }

            // 0. 기본 폼 세팅
            this.Visible = true;
            this.Opacity = 100;

            // 1. FORM 및 기본 테마 속성 업그레이드
            this.BackColor = Color.FromArgb(13, 13, 24);      // #0d0d18 (딥 스페이스 네이비)
            this.ForeColor = Color.FromArgb(204, 204, 204);   // #cccccc (실버 그레이)
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular);

            // 2. TAB CONTROL 하이테크 스타일 매칭
            tabControl.ItemSize = new Size(480, 35);
            tabControl.Appearance = TabAppearance.FlatButtons;
            tabControl.SizeMode = TabSizeMode.Fixed;

            tab_data.BackColor = Color.FromArgb(13, 13, 24);
            tab_train.BackColor = Color.FromArgb(13, 13, 24);

            // 3. PICTUREBOX & LISTBOX (어두운 톤 깊이감 및 테두리 글로우 효과)
            void StyleMonitorControl(Control control)
            {
                control.BackColor = Color.FromArgb(7, 7, 15);
                if (control is PictureBox pb) pb.BorderStyle = BorderStyle.None;
                if (control is ListBox lb) lb.BorderStyle = BorderStyle.None;

                control.Paint += (sender, e) =>
                {
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
                    {
                        int r = 10;
                        path.AddArc(0, 0, r, r, 180, 90);
                        path.AddArc(control.Width - r - 1, 0, r, r, 270, 90);
                        path.AddArc(control.Width - r - 1, control.Height - r - 1, r, r, 0, 90);
                        path.AddArc(0, control.Height - r - 1, r, r, 90, 90);
                        path.CloseFigure();

                        control.Region = new Region(path);

                        using (Pen pen = new Pen(Color.FromArgb(79, 195, 247), 2f))
                        {
                            e.Graphics.DrawPath(pen, path);
                        }
                    }
                };
            }

            StyleMonitorControl(picImage);
            StyleMonitorControl(picEdge);
            StyleMonitorControl(listImages);
            StyleMonitorControl(listBox_delete);

            // 진행률 레이블 스타일
            label_progressai.ForeColor = Color.FromArgb(79, 195, 247);
            label_progressai.Font = new Font("Consolas", 13F, FontStyle.Bold);
            label_progressai.BackColor = Color.Transparent;

            picImage.SizeMode = PictureBoxSizeMode.Zoom;
            picEdge.SizeMode = PictureBoxSizeMode.Zoom;

            // 리스트 박스 폰트 및 색상
            listImages.ForeColor = Color.FromArgb(0, 191, 255);
            listImages.Font = new Font("Consolas", 9.5F, FontStyle.Regular);

            listBox_delete.ForeColor = Color.FromArgb(0, 191, 255);
            listBox_delete.Font = new Font("Consolas", 9.5F, FontStyle.Regular);

            // 4. DIGITAL DASHBOARD LABELS (속도, 앵글 텍스트 대시보드화)
            label_throttle.BackColor = Color.FromArgb(13, 13, 24);
            label_throttle.ForeColor = Color.FromArgb(32, 201, 151);
            label_throttle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);

            label_angle.BackColor = Color.FromArgb(13, 13, 24);
            label_angle.ForeColor = Color.FromArgb(79, 195, 247);
            label_angle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);

            // 5. BUTTONS FLAT & CYBER COLOR STYLING (네온 글로우 버튼 고도화)
            void StyleButton(System.Windows.Forms.Button btn, Color borderColor)
            {
                btn.BackColor = Color.FromArgb(13, 13, 24);
                btn.ForeColor = borderColor;
                btn.FlatStyle = FlatStyle.Flat;
                // 로딩 빠른 버전 디자인
                btn.FlatAppearance.BorderColor = borderColor;  // ← 테두리 색
                btn.FlatAppearance.BorderSize = 1;             // ← 테두리 두께

                //btn.FlatAppearance.BorderSize = 0;
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(20, 20, 40);
                btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(7, 7, 15);

                btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                btn.Cursor = Cursors.Hand;

            }

            // splitContainer 스타일링 공통 메서드
            void StyleSplitContainer(SplitContainer sc)
            {
                sc.BackColor = Color.FromArgb(79, 195, 247);  // 스플릿 바 색
                sc.Panel1.BackColor = Color.FromArgb(13, 13, 24);
                sc.Panel2.BackColor = Color.FromArgb(13, 13, 24);
                sc.SplitterWidth = 4;  // 바 두께
                sc.IsSplitterFixed = false;  // 드래그 가능
            }

            StyleSplitContainer(splitContainer_up);
            StyleSplitContainer(splitContainer_allwindow);
            StyleSplitContainer(splitContainer_down);
            StyleSplitContainer(split_learnLeft);
            StyleSplitContainer(splitContainer_ai);

            // 데이터 매니저 chart_data 초기 설정
            chart_data.BackColor = Color.FromArgb(13, 13, 24);
            chart_data.BorderlineColor = Color.FromArgb(30, 30, 58);
            chart_data.BorderlineDashStyle = ChartDashStyle.Solid;
            chart_data.BorderlineWidth = 1;

            var area = chart_data.ChartAreas[0];
            area.BackColor = Color.FromArgb(7, 7, 15);
            area.AxisX.LabelStyle.ForeColor = Color.FromArgb(80, 80, 100);
            area.AxisY.LabelStyle.ForeColor = Color.FromArgb(80, 80, 100);
            area.AxisX.MajorGrid.LineColor = Color.FromArgb(20, 20, 35);
            area.AxisY.MajorGrid.LineColor = Color.FromArgb(20, 20, 35);
            area.AxisX.LineColor = Color.FromArgb(30, 30, 58);
            area.AxisY.LineColor = Color.FromArgb(30, 30, 58);
            area.AxisY.Minimum = -1.0;
            area.AxisY.Maximum = 1.0;

            foreach (var legend in chart_data.Legends)
            {
                legend.BackColor = Color.Transparent;
                legend.ForeColor = Color.FromArgb(204, 204, 204);
            }

            StyleButton(btn_delete, Color.FromArgb(239, 83, 80));
            StyleButton(btn_restore, Color.FromArgb(255, 213, 79));
            StyleButton(btnPlay, Color.FromArgb(102, 187, 106));
            StyleButton(btn_openfolder, Color.FromArgb(204, 204, 204));
            StyleButton(btnSetStart, Color.FromArgb(178, 223, 219));
            StyleButton(btnSetEnd, Color.FromArgb(178, 223, 219));
            StyleButton(btn_changquality, Color.FromArgb(255, 167, 38));
            StyleButton(btn_train, Color.FromArgb(79, 195, 247));
            StyleButton(btn_stopTrain, Color.FromArgb(239, 83, 80));
            StyleButton(btn_before, Color.FromArgb(102, 187, 106));
            StyleButton(btn_imgnext, Color.FromArgb(102, 187, 106));
            StyleButton(btnHelp, Color.FromArgb(79, 195, 247));
            StyleButton(btnDetect, Color.FromArgb(171, 71, 188));  // 이상탐지기능

            //6.TRACKBAR(순정 스타일 제거 후 네온 렌더링 세팅)

            trackBar_frame.BackColor = Color.FromArgb(13, 13, 24);
            trackBar_frame.TickStyle = TickStyle.None;
            trackBar_frame.ValueChanged += (sender, e) =>
            {
                trackBar_frame.Invalidate();
            };
            // 8. 학습 탭 하이테크 테마 디자인
            list_log.BackColor = Color.FromArgb(7, 7, 15);
            list_log.ForeColor = Color.FromArgb(0, 191, 255);
            list_log.BorderStyle = BorderStyle.FixedSingle;
            list_log.Font = new Font("Consolas", 9.5F, FontStyle.Regular);

            if (chart_loss != null)
            {
                chart_loss.BackColor = Color.FromArgb(13, 13, 24);

                foreach (var lossarea in chart_loss.ChartAreas)
                {
                    lossarea.BackColor = Color.FromArgb(7, 7, 15);
                    lossarea.AxisX.LabelStyle.ForeColor = Color.FromArgb(204, 204, 204);
                    lossarea.AxisY.LabelStyle.ForeColor = Color.FromArgb(204, 204, 204);

                    lossarea.AxisX.MajorGrid.LineColor = Color.FromArgb(40, 40, 60);
                    lossarea.AxisY.MajorGrid.LineColor = Color.FromArgb(40, 40, 60);

                    lossarea.AxisX.LineColor = Color.FromArgb(79, 195, 247);
                    lossarea.AxisY.LineColor = Color.FromArgb(79, 195, 247);
                    lossarea.AxisY.LineColor = Color.FromArgb(79, 195, 247);
                }

                foreach (var legend in chart_loss.Legends)
                {
                    legend.BackColor = Color.Transparent;
                    legend.ForeColor = Color.FromArgb(204, 204, 204);
                }

                if (chart_loss.Series.Count > 0)
                {
                    chart_loss.Series[0].Color = Color.FromArgb(32, 201, 151);
                }
                if (chart_loss.Series.IndexOf("Epoch") >= 0)
                {
                    chart_loss.Series["Epoch"].ChartType = SeriesChartType.Line;
                    chart_loss.Series["Epoch"].Color = Color.FromArgb(32, 201, 151);
                    chart_loss.Series["Epoch"].BorderWidth = 2;
                }
                if (chart_loss.Series.IndexOf("Loss") >= 0)
                {
                    chart_loss.Series["Loss"].ChartType = SeriesChartType.Line;
                    chart_loss.Series["Loss"].Color = Color.FromArgb(255, 167, 38);
                    chart_loss.Series["Loss"].BorderWidth = 2;
                }
            }

            // 데이터 경로 로드

            string imageFolderPath = Path.Combine(baseDir, "data", "images");

            historyPath = Path.Combine(baseDir, "deleted_history.json");

            trashFolderPath = Path.Combine(baseDir, "trash");

            backupFolderPath = Path.Combine(baseDir, "backup");

            Directory.CreateDirectory(backupFolderPath);

            Directory.CreateDirectory(trashFolderPath);

            try
            {
                LoadCatalog();
                UpdateDataChart();
            }
            catch (Exception ex)
            {
                MessageBox.Show("카탈로그 로드 오류: " + ex.Message);
            }
            LoadImageFolder(imageFolderPath);
            LoadCompareCombo();
            LoadTrashFolders();
            LoadTrashList();
            LoadVenvList();

            // 부모-자식 관계를 설정하여 완전한 투명(유리판) 효과를 구현
            picNeedleSpeed.Parent = picture_Gage;
            picNeedleAngle.Parent = picture_Gage;

            // 디자인 창에서 얹어둔 상대 위치에 맞게 좌표를 자동으로 재계산
            picNeedleSpeed.Location = new Point(picNeedleSpeed.Left - picture_Gage.Left, picNeedleSpeed.Top - picture_Gage.Top);
            picNeedleAngle.Location = new Point(picNeedleAngle.Left - picture_Gage.Left, picNeedleAngle.Top - picture_Gage.Top);
            picNeedleSpeed.BackColor = Color.Transparent;
            picNeedleAngle.BackColor = Color.Transparent;

            // 계기판 초기 바늘 렌더링
            DrawDashboardNeedles(0.0, 0.0);

            //label 계기판 위 표시
            label_throttle.Parent = picture_Gage;
            label_angle.Parent = picture_Gage;

            label_throttle.Location = new Point(110, 240);   // 왼쪽 박스 위치
            label_angle.Location = new Point(325, 240);     // 오른쪽 박스 위치

            label_throttle.BackColor = Color.Transparent;
            label_angle.BackColor = Color.Transparent;

            // imageFiles 로드 후 아래에 추가
            LoadThumbnails(currentIndex);

            // AI 수치 비교 타이틀
            label_aicompare.ForeColor = Color.FromArgb(79, 195, 247);
            label_aicompare.Font = new Font("Consolas", 22F, FontStyle.Bold);
            label_aicompare.BackColor = Color.Transparent;

            // 비교 레이블 및 수치 레이블
            StyleCompLabel(label_compthrottle);
            StyleCompLabel(label_compangle);
            StyleCompLabel(label_aithrottle);
            StyleCompLabel(label_aiangle);
            StyleNumLabel(label_compthroNum, Color.FromArgb(79, 195, 247));
            StyleNumLabel(label_compangleNum, Color.FromArgb(79, 195, 247));
            StyleNumLabel(label_aithroNum, Color.FromArgb(255, 167, 38));
            StyleNumLabel(label_aiangleNum, Color.FromArgb(255, 167, 38)); StyleProgressBar(progre_compthro, Color.FromArgb(79, 195, 247));
            StyleProgressBar(progre_compangle, Color.FromArgb(79, 195, 247));
            StyleProgressBar(progre_aithro, Color.FromArgb(255, 167, 38));
            StyleProgressBar(progre_aiangle, Color.FromArgb(255, 167, 38));
            // 학습률 프로그레스바
            StyleProgressBar(progressBar_learn, Color.FromArgb(79, 195, 247));
            StyleProgressBar(progressDelete, Color.FromArgb(239, 83, 80));

            cmbTrashList.SelectedIndexChanged += (s, e) =>
            {
                listBox_delete.Items.Clear();

                string selectedFolder = cmbTrashList.SelectedItem?.ToString();
                string targetPath = selectedFolder == "전체"
                    ? trashFolderPath
                    : Path.Combine(trashFolderPath, selectedFolder);

                if (!Directory.Exists(targetPath)) return;

                string[] files = Directory.GetFiles(targetPath, "*.jpg", SearchOption.AllDirectories)
                    .OrderBy(f => ExtractNumber(Path.GetFileNameWithoutExtension(f)))
                    .ToArray();

                foreach (string file in files)
                    listBox_delete.Items.Add(Path.GetFileName(file));
            };

            // 속도 앵글 차트 체크박스
            checkBox_throttle.CheckedChanged += (s, e) =>
            {
                chart_data.Series["Throttle"].Enabled = checkBox_throttle.Checked;
            };

            checkBox_angle.CheckedChanged += (s, e) =>
            {
                chart_data.Series["Angle"].Enabled = checkBox_angle.Checked;
            };

            // 초기 상태 체크
            checkBox_throttle.Checked = true;
            checkBox_angle.Checked = true;

            // 오차 레이블
            label_ocha.ForeColor = Color.FromArgb(102, 187, 106);
            label_ocha.Font = new Font("Consolas", 15F, FontStyle.Bold);
            label_ocha.BackColor = Color.Transparent;

            // 학습 품질 점수 타이틀
            label_learningNum.ForeColor = Color.FromArgb(79, 195, 247);
            label_learningNum.Font = new Font("Consolas", 22F, FontStyle.Bold);
            label_learningNum.BackColor = Color.Transparent;

            // 큰 점수 숫자
            label_score.Text = "0";
            label_score.ForeColor = Color.FromArgb(102, 187, 106);
            label_score.Font = new Font("Segoe UI", 36F, FontStyle.Bold);
            label_score.BackColor = Color.Transparent;
            label_score.TextAlign = ContentAlignment.MiddleCenter;

            // / 100점
            label_scoreUnit.Text = "/ 100점";
            label_scoreUnit.ForeColor = Color.FromArgb(220, 220, 220);
            label_scoreUnit.Font = new Font("Consolas", 18F);
            label_scoreUnit.BackColor = Color.Transparent;
            label_scoreUnit.TextAlign = ContentAlignment.MiddleCenter;

            // 등급
            label_grade.Text = "등급";
            label_grade.ForeColor = Color.FromArgb(102, 187, 106);
            label_grade.Font = new Font("Segoe UI", 25F, FontStyle.Bold);
            label_grade.BackColor = Color.Transparent;
            label_grade.TextAlign = ContentAlignment.MiddleCenter;

            // 학습 정확도 레이블
            label_progreScore.Text = "학습 정확도";
            label_progreScore.ForeColor = Color.FromArgb(220, 220, 220);
            label_progreScore.Font = new Font("Consolas", 14F);
            label_progreScore.BackColor = Color.Transparent;

            // 프로그레스바
            progressBar_score.Minimum = 0;
            progressBar_score.Maximum = 100;
            progressBar_score.Value = 0;
            progressBar_score.Style = ProgressBarStyle.Continuous;
            progressBar_score.ForeColor = Color.FromArgb(102, 187, 106);
            progressBar_score.BackColor = Color.FromArgb(26, 26, 48);

            // 기존 콤보박스 스타일 지정 구역들을 함수 호출로 대체
            StyleComboBox(cmbTrashList, 10F, "Consolas"); // Trash 리스트
            StyleComboBox(comboBox_play, 10F);             // 배속 조절 콤보박스
            StyleComboBox(combo_model, 10F);               // 모델 선택 콤보박스
            StyleComboBox(combo_compare, 15F, "Consolas"); // AI 수치 비교 콤보박스
            StyleComboBox(comboBox_venv, 10F);             // 가상환경 콤보박스

            // chart_loss 시리즈 초기화
            if (chart_loss.Series.Count == 0)
            {
                var trainSeries = new Series("Train Loss");
                trainSeries.ChartType = SeriesChartType.Line;
                trainSeries.Color = Color.FromArgb(32, 201, 151);
                trainSeries.BorderWidth = 2;
                chart_loss.Series.Add(trainSeries);

                var valSeries = new Series("Val Loss");
                valSeries.ChartType = SeriesChartType.Line;
                valSeries.Color = Color.FromArgb(255, 167, 38);
                valSeries.BorderWidth = 2;
                chart_loss.Series.Add(valSeries);
            }
            combo_compare.SelectedIndexChanged += combo_compare_SelectedIndexChanged;
            // 파일럿 아레나 탭 디자인
            tabPilotArena.BackColor = Color.FromArgb(13, 13, 24);

            StyleButton(btnStart, Color.FromArgb(102, 187, 106));
            StyleButton(btnbeforeFrame, Color.FromArgb(102, 187, 106));
            StyleButton(btnAfterFrame, Color.FromArgb(102, 187, 106));

            trackImage.BackColor = Color.FromArgb(13, 13, 24);
            trackImage.TickStyle = TickStyle.None;

            StyleProgressBar(progressSpeed, Color.FromArgb(79, 195, 247));
            StyleProgressBar(progressAngle, Color.FromArgb(79, 195, 247));
            StyleProgressBar(progressSpeedAI, Color.FromArgb(255, 167, 38));
            StyleProgressBar(progressAngleAI, Color.FromArgb(255, 167, 38));

            StyleNumLabel(label_compthroarenaNum, Color.FromArgb(79, 195, 247));
            StyleNumLabel(label_compangarenaNum, Color.FromArgb(79, 195, 247));
            StyleNumLabel(label_compthrottlearena, Color.FromArgb(79, 195, 247));
            StyleNumLabel(label_companglearena, Color.FromArgb(79, 195, 247));
            StyleNumLabel(label_aithroarenaNum, Color.FromArgb(255, 167, 38));
            StyleNumLabel(label_aiangarenaNum, Color.FromArgb(255, 167, 38));
            StyleNumLabel(label_aithroarena, Color.FromArgb(255, 167, 38));
            StyleNumLabel(label_aiangarena, Color.FromArgb(255, 167, 38));

            flowLayout_arena.BackColor = Color.FromArgb(7, 7, 15);
            flowLayout_arena.AutoScroll = true;

            // 파일럿 아레나 버튼 이벤트
            btnbeforeFrame.Click += (s, e) =>
            {
                SetCurrentIndex(currentIndex - 1);
                LoadArenaThumbnails(currentIndex);
            };

            btnAfterFrame.Click += (s, e) =>
            {
                SetCurrentIndex(currentIndex + 1);
                LoadArenaThumbnails(currentIndex);
            };

            trackImage.Scroll += (s, e) =>
            {
                SetCurrentIndex(trackImage.Value);
                LoadArenaThumbnails(currentIndex);
            };

            btnStart.Click += (s, e) =>
            {
                if (timer1.Enabled)
                {
                    timer1.Stop();
                    btnStart.Text = "▶";
                    LoadThumbnails(currentIndex);
                    LoadArenaThumbnails(currentIndex);
                }
                else
                {
                    timer1.Start();
                    btnStart.Text = "정지";
                }
            };

            LoadArenaThumbnails(currentIndex);

            panelTrackBarProgress.Paint += (sender, e) =>
            {
                if (startFrameIndex == -1 || validIndices.Count == 0) return;

                int startPointer = validIndices.IndexOf(startFrameIndex);
                if (startPointer == -1)
                {
                    int pos = validIndices.BinarySearch(startFrameIndex);
                    startPointer = pos < 0 ? ~pos : pos;
                }

                int endPointer = trackBar_frame.Value;

                if (startPointer > endPointer)
                {
                    int temp = startPointer;
                    startPointer = endPointer;
                    endPointer = temp;
                }

                int max = trackBar_frame.Maximum;
                if (max == 0) return;

                int trackWidth = panelTrackBarProgress.Width - 20;
                int trackLeft = 10;
                int trackY = panelTrackBarProgress.Height / 2;
                int trackHeight = 6;

                int x1 = trackLeft + (int)((double)startPointer / max * trackWidth);
                int x2 = trackLeft + (int)((double)endPointer / max * trackWidth);

                using (SolidBrush brush = new SolidBrush(Color.FromArgb(239, 83, 80)))
                {
                    e.Graphics.FillRectangle(brush, x1, trackY - trackHeight / 2, x2 - x1, trackHeight);
                }
            };
        }

        void LoadVenvList()
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "wsl";
                psi.Arguments = "bash -ic \"conda env list | awk '{print $1}' | grep -v '#' | grep -v '^$'\"";
                psi.UseShellExecute = false;
                psi.RedirectStandardOutput = true;
                psi.CreateNoWindow = true;

                Process p = Process.Start(psi);
                string output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();

                comboBox_venv.Items.Clear();
                foreach (string line in output.Split('\n'))
                {
                    string env = line.Trim();
                    if (!string.IsNullOrEmpty(env))
                        comboBox_venv.Items.Add(env);
                }

                if (comboBox_venv.Items.Count > 0)
                    comboBox_venv.SelectedIndex = 0;
            }
            catch
            {
                // WSL 없거나 conda 없으면 수동 입력 가능하게
                comboBox_venv.Items.Add("base");
            }

            // 사용자가 스크롤하면 자동 스크롤 중지
            list_log.MouseDown += (s, e) =>
            {
                userScrollingLog = true;
                savedTopIndex = list_log.TopIndex;
            };

            list_log.MouseUp += (s, e) =>
            {
                savedTopIndex = list_log.TopIndex;
            };

            list_log.MouseMove += (s, e) =>
            {
                if (userScrollingLog)
                    savedTopIndex = list_log.TopIndex;
            };

            list_log.MouseLeave += (s, e) =>
            {
                userScrollingLog = false;
            };


        }


        private void combo_compare_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combo_compare.SelectedIndex < 0) return;
            if (imageFiles == null || imageFiles.Length == 0) return;

            string selectedFile = combo_compare.SelectedItem?.ToString();
            if (selectedFile == null) return;

            string imagePath = imageFiles.FirstOrDefault(f => Path.GetFileName(f) == selectedFile);
            if (imagePath == null) return;

            int idx = ExtractNumber(Path.GetFileNameWithoutExtension(selectedFile));
            if (catalogData.ContainsKey(idx))
            {
                var entry = catalogData[idx];
                label_compthroNum.Text = entry.user_throttle.ToString("F3");
                label_compangleNum.Text = entry.user_angle.ToString("F3");
                progre_compthro.Value = Math.Min(100, (int)(Math.Abs(entry.user_throttle) * 100));
                progre_compangle.Value = Math.Min(100, (int)((entry.user_angle + 1) / 2 * 100));
            }

            string modelPath = Path.Combine(baseDir, "model.h5");
            if (!File.Exists(modelPath)) return;

            string capturedFile = selectedFile;
            string capturedImagePath = imagePath;
            string capturedEnvName = comboBox_venv.Text.Trim().Split(new char[] { ' ', '\t' })[0]; // ← envName 캡처

            Task.Run(() => RunAiPredict(capturedFile, capturedImagePath, capturedEnvName));
        }

        private void RunAiPredict(string selectedFile, string imagePath, string envName)
        {
            try
            {
                string wslImagePath = imagePath.Replace("C:\\", "/mnt/c/").Replace("\\", "/");
                string wslBase = baseDir.Replace("C:\\", "/mnt/c/").Replace("\\", "/");

                // conda 경로 가져오기
                string condaBase = "";
                ProcessStartInfo condaPsi = new ProcessStartInfo();
                condaPsi.FileName = "wsl";
                condaPsi.Arguments = "bash -ic \"conda info --base 2>/dev/null | tail -1\"";
                condaPsi.UseShellExecute = false;
                condaPsi.RedirectStandardOutput = true;
                condaPsi.CreateNoWindow = true;
                Process condaProc = Process.Start(condaPsi);
                string condaOutput = condaProc.StandardOutput.ReadToEnd();
                condaProc.WaitForExit();
                var matchConda = System.Text.RegularExpressions.Regex.Match(condaOutput, @"(/[^\s*]+miniconda\d*)");
                if (matchConda.Success) condaBase = matchConda.Value.Trim();

                string pythonPath = envName == "base"
                    ? $"{condaBase}/bin/python"
                    : $"{condaBase}/envs/{envName}/bin/python";

                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "wsl";
                psi.Arguments = $"bash -c \"cd {wslBase} && {pythonPath} evaluate_single.py {wslImagePath}\"";
                psi.UseShellExecute = false;
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardError = true;  // ← 추가
                psi.CreateNoWindow = true;

                Process p = Process.Start(psi);
                string output = p.StandardOutput.ReadToEnd();
                string error = p.StandardError.ReadToEnd();  // ← 추가
                p.WaitForExit();

                // 디버그 로그
                this.Invoke((Action)(() =>
                {
                    System.Diagnostics.Debug.WriteLine($"output: {output}");
                    System.Diagnostics.Debug.WriteLine($"error: {error}");
                    System.Diagnostics.Debug.WriteLine($"ExitCode: {p.ExitCode}");
                }));

                var resultMatch = System.Text.RegularExpressions.Regex.Match(output, @"\{.*\}");
                if (resultMatch.Success)
                {
                    dynamic result = JsonConvert.DeserializeObject(resultMatch.Value);
                    double aiAngle = (double)result["angle"];
                    double aiThrottle = (double)result["throttle"];

                    this.Invoke((Action)(() =>
                    {
                        label_aithroNum.Text = aiThrottle.ToString("F3");
                        label_aiangleNum.Text = aiAngle.ToString("F3");
                        progre_aithro.Value = Math.Min(100, (int)(Math.Abs(aiThrottle) * 100));
                        progre_aiangle.Value = Math.Min(100, (int)((aiAngle + 1) / 2 * 100));

                        int idx = ExtractNumber(Path.GetFileNameWithoutExtension(selectedFile));
                        if (catalogData.ContainsKey(idx))
                        {
                            double diff = Math.Abs(catalogData[idx].user_angle - aiAngle);
                            label_ocha.Text = $"오차: {diff:F3}";
                            label_ocha.ForeColor = diff < 0.1
                                ? Color.FromArgb(102, 187, 106)
                                : Color.FromArgb(239, 83, 80);
                        }
                    }));
                }
                else
                {
                    this.Invoke((Action)(() =>
                    {
                        System.Diagnostics.Debug.WriteLine($"JSON 파싱 실패. output: {output}");
                    }));
                }
            }
            catch (Exception ex)
            {
                this.Invoke((Action)(() =>
                {
                    System.Diagnostics.Debug.WriteLine($"RunAiPredict 오류: {ex.Message}");
                    label_aithroNum.Text = "오류";
                    label_aiangleNum.Text = "오류";
                }));
            }
        }

        private void RunAiPredict(string selectedFile, string imagePath)
        {
            try
            {
                string wslImagePath = imagePath.Replace("C:\\", "/mnt/c/").Replace("\\", "/");
                string wslBase = baseDir.Replace("C:\\", "/mnt/c/").Replace("\\", "/");

                string condaBase = "";
                ProcessStartInfo condaPsi = new ProcessStartInfo();
                condaPsi.FileName = "wsl";
                condaPsi.Arguments = "bash -ic \"conda info --base 2>/dev/null | tail -1\"";
                condaPsi.UseShellExecute = false;
                condaPsi.RedirectStandardOutput = true;
                condaPsi.CreateNoWindow = true;
                Process condaProc = Process.Start(condaPsi);
                string condaOutput = condaProc.StandardOutput.ReadToEnd();
                condaProc.WaitForExit();
                var match = System.Text.RegularExpressions.Regex.Match(condaOutput, @"(/[^\s*]+miniconda\d*)");
                if (match.Success) condaBase = match.Value.Trim();

                string pythonPath = envName == "base"
                    ? $"{condaBase}/bin/python"
                    : $"{condaBase}/envs/{envName}/bin/python";

                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "wsl";
                psi.Arguments = $"bash -c \"cd {wslBase} && {pythonPath} evaluate_single.py {wslImagePath}\"";
                psi.UseShellExecute = false;
                psi.RedirectStandardOutput = true;
                psi.CreateNoWindow = true;

                Process p = Process.Start(psi);
                string output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();

                var resultMatch = System.Text.RegularExpressions.Regex.Match(output, @"\{.*\}");
                if (resultMatch.Success)
                {
                    dynamic result = JsonConvert.DeserializeObject(resultMatch.Value);
                    double aiAngle = (double)result["angle"];
                    double aiThrottle = (double)result["throttle"];

                    this.Invoke((Action)(() =>
                    {
                        label_aithroNum.Text = aiThrottle.ToString("F3");
                        label_aiangleNum.Text = aiAngle.ToString("F3");
                        progre_aithro.Value = Math.Min(100, (int)(Math.Abs(aiThrottle) * 100));
                        progre_aiangle.Value = Math.Min(100, (int)((aiAngle + 1) / 2 * 100));

                        int idx = ExtractNumber(Path.GetFileNameWithoutExtension(selectedFile));
                        if (catalogData.ContainsKey(idx))
                        {
                            double diff = Math.Abs(catalogData[idx].user_angle - aiAngle);
                            label_ocha.Text = $"오차: {diff:F3}";
                            label_ocha.ForeColor = diff < 0.1
                                ? Color.FromArgb(102, 187, 106)
                                : Color.FromArgb(239, 83, 80);
                        }
                    }));
                }
            }
            catch { }
        }

        // AI 수치 비교용 콤보박스에 랜덤 5개 아이템 로드
        void LoadCompareCombo()
        {
            combo_compare.Items.Clear();

            if (imageFiles == null || imageFiles.Length == 0)
                return;

            // 랜덤 5개 선택
            var random = new Random();
            var randomIndices = Enumerable.Range(0, imageFiles.Length)
                .OrderBy(x => random.Next())
                .Take(5)
                .OrderBy(x => x)  // 인덱스 순서로 정렬
                .ToList();

            foreach (int idx in randomIndices)
            {
                combo_compare.Items.Add(Path.GetFileName(imageFiles[idx]));
            }

            if (combo_compare.Items.Count > 0)
                combo_compare.SelectedIndex = 0;
        }

        // 콤보박스 하이테크 스타일 적용 공통 메서드
        private void StyleComboBox(System.Windows.Forms.ComboBox cmb)
        {
            cmb.FlatStyle = FlatStyle.Flat;
            cmb.BackColor = Color.FromArgb(18, 18, 32);
            cmb.ForeColor = Color.FromArgb(204, 204, 204);
            cmb.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        }
        // 폰트 크기를 커스텀 함수
        private void StyleComboBox(System.Windows.Forms.ComboBox cmb, float fontSize, string fontName = "Segoe UI")
        {
            cmb.FlatStyle = FlatStyle.Flat;
            cmb.BackColor = Color.FromArgb(18, 18, 32);
            cmb.ForeColor = Color.FromArgb(204, 204, 204);
            cmb.Font = new Font(fontName, fontSize, FontStyle.Bold);
        }

        // 10. CONTROL-BASED NEEDLE DRAWING 
        public void DrawDashboardNeedles(double speed, double angle)
        {
            // 1. 왼쪽 속도 바늘 드로잉 (picNeedleSpeed)
            if (picNeedleSpeed.Width > 0 && picNeedleSpeed.Height > 0)
            {
                Bitmap bmpSpeed = new Bitmap(picNeedleSpeed.Width, picNeedleSpeed.Height);
                using (Graphics g = Graphics.FromImage(bmpSpeed))
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.Clear(Color.Transparent);

                    int cx = picNeedleSpeed.Width / 2;
                    int cy = picNeedleSpeed.Height / 2;
                    int len = (int)(picNeedleSpeed.Height * 0.88);

                    float speedAngle = (float)(360 + (speed * 135));
                    DrawNeonNeedleControl(g, cx, cy, speedAngle, len, Color.FromArgb(32, 201, 151));
                }
                if (picNeedleSpeed.Image != null) picNeedleSpeed.Image.Dispose();
                picNeedleSpeed.Image = bmpSpeed;
            }

            // 2. 오른쪽 앵글 바늘 드로잉 (picNeedleAngle)
            if (picNeedleAngle.Width > 0 && picNeedleAngle.Height > 0)
            {
                Bitmap bmpAngle = new Bitmap(picNeedleAngle.Width, picNeedleAngle.Height);
                using (Graphics g = Graphics.FromImage(bmpAngle))
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.Clear(Color.Transparent);

                    int cx = picNeedleAngle.Width / 2;
                    int cy = picNeedleAngle.Height / 2;
                    int len = (int)(picNeedleAngle.Height * 0.88);

                    // 마찬가지로 베이스 각도를 360으로 보정하여 90도 시계 방향 회전을 매핑합니다.
                    float angleRot = (float)(360 + (angle * 135));
                    DrawNeonNeedleControl(g, cx, cy, angleRot, len, Color.FromArgb(79, 195, 247));
                }
                if (picNeedleAngle.Image != null) picNeedleAngle.Image.Dispose();
                picNeedleAngle.Image = bmpAngle;
            }
        }

        // 네온 바늘을 그리는 메서드입니다.
        private void DrawNeonNeedleControl(Graphics g, int cx, int cy, float angleInDegrees, int length, Color needleColor)
        {
            double radians = (angleInDegrees - 90) * Math.PI / 180.0;

            // 바늘 끝 (길게)
            int endX = cx + (int)(length * Math.Cos(radians));
            int endY = cy + (int)(length * Math.Sin(radians));

            // 바늘 뒤쪽 (반대 방향 짧게 - 실제 계기판처럼)
            int tailX = cx - (int)(length * 0.2 * Math.Cos(radians));
            int tailY = cy - (int)(length * 0.2 * Math.Sin(radians));

            // 바늘 좌우 폭 (끝은 뾰족하게, 중간은 살짝 굵게)
            double perpRadians = radians + Math.PI / 2;
            int halfWidth = 4;
            PointF tipPoint = new PointF(endX, endY);  // 뾰족한 끝
            PointF leftBase = new PointF(
                tailX + (int)(halfWidth * Math.Cos(perpRadians)),
                tailY + (int)(halfWidth * Math.Sin(perpRadians))
            );
            PointF rightBase = new PointF(
                tailX - (int)(halfWidth * Math.Cos(perpRadians)),
                tailY - (int)(halfWidth * Math.Sin(perpRadians))
            );

            PointF[] needleShape = { tipPoint, leftBase, rightBase };

            // 글로우 효과 (외곽 퍼짐)
            using (Pen glowPen = new Pen(Color.FromArgb(40, needleColor), 10f))
            {
                glowPen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
                g.DrawPolygon(glowPen, needleShape);
            }

            // 바늘 채우기
            using (System.Drawing.Drawing2D.PathGradientBrush brush =
                new System.Drawing.Drawing2D.PathGradientBrush(needleShape))
            {
                brush.CenterColor = Color.White;
                brush.SurroundColors = new Color[] { needleColor, needleColor, needleColor };
                g.FillPolygon(brush, needleShape);
            }

            // 바늘 테두리
            using (Pen outlinePen = new Pen(Color.FromArgb(200, needleColor), 1f))
            {
                g.DrawPolygon(outlinePen, needleShape);
            }

            // 중심 캡 (작고 깔끔하게)
            int capR = 6;
            using (System.Drawing.Drawing2D.GraphicsPath capPath = new System.Drawing.Drawing2D.GraphicsPath())
            {
                capPath.AddEllipse(cx - capR, cy - capR, capR * 2, capR * 2);
                using (System.Drawing.Drawing2D.PathGradientBrush capBrush =
                    new System.Drawing.Drawing2D.PathGradientBrush(capPath))
                {
                    capBrush.CenterColor = Color.White;
                    capBrush.SurroundColors = new Color[] { needleColor };
                    g.FillEllipse(capBrush, cx - capR, cy - capR, capR * 2, capR * 2);
                }
            }
            using (Pen capPen = new Pen(Color.FromArgb(180, needleColor), 1.5f))
            {
                g.DrawEllipse(capPen, cx - capR, cy - capR, capR * 2, capR * 2);
            }
        }

        // 글자 레이블 공통 스타일
        void StyleCompLabel(Label lbl)
        {
            lbl.ForeColor = Color.FromArgb(220, 220, 220);
            lbl.Font = new Font("Consolas", 15F, FontStyle.Bold);
            lbl.BackColor = Color.Transparent;
        }

        // 수치 레이블 공통 스타일
        void StyleNumLabel(Label lbl, Color color)
        {
            lbl.ForeColor = color;
            lbl.Font = new Font("Consolas", 15F, FontStyle.Bold);
            lbl.BackColor = Color.Transparent;
        }

        // ProgressBar 공통 스타일
        void StyleProgressBar(ProgressBar pb, Color color)
        {
            pb.Style = ProgressBarStyle.Continuous;
            pb.Minimum = 0;
            pb.Maximum = 100;
            pb.Value = 0;
            pb.ForeColor = color;
            pb.BackColor = Color.FromArgb(26, 26, 48);
        }
        // 삭제된 이미지 리스트 로드 및 초기화
        void LoadTrashList()
        {
            listBox_delete.Items.Clear();

            if (!Directory.Exists(trashFolderPath))
                return;

            string[] trashFiles = Directory.GetFiles(trashFolderPath, "*.jpg", SearchOption.AllDirectories)
                .OrderBy(f => ExtractNumber(Path.GetFileNameWithoutExtension(f)))
                .ToArray();

            foreach (string file in trashFiles)
            {
                string relativePath = file.Replace(trashFolderPath + "\\", "");
                listBox_delete.Items.Add(relativePath);
            }
        }
        void LoadTrashFolders()
        {
            cmbTrashList.Items.Clear();
            cmbTrashList.Items.Add("전체");

            if (!Directory.Exists(trashFolderPath))
                return;

            string[] subFolders = Directory.GetDirectories(trashFolderPath);
            foreach (string folder in subFolders)
            {
                cmbTrashList.Items.Add(Path.GetFileName(folder));
            }

            cmbTrashList.SelectedIndex = 0;
        }

        void LoadImageFolder(string folderPath) // 이미지 폴더 로드 및 초기화
        {
            if (!Directory.Exists(folderPath))
            {
                MessageBox.Show("폴더가 존재하지 않습니다.");
                using (FolderBrowserDialog dialog = new FolderBrowserDialog())
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        LoadImageFolder(dialog.SelectedPath);
                    }
                }
            }

            currentFolderPath = folderPath;

            imageFiles = Directory
                .GetFiles(folderPath, "*.jpg")
                .OrderBy(f => ExtractNumber(Path.GetFileNameWithoutExtension(f)))
                .ToArray();

            listImages.Items.Clear();

            foreach (string file in imageFiles)
            {
                listImages.Items.Add(
                    Path.GetFileName(file)
                );
            }

            if (imageFiles.Length > 0)
            {
                trackBar_frame.Enabled = true;
                trackBar_frame.Minimum = 0;
                trackBar_frame.Maximum = imageFiles.Length - 1;

                // validIndices가 채워진 후에만 SetCurrentIndex 호출
                if (validIndices != null && validIndices.Count > 0)
                    SetCurrentIndex(0);
            }
            else
            {
                trackBar_frame.Enabled = false;
            }
        }

        int ExtractNumber(string name)
        {
            string number =
                new string(name.Where(char.IsDigit).ToArray());

            if (int.TryParse(number, out int result))
                return result;

            return int.MaxValue;
        }

        void CompressAllImages(int quality, double scale)
        {
            foreach (string imagePath in imageFiles)
            {
                string fileName = Path.GetFileName(imagePath);

                string backupPath =
                    Path.Combine(backupFolderPath, fileName);

                // 최초 1회만 원본 백업
                if (!File.Exists(backupPath))
                {
                    File.Copy(imagePath, backupPath);
                }

                // 항상 원본(back up) 기준으로 읽기
                Mat img = CvInvoke.Imread(backupPath);

                if (img.IsEmpty)
                    continue;

                Mat resized = new Mat();

                CvInvoke.Resize(
                    img,
                    resized,
                    Size.Empty,
                    scale,
                    scale,
                    Inter.Area
                );

                var param = new KeyValuePair<ImwriteFlags, int>[]
                {
                    new KeyValuePair<ImwriteFlags, int>(
                        ImwriteFlags.JpegQuality,
                        quality
                    )
                };

                // 작업 폴더(images)에 저장
                CvInvoke.Imwrite(imagePath, resized, param);
            }

            ReloadCurrentFolder();

            var info = new
            {
                quality = quality,
                scale = scale,
                isCompressed = (quality < 100 || scale < 1.0)
            };

            File.WriteAllText(
                Path.Combine(baseDir, "data", "compression_info.json"),
                JsonConvert.SerializeObject(info, Formatting.Indented)
            );

            MessageBox.Show("전체 이미지 압축 완료");
        }
        void SaveDeleteHistory()    //  삭제 기록을 JSON 파일로 저장
        {
            string json =
                System.Text.Json.JsonSerializer.Serialize(
                    deletedHistory,
                    new System.Text.Json.JsonSerializerOptions
                    {
                        WriteIndented = true
                    });

            File.WriteAllText(historyPath, json);
        }
        void RestoreOriginalImages()    //  백업 폴더에서 원본 이미지 복구
        {
            string[] backupFiles =
                Directory.GetFiles(backupFolderPath, "*.jpg");

            foreach (string backupPath in backupFiles)
            {
                string fileName = Path.GetFileName(backupPath);

                string restorePath =
                    Path.Combine(currentFolderPath, fileName);

                File.Copy(backupPath, restorePath, true);
            }

            ReloadCurrentFolder();

            MessageBox.Show("원본 이미지 복구 완료");
        }
        void ReloadCurrentFolder()
        {
            LoadImageFolder(currentFolderPath);
        }

        void SetCurrentIndex(int index)
        {
            if (imageFiles.Length == 0) return;
            if (validIndices == null || validIndices.Count == 0) return;

            if (index >= imageFiles.Length) index = 0;
            if (index < 0) index = imageFiles.Length - 1;

            int targetIndex = index;

            if (!catalogData.ContainsKey(targetIndex))
            {
                int pos = validIndices.BinarySearch(targetIndex);
                if (pos < 0)
                {
                    int nextValidPointer = ~pos;
                    if (nextValidPointer >= validIndices.Count)
                        nextValidPointer = 0;
                    targetIndex = validIndices[nextValidPointer];
                }
            }

            currentIndex = targetIndex;

            isScrolling = true;
            int uiPointer = validIndices.IndexOf(currentIndex);
            if (uiPointer != -1)
            {
                trackBar_frame.Value = uiPointer;
                if (!timer1.Enabled && listImages.Items.Count > uiPointer)
                    listImages.SelectedIndex = uiPointer;
            }
            isScrolling = false;

            LoadImage();

            lblCurrentFrame.Text = $"현재 프레임: {currentIndex}";
            lblTotalFrame.Text = $"전체 프레임: {imageFiles.Length}";

            if (catalogData.ContainsKey(currentIndex))
            {
                var entry = catalogData[currentIndex];
                label_throttle.Text = $"{entry.user_throttle:F3}";
                label_angle.Text = $"{entry.user_angle:F3}";
                DrawDashboardNeedles(entry.user_throttle, entry.user_angle);
            }

            if (!timer1.Enabled)
            {
                LoadThumbnails(currentIndex);
                LoadArenaThumbnails(currentIndex);
            }
        }

        void LoadImage()
        {
            if (imageFiles == null || imageFiles.Length == 0) return;

            Mat frame = CvInvoke.Imread(imageFiles[currentIndex]);
            if (frame.IsEmpty)
            {
                MessageBox.Show("이미지 로드 실패");
                return;
            }
            ProcessFrame(frame);
        }
        void UpdateDataChart()
        {
            chart_data.Series["Throttle"].Points.Clear();
            chart_data.Series["Angle"].Points.Clear();

            foreach (var kv in catalogData.OrderBy(k => k.Key))
            {
                chart_data.Series["Throttle"].Points.AddXY(kv.Key, kv.Value.user_throttle);
                chart_data.Series["Angle"].Points.AddXY(kv.Key, kv.Value.user_angle);
            }
        }

        void ProcessFrame(Mat frame)
        {
            Mat originalFrame = frame.Clone();

            leftDetected = false;
            rightDetected = false;

            int centerX = frame.Width / 2;
            List<Point> leftPoints = new List<Point>();
            List<Point> rightPoints = new List<Point>();

            Mat roiEdge = CreateRoiEdge(frame);

            LineSegment2D[] lines = CvInvoke.HoughLinesP(roiEdge, 1, Math.PI / 180, 15, 15, 10);

            foreach (LineSegment2D line in lines)
            {
                double dx = line.P2.X - line.P1.X;
                double dy = line.P2.Y - line.P1.Y;
                if (dx == 0) continue;

                double slope = dy / dx;
                Point p1 = line.P1;
                Point p2 = line.P2;

                if (slope < -0.3 && p1.X < centerX && p2.X < centerX)
                {
                    leftPoints.Add(p1);
                    leftPoints.Add(p2);
                }
                else if (slope > 0.3 && p1.X > centerX && p2.X > centerX)
                {
                    rightPoints.Add(p1);
                    rightPoints.Add(p2);
                }
            }

            DrawFitLine(frame, leftPoints, new MCvScalar(255, 0, 0), true);
            DrawFitLine(frame, rightPoints, new MCvScalar(0, 0, 255), false);

            if (leftDetected && rightDetected)
            {
                bottomWidth = rightBottomX - leftBottomX;
                topWidth = rightTopX - leftTopX;
            }

            if (leftDetected && !rightDetected)
            {
                int fakeRightBottom = leftBottomX + bottomWidth;
                int fakeRightTop = leftTopX + topWidth;
                CvInvoke.Line(frame, new Point(fakeRightBottom, frame.Rows), new Point(fakeRightTop, frame.Rows / 2), new MCvScalar(0, 255, 255), 5);
            }
            else if (!leftDetected && rightDetected)
            {
                int fakeLeftBottom = rightBottomX - bottomWidth;
                int fakeLeftTop = rightTopX - topWidth;
                CvInvoke.Line(frame, new Point(fakeLeftBottom, frame.Rows), new Point(fakeLeftTop, frame.Rows / 2), new MCvScalar(0, 255, 255), 3);
            }

            // 기존 이미지 해제
            if (picImage.Image != null)
            {
                picImage.Image.Dispose();
                picImage.Image = null;
            }

            if (picEdge.Image != null)
            {
                picEdge.Image.Dispose();
                picEdge.Image = null;
            }

            if (checkBox1.Checked)
                picImage.Image = frame.ToBitmap();
            else
                picImage.Image = originalFrame.ToBitmap();
            picEdge.Image = roiEdge.ToBitmap();
        }

        void DrawFitLine(Mat frame, List<Point> points, MCvScalar color, bool isLeft)
        {
            if (points.Count < 2) return;

            PointF[] pts = points.Select(p => new PointF(p.X, p.Y)).ToArray();
            VectorOfPointF vec = new VectorOfPointF(pts);
            Mat line = new Mat();

            CvInvoke.FitLine(vec, line, DistType.L2, 0, 0.01, 0.01);
            float[] data = new float[4];
            line.CopyTo(data);

            float vx = data[0];
            float vy = data[1];
            float x = data[2];
            float y = data[3];

            if (Math.Abs(vy) < 0.0001) return;

            int y1 = frame.Rows;
            int y2 = frame.Rows / 2;

            int x1 = (int)(x + (y1 - y) * vx / vy);
            int x2 = (int)(x + (y2 - y) * vx / vy);

            if (isLeft)
            {
                if (leftX1Ema == 0) { leftX1Ema = x1; leftX2Ema = x2; }
                leftX1Ema = alpha * x1 + (1 - alpha) * leftX1Ema;
                leftX2Ema = alpha * x2 + (1 - alpha) * leftX2Ema;
                x1 = (int)leftX1Ema; x2 = (int)leftX2Ema;
            }
            else
            {
                if (rightX1Ema == 0) { rightX1Ema = x1; rightX2Ema = x2; }
                rightX1Ema = alpha * x1 + (1 - alpha) * rightX1Ema;
                rightX2Ema = alpha * x2 + (1 - alpha) * rightX2Ema;
                x1 = (int)rightX1Ema; x2 = (int)rightX2Ema;
            }

            if (isLeft) { leftBottomX = x1; leftTopX = x2; leftDetected = true; }
            else { rightBottomX = x1; rightTopX = x2; rightDetected = true; }

            CvInvoke.Line(frame, new Point(x1, y1), new Point(x2, y2), color, 5);
        }

        Mat CreateRoiEdge(Mat frame)
        {
            Mat hsv = new Mat();
            CvInvoke.CvtColor(frame, hsv, ColorConversion.Bgr2Hsv);

            Mat whiteMask = new Mat();
            CvInvoke.InRange(
                hsv,
                new ScalarArray(new MCvScalar(0, 0, 160)),
                new ScalarArray(new MCvScalar(180, 80, 255)),
                whiteMask
            );

            Mat edge = new Mat();
            CvInvoke.Canny(whiteMask, edge, 50, 150);

            Mat mask = new Mat(edge.Size, DepthType.Cv8U, 1);
            mask.SetTo(new MCvScalar(0));

            Point[] points =
            {
                new Point(100, edge.Rows),
                new Point(250, edge.Rows / 2),
                new Point(edge.Cols - 250, edge.Rows / 2),
                new Point(edge.Cols - 100, edge.Rows)
            };

            using (VectorOfPoint polygon = new VectorOfPoint(points))
            {
                CvInvoke.FillConvexPoly(mask, polygon, new MCvScalar(255));
            }

            Mat roiEdge = new Mat();
            CvInvoke.BitwiseAnd(edge, mask, roiEdge);

            hsv.Dispose();
            whiteMask.Dispose();
            edge.Dispose();
            mask.Dispose();

            return roiEdge;
        }

        void LoadCatalog()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string dataPath = Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..", "data"));
            // 경로 확인
            System.Diagnostics.Debug.WriteLine($"data 경로: {dataPath}");
            System.Diagnostics.Debug.WriteLine($"폴더 존재: {Directory.Exists(dataPath)}");
            if (!Directory.Exists(dataPath))
            {
                MessageBox.Show("data 폴더를 찾을 수 없습니다: " + dataPath);
                return;
            }

            string[] catalogFiles = Directory.GetFiles(dataPath, "*.catalog");

            foreach (string file in catalogFiles)
            {
                if (Path.GetFileName(file) == "training_data.catalog") continue;  // ← 추가

                if (file.EndsWith("_manifest")) continue;

                string[] lines = File.ReadAllLines(file);
                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    var json = System.Text.Json.JsonDocument.Parse(line);
                    var entry = new CatalogEntry
                    {
                        _index = json.RootElement.GetProperty("_index").GetInt32(),
                        cam_image_array = json.RootElement.GetProperty("cam/image_array").GetString(),
                        user_angle = json.RootElement.GetProperty("user/angle").GetDouble(),
                        user_throttle = json.RootElement.GetProperty("user/throttle").GetDouble()
                    };
                    catalogData[entry._index] = entry;
                }
            }

            validIndices = catalogData.Keys.OrderBy(k => k).ToList();
            // 만약 trackBar_frame이 있다면 최대값도 맞춰줍니다.
            trackBar_frame.Maximum = validIndices.Count - 1;

            originalCatalogData = new Dictionary<int, CatalogEntry>(catalogData);
        }
        private void RunPythonTrain(string modelType)
        {
            string script = modelType == "cnn" ? "train.py" : "train_lstm.py";
            string wslBase = baseDir.Replace("C:\\", "/mnt/c/").Replace("\\", "/");

            // conda base 경로 동적으로 가져오기
            string condaBase = "";
            try
            {
                ProcessStartInfo condaPsi = new ProcessStartInfo();
                condaPsi.FileName = "wsl";
                //condaPsi.Arguments = "bash -ic \"conda info --base\"";
                condaPsi.Arguments = "bash -c \"$(which conda 2>/dev/null || echo /home/$(whoami)/miniconda3/bin/conda) info --base 2>/dev/null | tail -1\"";
                condaPsi.UseShellExecute = false;
                condaPsi.RedirectStandardOutput = true;
                condaPsi.CreateNoWindow = true;
                Process condaProc = Process.Start(condaPsi);
                string output = condaProc.StandardOutput.ReadToEnd();
                condaProc.WaitForExit();
                // 정규식으로 추출 후 추가 정리
                var match = System.Text.RegularExpressions.Regex.Match(output, @"(/[^\s*]+miniconda\d*)");
                if (match.Success)
                    condaBase = match.Value.Trim();
            }
            catch { }


            // btn_train_Click 또는 RunPythonTrain에서
            envName = envName.Trim().Split(new char[] { ' ', '\t' })[0];
            if (envName == "base")
                pythonPath = $"{condaBase}/bin/python";
            else
                pythonPath = $"{condaBase}/envs/{envName}/bin/python";

            // pythonPath에서 공백/탭으로 분리된 첫 번째 부분만 사용
            pythonPath = pythonPath.Trim().Split(new char[] { ' ', '\t' })[0];

            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "wsl";
            psi.Arguments = $"bash -c \"cd {wslBase} && exec {pythonPath} {script} --data data --epochs 10\"";

            //디버그용
            this.Invoke((Action)(() =>
            {
                list_log.Items.Add($"condaBase: {condaBase}");
                list_log.Items.Add($"python 경로: {pythonPath}");
                list_log.Items.Add($"명령어: {psi.Arguments}");
            }));

            psi.WorkingDirectory = baseDir;
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.CreateNoWindow = true;
            // 나머지 동일...

            trainProcess = Process.Start(psi);

            // 실시간 로그 출력
            trainProcess.OutputDataReceived += (s, args) =>
            {
                if (args.Data == null) return;
                this.Invoke((Action)(() =>
                {
                    list_log.Items.Add($"[{DateTime.Now:HH:mm:ss}] {args.Data}");

                    if (list_log.UserAutoScroll)
                    {
                        int visibleItems = (int)(list_log.Height / list_log.ItemHeight);
                        if (list_log.Items.Count > visibleItems)
                            list_log.TopIndex = list_log.Items.Count - visibleItems;
                    }
                    // epoch 진행률 파싱 (예: "Epoch 3/10")
                    if (args.Data.Contains("Epoch"))
                    {
                        try
                        {
                            var match = System.Text.RegularExpressions.Regex.Match(
                                args.Data, @"Epoch (\d+)/(\d+)");
                            if (match.Success)
                            {
                                int current = int.Parse(match.Groups[1].Value);
                                int total = int.Parse(match.Groups[2].Value);
                                int percent = (int)((double)current / total * 100);
                                progressBar_learn.Value = Math.Min(percent, 100);
                                label_progressai.Text = $"진행률: {percent}% ({current}/{total} epoch)";
                            }
                        }
                        catch { }
                    }

                    // loss 값 파싱 (예: "loss: 0.0019 - val_loss: 0.0087")
                    if (args.Data.Contains("loss:") && args.Data.Contains("val_loss:"))
                    {
                        try
                        {
                            var lossMatch = System.Text.RegularExpressions.Regex.Match(
                                args.Data, @"loss: ([\d.]+) - val_loss: ([\d.]+)");
                            if (lossMatch.Success)
                            {
                                double trainLoss = double.Parse(lossMatch.Groups[1].Value);
                                double valLoss = double.Parse(lossMatch.Groups[2].Value);
                                int epochNum = chart_loss.Series["Epoch"].Points.Count + 1;

                                chart_loss.Series["Epoch"].Points.AddXY(epochNum, trainLoss);
                                chart_loss.Series["Loss"].Points.AddXY(epochNum, valLoss);
                            }
                        }
                        catch { }
                    }
                }));
            };

            trainProcess.ErrorDataReceived += (s, args) =>
            {
                if (args.Data == null) return;
                this.Invoke((Action)(() =>
                {
                    list_log.Items.Add($"[{DateTime.Now:HH:mm:ss}] {args.Data}");

                    if (list_log.UserAutoScroll)
                    {
                        int visibleItems = (int)(list_log.Height / list_log.ItemHeight);
                        if (list_log.Items.Count > visibleItems)
                            list_log.TopIndex = list_log.Items.Count - visibleItems;
                    }
                }));
            };

            trainProcess.BeginOutputReadLine();
            trainProcess.BeginErrorReadLine();
            trainProcess.WaitForExit();

            this.Invoke((Action)(() =>
            {
                if (trainProcess.ExitCode == 0)
                {
                    list_log.Items.Add($"[{DateTime.Now:HH:mm:ss}] ✅ 학습 완료!");

                    // 학습 완료 후 score.json 읽어서 점수 표시
                    string scorePath = Path.Combine(baseDir, "score.json");
                    if (File.Exists(scorePath))
                    {
                        string json = File.ReadAllText(scorePath);
                        dynamic result = JsonConvert.DeserializeObject(json);
                        double valLoss = result["val_loss"];
                        UpdateScore(valLoss);
                    }

                    LoadCompareCombo();

                    btnAfterFrame.Enabled = false;
                    btnbeforeFrame.Enabled = false;
                    btnStart.Enabled = false;

                    Task.Run(() =>
                    {
                        RunPredictionGeneration(modelType);

                        this.Invoke((Action)(() =>
                        {
                            LoadPredictions();

                            btnAfterFrame.Enabled = true;
                            btnbeforeFrame.Enabled = true;
                            btnStart.Enabled = true;
                        }));
                    });
                }
                else
                {
                    list_log.Items.Add($"[{DateTime.Now:HH:mm:ss}] ❌ 학습 실패 (ExitCode: {trainProcess.ExitCode})");
                }

                if (list_log.UserAutoScroll)
                {
                    int visibleItems = (int)(list_log.Height / list_log.ItemHeight);
                    if (list_log.Items.Count > visibleItems)
                        list_log.TopIndex = list_log.Items.Count - visibleItems;
                }
                btn_train.Enabled = true;
                btn_stopTrain.Enabled = false;
                trainProcess = null;
            }));


        }

        private void LoadPredictions()
        {
            string path =
                Path.Combine(
                    baseDir,
                    "predictions.json"
                );

            if (!File.Exists(path))
                return;

            string json =
                File.ReadAllText(path);

            predictions =
                JsonConvert.DeserializeObject<
                    List<PredictionRecord>
                >(json);
        }

        private void RunPredictionGeneration(string modelType)
        {
            try
            {
                string scripts = "predict_all.py";

                string wslBase =
                    baseDir.Replace("C:\\", "/mnt/c/")
                           .Replace("\\", "/");

                ProcessStartInfo psi =
                    new ProcessStartInfo();

                psi.FileName = "wsl";

                psi.Arguments =
                    $"bash -c \"cd {wslBase} && {pythonPath} {scripts} {modelType}\"";

                psi.UseShellExecute = false;
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardError = true;
                psi.CreateNoWindow = true;

                Process proc = Process.Start(psi);

                string stdout =
                    proc.StandardOutput.ReadToEnd();

                string stderr =
                    proc.StandardError.ReadToEnd();

                proc.WaitForExit();

                if (proc.ExitCode == 0)
                {
                    AddLog("✅ 예측 데이터 생성 완료");

                    this.Invoke((Action)(() =>
                    {
                        MessageBox.Show("예측 데이터 생성 완료");
                    }));
                }
                else
                {
                    AddLog($"[{DateTime.Now:HH:mm:ss}] ❌ 예측 생성 실패");

                    AddLog(stderr);
                }
            }
            catch (Exception ex)
            {
                AddLog(
                    $"[{DateTime.Now:HH:mm:ss}] ❌ 예측 오류 : {ex.Message}"
                );
            }
        }

        private void AddLog(string text)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => AddLog(text)));
                return;
            }

            list_log.Items.Add(text);
        }
        private void ShowFrame(int index)
        {
            if (predictions == null || index < 0 || index >= predictions.Count)
                return;

            currentFrame = index;

            string imagePath =
                Path.Combine(
                    baseDir,
                    "data",
                    "images",
                    predictions[index].image
                );

            if (!File.Exists(imagePath))
            {
                MessageBox.Show(imagePath + " 없음");
                return;
            }

            if (picboxImage.Image != null)
                picboxImage.Image.Dispose();

            picboxImage.Image =
                Image.FromFile(imagePath);

            picboxImage.Invalidate();
                       
            // 프레임 번호 표시
            lblCurrentFrame2.Text = $"프레임: {index}";
            // 실제 값 표시
            var p = predictions[index];
            label_compthroarenaNum.Text = p.real_throttle.ToString("F3");
            label_compangarenaNum.Text = p.real_angle.ToString("F3");

            // AI 예측값 표시
            label_aithroarenaNum.Text = p.pred_throttle.ToString("F3");
            label_aiangarenaNum.Text = p.pred_angle.ToString("F3");

            // 프로그레스바 업데이트, 앵글은 절대값 방식
            progressSpeed.Value = Math.Min(100, (int)(Math.Abs(p.real_throttle) * 100));
            progressAngle.Value = Math.Min(100, (int)(Math.Abs(p.real_angle) * 100));
            progressSpeedAI.Value = Math.Min(100, (int)(Math.Abs(p.pred_throttle) * 100));
            progressAngleAI.Value = Math.Min(100, (int)(Math.Abs(p.pred_angle) * 100));

            double speedError = Math.Abs(p.real_throttle - p.pred_throttle);
            double angleError = Math.Abs(p.real_angle - p.pred_angle);

            lblSpeedError.Text = $"오차: {speedError:F3}";
            lblAngleError.Text = $"오차: {angleError:F3}";

            // 오차 크기에 따라 색상 변경 0.1 미만이면 초록, 이상이면 빨강
            lblSpeedError.ForeColor = speedError < 0.1 ? Color.FromArgb(102, 187, 106) : Color.FromArgb(239, 83, 80);
            lblAngleError.ForeColor = angleError < 0.1 ? Color.FromArgb(102, 187, 106) : Color.FromArgb(239, 83, 80);
            //현재 이미지 근처 이미지 로드
            LoadArenaThumbnails(index);
        }

        private void DrawDriveLine(
            Graphics g,
            double angle,
            double throttle,
            Pen pen)
        {
            int centerX = picboxImage.Width / 2;
            int bottomY = picboxImage.Height - 20;

            int minLength = 30;
            int maxLength = 180;

            int length =
                minLength +
                (int)((maxLength - minLength) * throttle);

            double rad =
                angle * Math.PI / 2.0;

            int endX =
                centerX +
                (int)(Math.Sin(rad) * length);

            int endY =
                bottomY -
                (int)(Math.Cos(rad) * length);

            g.DrawLine(
                pen,
                centerX,
                bottomY,
                endX,
                endY
            );
        }

        void UpdateScore(double valLoss)
        {
            double score = Math.Max(0, (1.0 - valLoss * 2.0)) * 100;
            score = Math.Round(score, 1);

            string grade;
            Color gradeColor;

            if (score >= 90) { grade = "S  매우 우수"; gradeColor = Color.FromArgb(79, 195, 247); }
            else if (score >= 75) { grade = "A  우수"; gradeColor = Color.FromArgb(102, 187, 106); }
            else if (score >= 60) { grade = "B  보통"; gradeColor = Color.FromArgb(255, 167, 38); }
            else if (score >= 40) { grade = "C  미흡"; gradeColor = Color.FromArgb(239, 83, 80); }
            else { grade = "D  불량"; gradeColor = Color.FromArgb(150, 50, 50); }

            label_score.Text = $"{score:F1}";
            label_score.ForeColor = gradeColor;
            label_grade.Text = grade;
            label_grade.ForeColor = gradeColor;
            progressBar_score.Value = (int)score;
        }
        private void RunPythonEvaluate(String modelType)
        {
            string script = "";

            if (modelType == "cnn")
                script = "evaluate.py";
            else if (modelType == "lstm")
                script = "evaluate_lstm.py";

            ProcessStartInfo psi =
            new ProcessStartInfo();

            psi.FileName = "wsl";

            psi.Arguments = $"bash -ic \"conda activate {envName} && python {script}\"";

            psi.WorkingDirectory = baseDir;

            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.CreateNoWindow = true;

            Process p = Process.Start(psi);

            string output =
                p.StandardOutput.ReadToEnd();

            string error =
                p.StandardError.ReadToEnd();

            p.WaitForExit();

            list_log.Items.Add(output);

            if (!string.IsNullOrEmpty(error))
                list_log.Items.Add(error);

            p.WaitForExit();

            list_log.Items.Add(
                "EXIT CODE = " + p.ExitCode
            );
        }

        private void listImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isScrolling) return;
            if (listImages.SelectedIndex == -1) return;
            if (timer1.Enabled) return;

            int targetIdx = validIndices[listImages.SelectedIndex];
            SetCurrentIndex(targetIdx);

            for (int i = 0; i < flowPanel_thumbnails.Controls.Count; i++)
            {
                var thumb = (PictureBox)flowPanel_thumbnails.Controls[i];
                thumb.BorderStyle = i == currentIndex
                    ? BorderStyle.Fixed3D
                    : BorderStyle.FixedSingle;
            }
        }

        private void trackBar_frame_Scroll(object sender, EventArgs e)
        {
            if (isScrolling) return;
            SetCurrentIndex(trackBar_frame.Value);
        }
        private void btn_imgnext_Click(object sender, EventArgs e)
        {
            isScrolling = true;
            listImages.ClearSelected();
            SetCurrentIndex(currentIndex + 1);
            isScrolling = false;
        }

        private void btn_before_Click(object sender, EventArgs e)
        {
            isScrolling = true;
            listImages.ClearSelected();
            SetCurrentIndex(currentIndex - 1);
            isScrolling = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SetCurrentIndex(currentIndex + 1);
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Stop();
                btnPlay.Text = "▶";

                int uiPointer = validIndices.IndexOf(currentIndex);
                if (uiPointer != -1 && listImages.Items.Count > uiPointer)
                {
                    isScrolling = true;
                    listImages.SelectedIndex = uiPointer;
                    isScrolling = false;
                }

                LoadThumbnails(currentIndex);
                LoadArenaThumbnails(currentIndex);
            }
            else
            {
                timer1.Start();
                btnPlay.Text = "정지";
            }
        }

        private void btn_train_Click(object sender, EventArgs e)
        {
            bool useBackupImages =
                Directory.Exists(backupFolderPath) &&
                Directory.GetFiles(backupFolderPath).Length > 0;    //  백업 폴더에 이미지가 존재하는 경우

            bool useBackupForTraining = false;

            string compressionFile =
                Path.Combine(baseDir, "data", "compression_info.json");

            try
            {
                if (File.Exists(compressionFile))
                {
                    dynamic info =
                        JsonConvert.DeserializeObject(
                            File.ReadAllText(compressionFile)
                        );

                    useBackupForTraining =
                        info?.isCompressed ?? false;
                }
            }
            catch
            {
                useBackupForTraining = false;
            }


            // 1. 데이터 검증
            if (imageFiles == null || imageFiles.Length == 0)
            {
                MessageBox.Show("학습할 이미지가 없습니다.\n\n폴더를 먼저 열어주세요.", "데이터 없음", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (catalogData.Count == 0)
            {
                MessageBox.Show("카탈로그 데이터가 없습니다.\n\ndata 폴더에 .catalog 파일이 있는지 확인하세요.", "카탈로그 없음", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. 초기화
            string imagesPath = Path.Combine(baseDir, "data", "images");
            string wbImagesPath = Path.Combine(baseDir, "data", "wbimages");

            // ⭐ 학습 버튼 누를 때 프로그레스바와 상단 텍스트 레이블 즉시 초기화
            progressBar_learn.Value = 0;
            label_progressai.Text = "진행률: 0% (0/10 epoch)"; // ← 이 부분을 추가하여 텍스트도 초기화합니다.

            list_log.Items.Clear();

            if (chart_loss.Series.IndexOf("Epoch") >= 0)
                chart_loss.Series["Epoch"].Points.Clear();
            if (chart_loss.Series.IndexOf("Loss") >= 0)
                chart_loss.Series["Loss"].Points.Clear();

            Directory.CreateDirectory(wbImagesPath);

            // 중복 호출 방지를 위해 버튼을 여기서 확실히 비활성화
            btn_train.Enabled = false;
            list_log.Items.Add($"[{DateTime.Now:HH:mm:ss}] 🔍 원본 이미지 스캔 시작...");
            list_log.Items.Add($"[{DateTime.Now:HH:mm:ss}] 📊 카탈로그 레코드: {catalogData.Count}개");

            try
            {
                int successCount = 0;
                int skippedCount = 0;
                int recordIndex = 0;
                List<Dictionary<string, object>> jsonRecords = new List<Dictionary<string, object>>();
                List<Dictionary<string, object>> backupRecords = new List<Dictionary<string, object>>();
                List<string> failedImages = new List<string>();

                int totalImages = imageFiles.Length;
                list_log.Items.Add($"[{DateTime.Now:HH:mm:ss}] ⚙️ 총 {totalImages}장의 이미지 검증 및 전처리 시작...");

                // 3. 각 원본 이미지 처리
                int processedCount = 0;
                foreach (string imagePath in imageFiles)
                {
                    try
                    {
                        string fileName = Path.GetFileName(imagePath);
                        int originalIndex = ExtractNumber(Path.GetFileNameWithoutExtension(fileName));

                        double angle = 0.0;
                        double throttle = 0.0;

                        if (catalogData.ContainsKey(originalIndex))
                        {
                            angle = catalogData[originalIndex].user_angle;
                            throttle = catalogData[originalIndex].user_throttle;
                        }

                        // 필터링 체크박스가 체크된 경우에만 앵글 0 또는 쓰로틀 0 이하 데이터 제외
                        if (checkBox_filter.Checked && (angle == 0.0 || throttle <= 0.0))
                        {
                            skippedCount++;
                            continue;
                        }

                        // 흑백 전처리 이미지 생성
                        string wbImageSavePath = Path.Combine(wbImagesPath, fileName);

                        if (!File.Exists(wbImageSavePath))
                        {
                            using (Mat frame = CvInvoke.Imread(imagePath, ImreadModes.AnyColor))
                            {
                                if (!frame.IsEmpty)
                                {
                                    using (Mat processedRoi = CreateRoiEdge(frame))
                                    {
                                        using (Mat finalImage = new Mat())
                                        {
                                            CvInvoke.CvtColor(processedRoi, finalImage, ColorConversion.Gray2Bgr);
                                            CvInvoke.Imwrite(wbImageSavePath, finalImage);
                                        }
                                    }
                                }
                            }
                        }

                        // 레코드 생성
                        var record = new Dictionary<string, object>
                        {
                            ["_index"] = recordIndex++,
                            ["_timestamp"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                            ["cam/image_array"] = $"images/{fileName}",
                            ["cam/image_wb"] = $"wbimages/{fileName}",
                            ["user/angle"] = angle,
                            ["user/throttle"] = throttle
                        };

                        jsonRecords.Add(record);

                        if (useBackupImages && useBackupForTraining)
                        {
                            string backupImage =
                                Path.Combine(
                                    backupFolderPath,
                                    fileName
                                );

                            if (File.Exists(backupImage))
                            {
                                string backupFileName =
                                    "original_" + fileName;

                                string wbBackupPath =
                                    Path.Combine(
                                        wbImagesPath,
                                        backupFileName
                                    );

                                if (!File.Exists(wbBackupPath))
                                {
                                    using (Mat frame = CvInvoke.Imread(backupImage, ImreadModes.AnyColor))
                                    {
                                        if (!frame.IsEmpty)
                                        {
                                            using (Mat processedRoi = CreateRoiEdge(frame))
                                            {
                                                using (Mat finalImage = new Mat())
                                                {
                                                    CvInvoke.CvtColor(
                                                        processedRoi,
                                                        finalImage,
                                                        ColorConversion.Gray2Bgr
                                                    );

                                                    CvInvoke.Imwrite(
                                                        wbBackupPath,
                                                        finalImage
                                                    );
                                                }
                                            }
                                        }
                                    }
                                }

                                var backupRecord =
                                    new Dictionary<string, object>
                                    {
                                        ["_index"] = recordIndex++,
                                        ["_timestamp"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                                        ["cam/image_array"] = $"backup/{fileName}",
                                        ["cam/image_wb"] = $"wbimages/{backupFileName}",
                                        ["user/angle"] = angle,
                                        ["user/throttle"] = throttle
                                    };

                                backupRecords.Add(backupRecord);
                            }
                        }

                        successCount++;

                        // 진행률 표시 (10%마다)
                        processedCount++;
                        int updateInterval = Math.Max(1, totalImages / 10);
                        if (processedCount % updateInterval == 0)
                        {
                            int percentage = (int)((double)processedCount / totalImages * 100);
                            list_log.Items.Add($"[{DateTime.Now:HH:mm:ss}] 📊 진행률: {percentage}% ({processedCount}/{totalImages}장)");
                            if (userScrollingLog && savedTopIndex >= 0)
                            {
                                // 사용자가 보던 위치 유지
                                list_log.TopIndex = savedTopIndex;
                            }
                            else
                            {
                                // 자동 스크롤
                                list_log.SelectedIndex = list_log.Items.Count - 1;
                                list_log.ClearSelected();
                            }
                            Application.DoEvents();
                        }
                    }
                    catch (Exception imageEx)
                    {
                        string errorMsg = $"{Path.GetFileName(imagePath)}: {imageEx.Message}";
                        failedImages.Add(errorMsg);
                        Console.WriteLine($"⚠️ {errorMsg}");
                    }
                }

                // 4. training_data.catalog 저장
                jsonRecords.AddRange(backupRecords);

                string catalogPath = Path.Combine(baseDir, "data", "training_data.catalog");
                list_log.Items.Add($"[{DateTime.Now:HH:mm:ss}] 💾 카탈로그 파일 저장 중...");

                using (StreamWriter writer = new StreamWriter(catalogPath, false, System.Text.Encoding.UTF8))
                {
                    foreach (var record in jsonRecords)
                    {
                        string jsonLine = System.Text.Json.JsonSerializer.Serialize(record);
                        writer.WriteLine(jsonLine);
                    }
                }

                list_log.Items.Add($"[{DateTime.Now:HH:mm:ss}] ✅ 저장 완료: {catalogPath}");
                list_log.Items.Add($"[{DateTime.Now:HH:mm:ss}] 📝 총 {jsonRecords.Count}개 레코드 작성");

                // 5. 실패 로그 저장
                if (failedImages.Count > 0)
                {
                    string failedLogPath = Path.Combine(baseDir, "data", "failed_images.log");
                    File.WriteAllLines(failedLogPath, failedImages);
                    list_log.Items.Add($"[{DateTime.Now:HH:mm:ss}] ⚠️ 실패 로그: {failedLogPath}");
                }

                // 6. 최종 통계
                list_log.Items.Add($"[{DateTime.Now:HH:mm:ss}] ═══════════════════════════");
                list_log.Items.Add($"[{DateTime.Now:HH:mm:ss}] 📊 최종 통계");
                list_log.Items.Add($"[{DateTime.Now:HH:mm:ss}] ✅ 성공: {successCount}장");
                list_log.Items.Add($"[{DateTime.Now:HH:mm:ss}] ⏭️ 스킵: {skippedCount}장 (angle==0 또는 throttle<=0)");
                list_log.Items.Add($"[{DateTime.Now:HH:mm:ss}] ❌ 실패: {failedImages.Count}장");
                list_log.Items.Add($"[{DateTime.Now:HH:mm:ss}] ═══════════════════════════");
                if (userScrollingLog && savedTopIndex >= 0)
                {
                    // 사용자가 보던 위치 유지
                    list_log.TopIndex = savedTopIndex;
                }
                else
                {
                    // 자동 스크롤
                    list_log.SelectedIndex = list_log.Items.Count - 1;
                    list_log.ClearSelected();
                }

                // 완료 메시지
                string resultMessage = $"✅ 학습 데이터 준비 완료!\n\n";
                resultMessage += $"📊 통계:\n";
                resultMessage += $"  • 성공: {successCount}장\n";
                resultMessage += $"  • 스킵: {skippedCount}장\n";
                resultMessage += $"  • 실패: {failedImages.Count}장\n\n";
                resultMessage += $"📁 생성된 파일:\n";
                resultMessage += $"  • training_data.catalog\n";
                resultMessage += $"  • wbimages/ 폴더 ({successCount}장)";

                if (failedImages.Count > 0)
                {
                    resultMessage += $"\n  • failed_images.log";
                }

                MessageBox.Show(resultMessage, "학습 데이터 준비 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ 오류:\n{ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                list_log.Items.Add($"[{DateTime.Now:HH:mm:ss}] ❌ 오류: {ex.Message}");
            }
            /*finally
            {
                btn_train.Enabled = true;
            }*/ //학습이 비동기로 처리되므로 우선 주석처리함

            //////////////////////////////////////////// 모델 학습 및 평가
            ///
            // 가상환경 확인
            if (string.IsNullOrWhiteSpace(comboBox_venv.Text))
            {
                MessageBox.Show("가상환경을 선택하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btn_train.Enabled = true;
                return;
            }

            // 모델 확인
            if (combo_model.SelectedIndex < 0)
            {
                MessageBox.Show("모델을 선택하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btn_train.Enabled = true;
                return;
            }

            string modelType = combo_model.SelectedItem.ToString().ToLower();
            envName = comboBox_venv.Text;

            list_log.Items.Add($"[{DateTime.Now:HH:mm:ss}] 🚀 학습 시작 (모델: {modelType}, 환경: {envName})");
            if (userScrollingLog && savedTopIndex >= 0)
            {
                // 사용자가 보던 위치 유지
                list_log.TopIndex = savedTopIndex;
            }
            else
            {
                // 자동 스크롤
                list_log.SelectedIndex = list_log.Items.Count - 1;
                list_log.ClearSelected();
            }

            btn_stopTrain.Enabled = true;

            Task.Run(() =>
            {
                try
                {
                    RunPythonTrain(modelType);
                }
                catch (Exception ex)
                {
                    this.Invoke((Action)(() =>
                    {
                        list_log.Items.Add($"[{DateTime.Now:HH:mm:ss}] ❌ 학습 오류: {ex.Message}");
                        btn_train.Enabled = true;
                        btn_stopTrain.Enabled = false;
                    }));
                }
            });
        }
        // 학습 중단 버튼 클릭 시
        private void btn_stopTrain_Click(object sender, EventArgs e)
        {
            if (trainProcess != null && !trainProcess.HasExited)
            {
                trainProcess.Kill();
                list_log.Items.Add($"[{DateTime.Now:HH:mm:ss}]  학습 중단됨");
                btn_train.Enabled = true;
                btn_stopTrain.Enabled = false;
                trainProcess = null;
            }
        }

        void LoadThumbnails(int centerIndex)
        {
            flowPanel_thumbnails.Controls.Clear();

            // 현재 프레임 기준 앞뒤 10개씩만 표시
            int start = Math.Max(0, centerIndex - 10);
            int end = Math.Min(imageFiles.Length - 1, centerIndex + 10);

            for (int i = start; i <= end; i++)
            {
                PictureBox thumb = new PictureBox();
                thumb.Size = new Size(80, 60);
                thumb.SizeMode = PictureBoxSizeMode.Zoom;
                thumb.BackColor = Color.FromArgb(7, 7, 15);
                thumb.Cursor = Cursors.Hand;
                thumb.Tag = i;

                // 현재 선택된 것 강조
                thumb.BorderStyle = i == centerIndex
                    ? BorderStyle.Fixed3D
                    : BorderStyle.FixedSingle;

                try { thumb.Image = Image.FromFile(imageFiles[i]); }
                catch { }

                thumb.Click += (sender, e) =>
                {
                    int thumbIndex = (int)((PictureBox)sender).Tag;
                    SetCurrentIndex(thumbIndex);
                };

                flowPanel_thumbnails.Controls.Add(thumb);
            }

            // 현재 선택된 썸네일로 스크롤
            int selectedThumbIndex = centerIndex - start;
            if (selectedThumbIndex >= 0 && selectedThumbIndex < flowPanel_thumbnails.Controls.Count)
            {
                flowPanel_thumbnails.ScrollControlIntoView(
                    flowPanel_thumbnails.Controls[selectedThumbIndex]
                );
            }
        }

        private void btn_openfolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    LoadImageFolder(dialog.SelectedPath);
                }
            }
        }

        // 이미지 파일 검증
        private bool IsValidImage(string imagePath, out string errorReason)
        {
            errorReason = "";

            // 파일 존재 확인
            if (!File.Exists(imagePath))
            {
                errorReason = "파일 없음";
                return false;
            }

            try
            {
                // 파일 크기 확인
                FileInfo fileInfo = new FileInfo(imagePath);
                if (fileInfo.Length == 0)
                {
                    errorReason = "빈 파일 (0 bytes)";
                    return false;
                }

                if (fileInfo.Length < 100) // 최소 크기
                {
                    errorReason = $"파일 크기 너무 작음 ({fileInfo.Length} bytes)";
                    return false;
                }

                // OpenCV로 이미지 로드 시도
                using (Mat frame = CvInvoke.Imread(imagePath, ImreadModes.AnyColor))
                {
                    if (frame.IsEmpty)
                    {
                        errorReason = "이미지 로드 실패 (손상된 파일)";
                        return false;
                    }

                    if (frame.Width == 0 || frame.Height == 0)
                    {
                        errorReason = "이미지 해상도 0x0";
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                errorReason = $"이미지 검증 오류: {ex.Message}";
                return false;
            }

            return true;
        }
        private void btn_delete_Click(object sender, EventArgs e)
        {
            // 1. 예외 처리
            if (startFrameIndex == -1 || endFrameIndex == -1)
            {
                MessageBox.Show("시작 프레임과 끝 프레임을 먼저 설정해 주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. 현재 표시 중인 이미지 자원 해제
            if (picImage.Image != null) { picImage.Image.Dispose(); picImage.Image = null; }
            if (picEdge.Image != null) { picEdge.Image.Dispose(); picEdge.Image = null; }

            // 3. 정렬 보정
            int minIdx = Math.Min(startFrameIndex, endFrameIndex);
            int maxIdx = Math.Max(startFrameIndex, endFrameIndex);

            int totalToProcess = maxIdx - minIdx + 1;
            progressDelete.Minimum = 0;
            progressDelete.Maximum = totalToProcess;
            progressDelete.Value = 0;

            int deleteCount = 0;
            int currentLoopCount = 0;

            // ListBox 업데이트를 일시 중지하여 속도 최적화
            listBox_delete.BeginUpdate();

            // 4. 해당 구간의 Catalog 데이터를 주석 처리 및 삭제 리스트에 추가
            for (int i = minIdx; i <= maxIdx; i++)
            {
                currentLoopCount++;

                if (catalogData.ContainsKey(i))
                {
                    // 아직 삭제 목록에 없는 인덱스만 추가 (중복 방지)
                    if (!deletedIndices.Contains(i))
                    {
                        deletedIndices.Add(i);
                        listBox_delete.Items.Add($"Frame {i}");
                    }

                    catalogData.Remove(i);
                    deleteCount++;
                }

                // [추가] 100프레임마다 ProgressBar와 UI를 갱신 (성능 저하 방지 및 부드러운 연동)
                if (currentLoopCount % 100 == 0 || currentLoopCount == totalToProcess)
                {
                    progressDelete.Value = currentLoopCount;
                    Application.DoEvents(); // UI가 멈추지 않고 실시간으로 그려지도록 강제 갱신
                }
            }

            // 삭제 리스트 오름차순 정렬 (보기 좋게 정렬)
            deletedIndices.Sort();

            // ListBox 업데이트 재개
            listBox_delete.EndUpdate();

            // 5. 대량 삭제 최적화: 유효 인덱스 리스트 갱신
            validIndices = catalogData.Keys.OrderBy(k => k).ToList();

            RefreshImageListUI();

            MessageBox.Show($"{minIdx} ~ {maxIdx} 구간에서 {deleteCount}개의 프레임 데이터가 삭제(주석 처리)되었습니다.", "삭제 완료");

            // 6. 사용한 선택 변수 초기화
            startFrameIndex = -1;
            endFrameIndex = -1;
            progressDelete.Value = 0;

            //lblStartStatus.Text = "시작: 미지정";
            //lblEndStatus.Text = "끝: 미지정";

            // 앞서 만든 이진 탐색 기반 SetCurrentIndex 덕분에 1,000장이 지워졌어도 렉 없이 즉시 다음 프레임을 찾아갑니다.
            SetCurrentIndex(minIdx);

        }

        void RefreshImageListUI()
        {
            // 이벤트 중복 발생 및 UI 깜빡임 방지
            isScrolling = true;
            listImages.BeginUpdate();

            listImages.Items.Clear();

            // validIndices에 살아남은 프레임들만 ListBox에 추가
            foreach (int idx in validIndices)
            {
                // 예: "Frame 0015 (image_0015.jpg)" 형태로 보기 좋게 출력
                // 원본 파일명을 보여주고 싶다면 Path.GetFileName(imageFiles[idx]) 활용
                string fileName = Path.GetFileName(imageFiles[idx]);
                listImages.Items.Add($"{fileName}");
            }

            listImages.EndUpdate();
            isScrolling = false;
        }

        private void btn_restore_Click(object sender, EventArgs e)
        {
            // 1. 예외 처리: 선택된 항목이 없을 때
            if (listBox_delete.SelectedItems.Count == 0)
            {
                MessageBox.Show("복원할 프레임을 삭제 목록에서 선택해 주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 선택된 항목들을 담을 리스트 (인덱스 반복 및 UI 순회 꼬임 방지용 복사본)
            List<string> selectedItemsCopy = new List<string>();
            foreach (var item in listBox_delete.SelectedItems)
            {
                selectedItemsCopy.Add(item.ToString());
            }

            List<int> indicesToRestore = new List<int>();

            // "Frame 123" 형태 또는 실제 파일명에서 숫자 추출
            foreach (string itemText in selectedItemsCopy)
            {
                if (itemText.StartsWith("Frame "))
                {
                    if (int.TryParse(itemText.Replace("Frame ", ""), out int resIdx))
                    {
                        indicesToRestore.Add(resIdx);
                    }
                }
                else
                {
                    // 만약 파일명 형태(예: image_0123.jpg)로 들어있을 경우를 대비한 안전장치
                    int resIdx = ExtractNumber(Path.GetFileNameWithoutExtension(itemText));
                    if (resIdx != int.MaxValue)
                    {
                        indicesToRestore.Add(resIdx);
                    }
                }
            }

            int restoreCount = 0;

            // ListBox 업데이트 일시 중지 (렉 방지 및 연동 꼬임 방지)
            listBox_delete.BeginUpdate();

            foreach (int idx in indicesToRestore)
            {
                // 원본 백업 데이터(originalCatalogData)에서 데이터를 찾아 catalogData에 재삽입
                if (originalCatalogData != null && originalCatalogData.ContainsKey(idx))
                {
                    if (!catalogData.ContainsKey(idx))
                    {
                        catalogData.Add(idx, originalCatalogData[idx]);
                    }

                    // 전역 삭제 리스트에서 제거
                    deletedIndices.Remove(idx);

                    // UI 목록에서 '내가 선택했던 그 항목'만 정확하게 한 줄 제거
                    string exactFrameKey = $"Frame {idx}";
                    object itemToRemove = null;
                    foreach (var item in listBox_delete.Items)
                    {
                        if (item.ToString() == exactFrameKey || ExtractNumber(Path.GetFileNameWithoutExtension(item.ToString())) == idx)
                        {
                            itemToRemove = item;
                            break;
                        }
                    }
                    if (itemToRemove != null)
                    {
                        listBox_delete.Items.Remove(itemToRemove);
                    }

                    restoreCount++;
                }
            }

            listBox_delete.EndUpdate();

            // 2. 복원된 데이터가 있을 때만 핵심 마스터 리스트 갱신 및 차트 업데이트
            if (restoreCount > 0)
            {
                // 유효 인덱스 리스트 정렬 갱신
                validIndices = catalogData.Keys.OrderBy(k => k).ToList();

                // 메인 이미지 리스트박스(listImages) UI 동기화 새로고침
                RefreshImageListUI();

                // 차트 데이터에 복원된 수치 반영
                UpdateDataChart();

                MessageBox.Show($"{restoreCount}개의 프레임이 성공적으로 복원되었습니다.", "복원 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 복원된 프레임 중 가장 첫 번째 프레임으로 화면을 즉시 이동시켜 시각적 확인 제공
                SetCurrentIndex(indicesToRestore.Min());
            }
            else
            {
                MessageBox.Show("복원 가능한 원본 카탈로그 데이터를 찾지 못했습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // ⚠️ [중요] 기존에 무조건 전체를 다시 긁어오던 LoadTrashList() 호출을 제거하여 
            // 메모리 주석 복원과 하드디스크 쓰레기통 폴더 조회가 충돌하는 현상을 원천 차단합니다.
        }

        private void btnSetStart_Click(object sender, EventArgs e)
        {
            startFrameIndex = currentIndex;
            int uiPointer = validIndices.IndexOf(currentIndex);
        }

        private void btnSetEnd_Click(object sender, EventArgs e)
        {
            endFrameIndex = currentIndex;
            int uiPointer = validIndices.IndexOf(currentIndex);
        }

        private void btn_changquality_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox(
                "JPEG 품질을 입력하세요 (10~100)",
                "화질 조정",
                "70"
            );

            if (string.IsNullOrWhiteSpace(input))
                return;

            if (!int.TryParse(input, out int quality))
            {
                MessageBox.Show("숫자만 입력하세요.");
                return;
            }

            if (quality < 10 || quality > 100)
            {
                MessageBox.Show("10~100 사이만 가능합니다.");
                return;
            }

            CompressAllImages(quality, 1.0);

            MessageBox.Show($"전체 이미지 품질 {quality} 적용 완료");
        }
        private void btnDetect_Click(object sender, EventArgs e)
        {
            if (imageFiles == null || imageFiles.Length == 0)
            {
                MessageBox.Show("이미지 폴더를 먼저 열어주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 이전 검증 결과 초기화
            corruptedFileIndices.Clear();

            int totalFiles = imageFiles.Length;
            int corruptedCount = 0;
            List<string> errorMessages = new List<string>();

            // 진행 상황 표시
            Cursor = Cursors.WaitCursor;
            btnDetect.Enabled = false; // 중복 클릭 방지

            try
            {
                // 각 이미지 파일 검증
                for (int i = 0; i < imageFiles.Length; i++)
                {
                    string imagePath = imageFiles[i];
                    string fileName = Path.GetFileName(imagePath);
                    int imageIndex = ExtractNumber(Path.GetFileNameWithoutExtension(fileName));

                    bool isCorrupted = false;
                    List<string> errors = new List<string>();

                    // [검증 1] 이미지 파일 검증
                    if (!IsValidImage(imagePath, out string imageError))
                    {
                        errors.Add($"이미지: {imageError}");
                        isCorrupted = true;
                    }

                    // [검증 2] 카탈로그 데이터 매칭 검증
                    if (!catalogData.ContainsKey(imageIndex))
                    {
                        errors.Add("카탈로그: 데이터 없음(미등록 이미지)");
                        isCorrupted = true;
                    }
                    else
                    {
                        // 카탈로그 데이터의 유효성 검증
                        var entry = catalogData[imageIndex];

                        if (double.IsNaN(entry.user_angle) || double.IsInfinity(entry.user_angle))
                        {
                            errors.Add("카탈로그: angle 비정상");
                            isCorrupted = true;
                        }

                        if (double.IsNaN(entry.user_throttle) || double.IsInfinity(entry.user_throttle))
                        {
                            errors.Add("카탈로그: throttle 비정상");
                            isCorrupted = true;
                        }

                        if (string.IsNullOrEmpty(entry.cam_image_array))
                        {
                            errors.Add("카탈로그: 이미지 경로 없음");
                            isCorrupted = true;
                        }
                    }

                    // 깨진 파일 적발 시
                    if (isCorrupted)
                    {
                        corruptedFileIndices.Add(i); // listImages의 인덱스 저장
                        corruptedCount++;

                        string errorDetail = $"{fileName}: {string.Join(", ", errors)}";
                        errorMessages.Add(errorDetail);
                    }

                    // 💡 500장마다 UI 멈춤 방지
                    if (i % 500 == 0 && i > 0)
                    {
                        Application.DoEvents(); // UI 응답 유지
                    }
                }

                // 리스트박스 전체 강제 리프레시
                listImages.Invalidate();

                // 결과 메시지
                if (corruptedCount > 0)
                {
                    MessageBox.Show(
                        $"총 {totalFiles}개 파일 중 {corruptedCount}개의 깨진 파일이 발견되었습니다.\n\n" +
                        $"깨진 파일은 프레임 목록에서 빨간색으로 표시됩니다.",
                        "이상 탐지 완료",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(
                        $"모든 파일({totalFiles}개)이 정상입니다.",
                        "이상 탐지 완료",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"이상 탐지 중 오류가 발생했습니다:\n{ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnDetect.Enabled = true; // 버튼 다시 활성화
                Cursor = Cursors.Default;
            }
        }

        // listImages 항목 그리기 (깨진 파일을 빨간색으로 표시)
        private void listImages_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            // 1. 시스템 기본 배경색으로 칠하기
            e.DrawBackground();

            // 깨진 파일인지 확인
            bool isCorrupted = corruptedFileIndices.Contains(e.Index);
            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

            // 2. 텍스트 색상 결정 (기존 테마 유지)
            Color textColor;
            if (isSelected)
            {
                // 선택된 항목
                textColor = isCorrupted ? Color.Red : Color.White;
            }
            else
            {
                // 선택되지 않은 항목
                textColor = isCorrupted ? Color.Red : Color.FromArgb(0, 191, 255);
            }

            // 3. 텍스트 그리기
            string text = listImages.Items[e.Index].ToString();
            using (SolidBrush textBrush = new SolidBrush(textColor))
            {
                e.Graphics.DrawString(text, e.Font, textBrush, e.Bounds);
            }

            // 4. 포커스 사각형 점선 그리기
            e.DrawFocusRectangle();
        }

        private void train_Click(object sender, EventArgs e)
        {
            label_progressai.Text = "진행률:";

            if (combo_model.SelectedIndex < 0)
            {
                MessageBox.Show("모델을 선택하세요");
                return;
            }


            string modelType = combo_model.SelectedItem.ToString().ToLower();

            RunPythonTrain(modelType);

            string scorePath = Path.Combine(baseDir, "score.json");

            if (!File.Exists(scorePath))
            {
                MessageBox.Show("평가 결과가 생성되지 않았습니다.");
                return;
            }

            string json =
                File.ReadAllText(scorePath);

            dynamic result =
                JsonConvert.DeserializeObject(json);

            label_score.Text =
                result["score"].ToString();
        }

        private const int BaseInterval = 100;
        private void comboBox_play_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_play.SelectedItem == null) return;

            // 현재 선택된 아이템 글자 (예: "x1.5배속")
            string selectedItemText = comboBox_play.SelectedItem.ToString();

            // "x"와 "배속"을 모두 지워서 숫자("1.5")만 남깁니다.
            string speedValue = selectedItemText.Replace("x", "").Replace("배속", "");

            // 숫자로 변환하여 타이머 Interval 계산
            if (double.TryParse(speedValue, out double speed))
            {
                // 공식: 기본 주기 / 배속
                // 예: x2.0배속 선택 시 -> 100 / 2.0 = 50ms (2배 빨라짐)
                timer1.Interval = (int)(BaseInterval / speed);
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            HelpForm helpForm = new HelpForm();
            helpForm.ShowDialog(this);
        }
        void LoadArenaThumbnails(int centerIndex)
        {
            if (imageFiles == null || imageFiles.Length == 0) return;

            flowLayout_arena.Controls.Clear();

            int start = Math.Max(0, centerIndex - 10);
            int end = Math.Min(imageFiles.Length - 1, centerIndex + 10);

            for (int i = start; i <= end; i++)
            {
                PictureBox thumb = new PictureBox();
                thumb.Size = new Size(160, 120);
                thumb.SizeMode = PictureBoxSizeMode.Zoom;
                thumb.BackColor = Color.FromArgb(7, 7, 15);
                thumb.Cursor = Cursors.Hand;
                thumb.Tag = i;
                thumb.BorderStyle = i == centerIndex
                    ? BorderStyle.Fixed3D
                    : BorderStyle.FixedSingle;

                try { thumb.Image = Image.FromFile(imageFiles[i]); }
                catch { }

                thumb.Click += (sender, e) =>
                {
                    int thumbIndex = (int)((PictureBox)sender).Tag;
                    SetCurrentIndex(thumbIndex);
                    LoadArenaThumbnails(thumbIndex);
                };

                flowLayout_arena.Controls.Add(thumb);
            }
        }

        private void picboxImage_Paint(object sender, PaintEventArgs e)
        {
            if (predictions.Count == 0)
                return;

            var p = predictions[currentFrame];

            using (Pen realPen = new Pen(Color.Lime, 4))
            using (Pen predPen = new Pen(Color.Red, 4))
            {
                DrawDriveLine(
                    e.Graphics,
                    p.real_angle,
                    p.real_throttle,
                    realPen
                );

                DrawDriveLine(
                    e.Graphics,
                    p.pred_angle,
                    p.pred_throttle,
                    predPen
                );
            }
        }

        private void btnAfterFrame_Click(object sender, EventArgs e)
        {
            if (predictions.Count == 0)
                return;

            currentFrame++;

            if (currentFrame >= predictions.Count)
                currentFrame = 0;

            ShowFrame(currentFrame);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadPredictions();
        }

        private void btnbeforeFrame_Click(object sender, EventArgs e)
        {
            if (predictions.Count == 0)
                return;

            currentFrame--;

            if (currentFrame < 0)
                currentFrame = predictions.Count - 1;

            ShowFrame(currentFrame);
        }

        private void timer_pilot_Tick(object sender, EventArgs e)
        {
            if (predictions.Count == 0)
                return;

            currentFrame++;

            if (currentFrame >= predictions.Count)
                currentFrame = 0;

            ShowFrame(currentFrame);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (predictions.Count == 0)
                return;

            if (isPlaying)
            {
                timer_pilot.Stop();
                isPlaying = false;
                btnPlay.Text = "재생";
            }
            else
            {
                timer_pilot.Start();
                isPlaying = true;
                btnPlay.Text = "정지";
            }
        }

        private void btn_stopTrain_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (trainProcess != null && !trainProcess.HasExited)
                {
                    // WSL bash에 Ctrl+C 전송
                    trainProcess.StandardInput.WriteLine("\x3");
                    trainProcess.StandardInput.Flush();

                    list_log.Items.Add($"[{DateTime.Now:HH:mm:ss}] 🛑 Ctrl+C 전송");

                    btn_train.Enabled = true;
                    btn_stopTrain.Enabled = false;
                }
            }
            catch
            {
                // fallback
                trainProcess?.Kill(true);
            }
        }
    }


    public class CatalogEntry
    {
        public int _index { get; set; }
        public string cam_image_array { get; set; }
        public double user_angle { get; set; }
        public double user_throttle { get; set; }
    }

    public class AutoScrollListBox : ListBox
    {
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public new bool UserAutoScroll { get; set; } = true;

        private const int WM_VSCROLL = 0x0115;
        private const int WM_MOUSEWHEEL = 0x020A;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_VSCROLL || m.Msg == WM_MOUSEWHEEL)
            {
                base.WndProc(ref m);

                int visibleItems = (int)(Height / ItemHeight);
                // 맨 아래에서 10칸 이내면 자동 스크롤 ON
                bool isNearBottom = (TopIndex >= Items.Count - visibleItems - 10);

                UserAutoScroll = isNearBottom;
                return;
            }
            base.WndProc(ref m);
        }
    }

    public class PredictionRecord
    {
        public string image { get; set; }

        public double real_angle { get; set; }
        public double real_throttle { get; set; }

        public double pred_angle { get; set; }
        public double pred_throttle { get; set; }
    }

}