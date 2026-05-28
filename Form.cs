using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.VisualBasic;

namespace Datamanager
{
    public partial class Form1 : Form
    {
        private bool isSliderDragging = false;

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
                btn.FlatAppearance.BorderSize = 0;

                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(20, 20, 40);
                btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(7, 7, 15);

                btn.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
                btn.Cursor = Cursors.Hand;

                btn.Paint += (sender, e) =>
                {
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
                    {
                        int r = 8;
                        path.AddArc(0, 0, r, r, 180, 90);
                        path.AddArc(btn.Width - r - 1, 0, r, r, 270, 90);
                        path.AddArc(btn.Width - r - 1, btn.Height - r - 1, r, r, 0, 90);
                        path.AddArc(0, btn.Height - r - 1, r, r, 90, 90);
                        path.CloseFigure();

                        using (System.Drawing.Drawing2D.PathGradientBrush glowBrush = new System.Drawing.Drawing2D.PathGradientBrush(path))
                        {
                            glowBrush.CenterColor = Color.FromArgb(50, borderColor);
                            glowBrush.SurroundColors = new Color[] { Color.FromArgb(0, borderColor) };
                            e.Graphics.FillPath(glowBrush, path);
                        }

                        using (Pen pen = new Pen(borderColor, 1.8f))
                        {
                            e.Graphics.DrawPath(pen, path);
                        }
                    }
                };
            }

            StyleButton(btn_delete, Color.FromArgb(239, 83, 80));
            StyleButton(btn_restore, Color.FromArgb(140, 140, 160));
            StyleButton(btnPlay, Color.FromArgb(102, 187, 106));
            StyleButton(btn_openfolder, Color.FromArgb(204, 204, 204));
            StyleButton(btnSetStart, Color.FromArgb(178, 223, 219));
            StyleButton(btnSetEnd, Color.FromArgb(178, 223, 219));
            StyleButton(btn_changquality, Color.FromArgb(255, 167, 38));
            StyleButton(btn_train, Color.FromArgb(79, 195, 247));
            StyleButton(btn_stopTrain, Color.FromArgb(239, 83, 80));
            StyleButton(btn_before, Color.FromArgb(102, 187, 106));
            StyleButton(btn_imgnext, Color.FromArgb(102, 187, 106));

            //6.TRACKBAR(순정 스타일 제거 후 네온 렌더링 세팅)

            trackBar_frame.BackColor = Color.FromArgb(13, 13, 24);
            trackBar_frame.TickStyle = TickStyle.None;
            trackBar_frame.ValueChanged += (sender, e) => trackBar_frame.Invalidate();

            // 8. 학습 탭 하이테크 테마 디자인
            list_log.BackColor = Color.FromArgb(7, 7, 15);
            list_log.ForeColor = Color.FromArgb(0, 191, 255);
            list_log.BorderStyle = BorderStyle.FixedSingle;
            list_log.Font = new Font("Consolas", 9.5F, FontStyle.Regular);

            combo_model.FlatStyle = FlatStyle.Flat;
            combo_model.BackColor = Color.FromArgb(18, 18, 32);
            combo_model.ForeColor = Color.FromArgb(204, 204, 204);
            combo_model.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            if (chart_loss != null)
            {
                chart_loss.BackColor = Color.FromArgb(13, 13, 24);

                foreach (var area in chart_loss.ChartAreas)
                {
                    area.BackColor = Color.FromArgb(7, 7, 15);
                    area.AxisX.LabelStyle.ForeColor = Color.FromArgb(204, 204, 204);
                    area.AxisY.LabelStyle.ForeColor = Color.FromArgb(204, 204, 204);

                    area.AxisX.MajorGrid.LineColor = Color.FromArgb(40, 40, 60);
                    area.AxisY.MajorGrid.LineColor = Color.FromArgb(40, 40, 60);

                    area.AxisX.LineColor = Color.FromArgb(79, 195, 247);
                    area.AxisY.LineColor = Color.FromArgb(79, 195, 247);
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

            label_throttle.Location = new Point(label_throttle.Left - picture_Gage.Left, label_throttle.Top - picture_Gage.Top);
            label_angle.Location = new Point(label_angle.Left - picture_Gage.Left, label_angle.Top - picture_Gage.Top);

            label_throttle.BackColor = Color.Transparent;
            label_angle.BackColor = Color.Transparent;

            // imageFiles 로드 후 아래에 추가
            LoadThumbnails(currentIndex);
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
                label_throttle.Text = $"{entry.user_throttle:F3}";
                label_angle.Text = $"{entry.user_angle:F3}";

                //데이터 인덱스 변화에 맞춰 계기판 바늘을 갱신합니다.
                DrawDashboardNeedles(entry.user_throttle, entry.user_angle);
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

        void ProcessFrame(Mat frame)
        {
            leftDetected = false;
            rightDetected = false;
            Mat edge = new Mat();
            Mat hsv = new Mat();

            CvInvoke.CvtColor(frame, hsv, ColorConversion.Bgr2Hsv);

            Mat whiteMask = new Mat();
            CvInvoke.InRange(
                hsv,
                new ScalarArray(new MCvScalar(0, 0, 160)),
                new ScalarArray(new MCvScalar(180, 80, 255)),
                whiteMask
            );

            int centerX = frame.Width / 2;
            List<Point> leftPoints = new List<Point>();
            List<Point> rightPoints = new List<Point>();

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

            VectorOfPoint polygon = new VectorOfPoint(points);
            CvInvoke.FillConvexPoly(mask, polygon, new MCvScalar(255));

            Mat roiEdge = new Mat();
            CvInvoke.BitwiseAnd(edge, mask, roiEdge);

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

        void LoadCatalog()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string dataPath = Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..", "data"));
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
            if (timer1.Enabled) { timer1.Stop(); btnPlay.Text = "재생"; }
            else { timer1.Start(); btnPlay.Text = "정지"; }
        }

        private void btn_train_Click(object sender, EventArgs e)
        {
            var validTrainingData = catalogData
                .Where(entry => entry.Value.user_angle != 0 && entry.Value.user_throttle > 0)
                .ToList();
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
        private void panelCustomSlider_MouseDown(object sender, MouseEventArgs e) { }
        private void panelCustomSlider_MouseMove(object sender, MouseEventArgs e) { }
        private void panelCustomSlider_MouseUp(object sender, MouseEventArgs e) { }
        private void panelCustomSlider_Paint(object sender, PaintEventArgs e) { }
        private void chart1_Click(object sender, EventArgs e) { }
        private void button7_Click(object sender, EventArgs e) { }

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
    }


    public class CatalogEntry
    {
        public int _index { get; set; }
        public string cam_image_array { get; set; }
        public double user_angle { get; set; }
        public double user_throttle { get; set; }
    }
}