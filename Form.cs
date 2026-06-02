using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;
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

        string[] imageFiles;
        int currentIndex = 0;

        string currentFolderPath = "";  // 현재 이미지 폴더 경로
        string trashFolderPath = "";    // 삭제된 이미지 보관 폴더 경로
        Stack<string> deletedFiles = new Stack<string>();   // 삭제된 파일 순서를 저장하는 스택
        List<string> deletedHistory = new List<string>();
        string backupFolderPath;    // 압축 전 원본 이미지를 보관하는 폴더 경로

        string historyPath;


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

        // catalog 데이터 저장할 딕셔너리
        Dictionary<int, CatalogEntry> catalogData = new Dictionary<int, CatalogEntry>();

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
            tabControl.ItemSize = new Size(610, 35);
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

            picImage.SizeMode = PictureBoxSizeMode.Zoom;
            picEdge.SizeMode = PictureBoxSizeMode.Zoom;

            listImages.ForeColor = Color.FromArgb(0, 191, 255);
            listImages.Font = new Font("Consolas", 9.5F, FontStyle.Regular);

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

            // 콤보박스
            cmbTrashList.BackColor = Color.FromArgb(18, 18, 32);
            cmbTrashList.ForeColor = Color.FromArgb(204, 204, 204);
            cmbTrashList.Font = new Font("Consolas", 10F);
            cmbTrashList.FlatStyle = FlatStyle.Flat;

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

            //6.TRACKBAR(순정 스타일 제거 후 네온 렌더링 세팅)

            trackBar_frame.BackColor = Color.FromArgb(13, 13, 24);
            trackBar_frame.TickStyle = TickStyle.None;
            trackBar_frame.ValueChanged += (sender, e) => trackBar_frame.Invalidate();

            // TrackBar에 더블버퍼링 강제 적용
            // 트랙바 깜빡거림 완화 위함
            typeof(TrackBar)
                .GetProperty("DoubleBuffered",
                    System.Reflection.BindingFlags.NonPublic |
                    System.Reflection.BindingFlags.Instance)
                ?.SetValue(trackBar_frame, true);

            // 8. 학습 탭 하이테크 테마 디자인
            list_log.BackColor = Color.FromArgb(7, 7, 15);
            list_log.ForeColor = Color.FromArgb(0, 191, 255);
            list_log.BorderStyle = BorderStyle.FixedSingle;
            list_log.Font = new Font("Consolas", 9.5F, FontStyle.Regular);

            combo_model.FlatStyle = FlatStyle.Flat;
            combo_model.BackColor = Color.FromArgb(18, 18, 32);
            combo_model.ForeColor = Color.FromArgb(204, 204, 204);
            combo_model.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

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
            }

            // 데이터 경로 로드

            string imageFolderPath = Path.Combine(baseDir, "data", "images");

            historyPath = Path.Combine(baseDir, "deleted_history.json");

            trashFolderPath = Path.Combine(baseDir, "trash");

            backupFolderPath = Path.Combine(baseDir, "backup");

            Directory.CreateDirectory(backupFolderPath);

            Directory.CreateDirectory(trashFolderPath);

            LoadImageFolder(imageFolderPath);

            try
            {
                LoadCatalog();
                UpdateDataChart();
            }
            catch (Exception ex)
            {
                MessageBox.Show("카탈로그 로드 오류: " + ex.Message);
            }

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

            // 콤보박스
            combo_compare.BackColor = Color.FromArgb(18, 18, 32);
            combo_compare.ForeColor = Color.FromArgb(204, 204, 204);
            combo_compare.Font = new Font("Consolas", 15F);
            combo_compare.FlatStyle = FlatStyle.Flat;
            // 콤보박스 아이템 로드
            combo_compare.SelectedIndexChanged += (s, e) =>
            {
                if (combo_compare.SelectedIndex < 0) return;

                int idx = combo_compare.SelectedIndex;

                if (catalogData.ContainsKey(idx))
                {
                    var entry = catalogData[idx];
                    double realAngle = entry.user_angle;
                    double realThrottle = entry.user_throttle;

                    // 실제값 표시
                    label_compthroNum.Text = realThrottle.ToString("F3");
                    label_compangleNum.Text = realAngle.ToString("F3");

                    // ProgressBar 실제값
                    progre_compthro.Value = (int)(Math.Abs(realThrottle) * 100);
                    progre_compangle.Value = (int)((realAngle + 1) / 2 * 100);

                    // AI 예측값은 학습 완료 후 연동
                    // label_aithroNum.Text = aiThrottle.ToString("F3");
                    // label_aiangleNum.Text = aiAngle.ToString("F3");

                    // 오차 표시
                    // double diff = Math.Abs(realAngle - aiAngle);
                    // label_ocha.Text = $"오차: {diff:F3}";
                    // label_ocha.ForeColor = diff < 0.1
                    //     ? Color.FromArgb(102, 187, 106)
                    //     : Color.FromArgb(239, 83, 80);
                }
            };

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

        void SetCurrentIndex(int index) // 인덱스 설정 및 이미지 로드
        {
            if (imageFiles.Length == 0)
                return;

            if (index < 0)
                index = imageFiles.Length - 1;

            if (index >= imageFiles.Length)
                index = 0;

            currentIndex = index;

            // 이벤트 중복 방지
            isScrolling = true;

            listImages.SelectedIndex = currentIndex;
            trackBar_frame.Value = currentIndex;

            isScrolling = false;

            LoadImage();

            // 데이터 연동 및 계기판 네온 바늘 실시간 호출 연계
            if (catalogData.ContainsKey(currentIndex))
            {
                var entry = catalogData[currentIndex];
                // 디버그용 임시 추가
                System.Diagnostics.Debug.WriteLine($"index:{currentIndex} throttle:{entry.user_throttle} angle:{entry.user_angle}");
                label_throttle.Text = $"{entry.user_throttle:F3}";
                label_angle.Text = $"{entry.user_angle:F3}";

                //데이터 인덱스 변화에 맞춰 계기판 바늘을 갱신합니다.
                DrawDashboardNeedles(entry.user_throttle, entry.user_angle);
            }
            else
            {
                // 키가 없는 경우 확인
                System.Diagnostics.Debug.WriteLine($"catalogData에 키 없음: {currentIndex}");
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

            picImage.Image = frame.ToBitmap();
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
        }

        private void RunPythonTrain(string modelType)
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "wsl";

            psi.Arguments = $"train.py --data data --model {modelType} --epochs 10";

            psi.WorkingDirectory = baseDir;

            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.CreateNoWindow = true;

            Process p = Process.Start(psi);

            string output = p.StandardOutput.ReadToEnd();
            string error = p.StandardError.ReadToEnd();

            p.WaitForExit();

            list_log.Items.Add(output);
            if (!string.IsNullOrEmpty(error))
                list_log.Items.Add("ERROR: " + error);

            RunPythonEvaluate();
        }
        private void RunPythonEvaluate()
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "python";
            psi.Arguments = "evaluate.py";

            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            psi.CreateNoWindow = true;

            Process p = Process.Start(psi);
            p.WaitForExit();
        }

        private void listImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isScrolling)
                return;

            SetCurrentIndex(listImages.SelectedIndex);

            // 썸네일 강조
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
            if (isScrolling)
                return;

            SetCurrentIndex(trackBar_frame.Value);
        }

        private void btn_before_Click(object sender, EventArgs e)
        {
            SetCurrentIndex(currentIndex - 1);
        }

        private void btn_imgnext_Click(object sender, EventArgs e)
        {
            SetCurrentIndex(currentIndex + 1);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SetCurrentIndex(currentIndex + 1);
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled) { timer1.Stop(); btnPlay.Text = "▶"; }
            else { timer1.Start(); btnPlay.Text = "정지"; }
        }

        private void btn_train_Click(object sender, EventArgs e)
        {
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

            Directory.CreateDirectory(wbImagesPath);

            btn_train.Enabled = false;
            list_log.Items.Add($"[{DateTime.Now:HH:mm:ss}] 🔍 원본 이미지 스캔 시작...");
            list_log.Items.Add($"[{DateTime.Now:HH:mm:ss}] 📊 카탈로그 레코드: {catalogData.Count}개");

            try
            {
                int successCount = 0;
                int skippedCount = 0;
                int recordIndex = 0;
                List<Dictionary<string, object>> jsonRecords = new List<Dictionary<string, object>>();
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

                        if (angle == 0.0 || throttle <= 0.0)
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
                            ["_index"] = recordIndex,
                            ["_timestamp"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                            ["cam/image_array"] = $"images/{fileName}",
                            ["cam/image_wb"] = $"wbimages/{fileName}",
                            ["user/angle"] = angle,
                            ["user/throttle"] = throttle
                        };

                        jsonRecords.Add(record);
                        recordIndex++;
                        successCount++;

                        // 진행률 표시 (10%마다)
                        processedCount++;
                        int updateInterval = Math.Max(1, totalImages / 10);
                        if (processedCount % updateInterval == 0)
                        {
                            int percentage = (int)((double)processedCount / totalImages * 100);
                            list_log.Items.Add($"[{DateTime.Now:HH:mm:ss}] 📊 진행률: {percentage}% ({processedCount}/{totalImages}장)");
                            list_log.SelectedIndex = list_log.Items.Count - 1;
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
                list_log.SelectedIndex = list_log.Items.Count - 1;

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
            finally
            {
                btn_train.Enabled = true;
            }

            if (combo_model.SelectedIndex < 0)
            {
                MessageBox.Show("모델을 선택하세요");
                return;
            }

            string modelType = combo_model.SelectedItem.ToString().ToLower();

            RunPythonTrain(modelType);

            while (!File.Exists("score.json"))
            {
                Application.DoEvents();
            }

            string json = File.ReadAllText("score.json");
            dynamic result = JsonConvert.DeserializeObject(json);

            label_score.Text = result["score"].ToString();
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
                    currentIndex = (int)((PictureBox)sender).Tag;
                    listImages.SelectedIndex = currentIndex;
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

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (imageFiles.Length == 0)
                return;

            // 현재 표시 이미지 해제
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

            string sourcePath = imageFiles[currentIndex];

            string fileName = Path.GetFileName(sourcePath);

            string destPath = Path.Combine(trashFolderPath, fileName);

            deletedFiles.Push(destPath);
            deletedHistory.Add(destPath);
            SaveDeleteHistory();

            // 이미 trash에 같은 파일이 있으면 삭제
            if (File.Exists(destPath))
            {
                File.Delete(destPath);
            }

            File.Move(sourcePath, destPath);

            ReloadCurrentFolder();
        }

        private void btn_restore_Click(object sender, EventArgs e)
        {
            string lastFile;

            // 실행 중 삭제 기록 사용
            if (deletedFiles.Count > 0)
            {
                lastFile = deletedFiles.Pop();

                // JSON 기록에서도 제거
                if (deletedHistory.Count > 0)
                {
                    deletedHistory.RemoveAt(deletedHistory.Count - 1);
                    SaveDeleteHistory();
                }
            }
            else
            {
                // JSON 기록 사용
                if (deletedHistory.Count == 0)
                {
                    MessageBox.Show("복구할 파일 없음");
                    return;
                }

                lastFile = deletedHistory.Last();

                deletedHistory.RemoveAt(deletedHistory.Count - 1);

                SaveDeleteHistory();
            }

            string fileName = Path.GetFileName(lastFile);

            string restorePath = Path.Combine(currentFolderPath, fileName);

            // 이미 있으면 덮어쓰기 위해 삭제
            if (File.Exists(restorePath))
            {
                File.Delete(restorePath);
            }

            File.Move(lastFile, restorePath);

            ReloadCurrentFolder();
        }

        private void btnSetStart_Click(object sender, EventArgs e)
        {
            SetCurrentIndex(0);
        }

        private void btnSetEnd_Click(object sender, EventArgs e)
        {
            SetCurrentIndex(imageFiles.Length - 1);
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

        private void progressDelete_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }


    public class CatalogEntry
    {
        public int _index { get; set; }
        public string cam_image_array { get; set; }
        public double user_angle { get; set; }
        public double user_throttle { get; set; }
    }
}