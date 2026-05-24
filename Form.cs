using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Microsoft.VisualBasic.ApplicationServices;

namespace Datamanager
{

    public partial class Form1 : Form
    {
        private bool isSliderDragging = false;

        string[] imageFiles;
        int currentIndex = 0;

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
        public Form1()
        {
            InitializeComponent();

            // ====================================================================
            // 0. GDI+ 안티앨리어싱 설정 (그래픽 품질 최상으로 설정)
            // ====================================================================
            // 최적의 성능과 가독성을 위해 폼 자체의 기본 설정을 변경합니다.
            this.Visible = true;
            this.Opacity = 100;

            // ====================================================================
            // 1. FORM 및 기본 테마 속성 업그레이드
            // ====================================================================
            this.BackColor = Color.FromArgb(13, 13, 24);      // #0d0d18 (딥 스페이스 네이비)
            this.ForeColor = Color.FromArgb(204, 204, 204);   // #cccccc (실버 그레이)
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular);

            // ====================================================================
            // 2. TAB CONTROL 하이테크 스타일 매칭
            // ====================================================================
            tabControl.ItemSize = new Size(610, 35);           
            tabControl.Appearance = TabAppearance.FlatButtons;
            tabControl.SizeMode = TabSizeMode.Fixed;

            // 탭 페이지 배경색 일치
            tab_data.BackColor = Color.FromArgb(13, 13, 24);
            tab_train.BackColor = Color.FromArgb(13, 13, 24);

            // ====================================================================
            // 3. PICTUREBOX & LISTBOX (어두운 톤 깊이감 및 테두리 글로우 효과)
            // ====================================================================
            // [UI 담당 치트키 1] PictureBox에 네온 글로우 테두리를 그립니다.
            void StyleMonitorControl(Control control)
            {
                control.BackColor = Color.FromArgb(7, 7, 15);      // 내부 더 어둡게
                                                                   // BorderStyle을 None으로 하고 Paint에서 글로우를 직접 그립니다.
                if (control is PictureBox pb) pb.BorderStyle = BorderStyle.None;
                if (control is ListBox lb) lb.BorderStyle = BorderStyle.None;

                control.Paint += (sender, e) =>
                {
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
                    {
                        int r = 10; // 모서리 깎기
                        path.AddArc(0, 0, r, r, 180, 90);
                        path.AddArc(control.Width - r - 1, 0, r, r, 270, 90);
                        path.AddArc(control.Width - r - 1, control.Height - r - 1, r, r, 0, 90);
                        path.AddArc(0, control.Height - r - 1, r, r, 90, 90);
                        path.CloseFigure();

                        control.Region = new Region(path); // 둥글게 자르기

                        // 네온 글로우 테두리 (사이언 블루 계열)
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

            // ====================================================================
            // 4. DIGITAL DASHBOARD LABELS (속도, 앵글 텍스트 대시보드화)
            // ====================================================================
            // 속도 레이블 (네온 그린 글로우 효과)
            text_throttle.BackColor = Color.FromArgb(13, 13, 24);
            text_throttle.ForeColor = Color.FromArgb(32, 201, 151);
            text_throttle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);

            // 앵글 레이블 (네온 블루 글로우 효과)
            text_angle.BackColor = Color.FromArgb(13, 13, 24);
            text_angle.ForeColor = Color.FromArgb(79, 195, 247);
            text_angle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);

            // ====================================================================
            // 5. BUTTONS FLAT & CYBER COLOR STYLING (네온 글로우 버튼 고도화)
            // ====================================================================
            void StyleButton(Button btn, Color borderColor)
            {
                btn.BackColor = Color.FromArgb(13, 13, 24);
                btn.ForeColor = borderColor;
                btn.FlatStyle = FlatStyle.Flat;

                //Transparent 대신 0 지정을 통해 기본 테두리를 아예 두께 0으로 없애버립니다.
                btn.FlatAppearance.BorderSize = 0;

                // 마우스 이벤트에 따른 색상 변화
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(20, 20, 40);
                btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(7, 7, 15);

                btn.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
                btn.Cursor = Cursors.Hand;

                // 버튼 전체에 부드러운 둥근 모서리와 강한 네온 글로우를 그립니다.
                btn.Paint += (sender, e) => {
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
                    {
                        int r = 8; // 둥글게 깎기 반지름
                        path.AddArc(0, 0, r, r, 180, 90);
                        path.AddArc(btn.Width - r - 1, 0, r, r, 270, 90);
                        path.AddArc(btn.Width - r - 1, btn.Height - r - 1, r, r, 0, 90);
                        path.AddArc(0, btn.Height - r - 1, r, r, 90, 90);
                        path.CloseFigure();

                        // 1. 강한 글로우 배경 그리기 (선택 사항 - 입체감 부여)
                        using (System.Drawing.Drawing2D.PathGradientBrush glowBrush = new System.Drawing.Drawing2D.PathGradientBrush(path))
                        {
                            glowBrush.CenterColor = Color.FromArgb(50, borderColor);
                            glowBrush.SurroundColors = new Color[] { Color.FromArgb(0, borderColor) };
                            e.Graphics.FillPath(glowBrush, path);
                        }

                        // 2. 형님이 원하시던 부드러운 네온 테두리 선 직접 그리기
                        using (Pen pen = new Pen(borderColor, 1.8f))
                        {
                            e.Graphics.DrawPath(pen, path);
                        }
                    }
                };
            }

            // 각 기능 버튼별 아이덴티티 컬러 매칭
            StyleButton(btn_delete, Color.FromArgb(239, 83, 80));       // 핫 레드 - 삭제
            StyleButton(btn_restore, Color.FromArgb(140, 140, 160));    // 차분한 그레이 - 복구
            StyleButton(btnPlay, Color.FromArgb(102, 187, 106));       // 메트릭스 그린 - 재생/정지
            StyleButton(btn_openfolder, Color.FromArgb(204, 204, 204)); // 기본 실버 - 폴더 열기
            StyleButton(btnSetStart, Color.FromArgb(178, 223, 219));    // 소프트 민트 - 범위 시작
            StyleButton(btnSetEnd, Color.FromArgb(178, 223, 219));      // 소프트 민트 - 범위 끝
            StyleButton(btn_changquality, Color.FromArgb(255, 167, 38)); // 웜 오렌지 - 화질
            StyleButton(btn_train, Color.FromArgb(79, 195, 247));       // 사이언 블루 - AI 학습
            StyleButton(btn_stopTrain, Color.FromArgb(239, 83, 80));   // 핫 레드 - 학습 중단
            StyleButton(btn_before, Color.FromArgb(102, 187, 106));    // 그린 - 이전 프레임
            StyleButton(btn_imgnext, Color.FromArgb(102, 187, 106));   // 그린 - 다음 프레임

            // ====================================================================
            // 6. TRACKBAR (하이테크 네온 스타일로 드로잉)
            // ====================================================================
            trackBar_frame.BackColor = Color.FromArgb(13, 13, 24);
            trackBar_frame.TickStyle = TickStyle.None;
            // [UI 담당 치트키 2] TrackBar에 네온 트랙과 핸들을 그립니다.
            trackBar_frame.Paint += (sender, e) => {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                // 트랙 (어두운 배경)
                using (Pen trackPen = new Pen(Color.FromArgb(40, 40, 60), 4f))
                {
                    e.Graphics.DrawLine(trackPen, 10, trackBar_frame.Height / 2, trackBar_frame.Width - 10, trackBar_frame.Height / 2);
                }
                // 현재 위치 핸들 (네온 사이언 블루 글로우)
                int thumbWidth = 14;
                int thumbHeight = 20;
                int thumbX = (int)((float)trackBar_frame.Value / trackBar_frame.Maximum * (trackBar_frame.Width - thumbWidth)) + thumbWidth / 2;
                Rectangle thumbRect = new Rectangle(thumbX - thumbWidth / 2, trackBar_frame.Height / 2 - thumbHeight / 2, thumbWidth, thumbHeight);

                using (System.Drawing.Drawing2D.GraphicsPath thumbPath = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    int r = 4;
                    thumbPath.AddArc(thumbRect.X, thumbRect.Y, r, r, 180, 90);
                    thumbPath.AddArc(thumbRect.Right - r, thumbRect.Y, r, r, 270, 90);
                    thumbPath.AddArc(thumbRect.Right - r, thumbRect.Bottom - r, r, r, 0, 90);
                    thumbPath.AddArc(thumbRect.X, thumbRect.Bottom - r, r, r, 90, 90);
                    thumbPath.CloseFigure();

                    using (System.Drawing.Drawing2D.PathGradientBrush glowBrush = new System.Drawing.Drawing2D.PathGradientBrush(thumbPath))
                    {
                        glowBrush.CenterColor = Color.FromArgb(79, 195, 247);
                        glowBrush.SurroundColors = new Color[] { Color.FromArgb(0, 79, 195, 247) };
                        e.Graphics.FillPath(glowBrush, thumbPath);
                    }
                    using (Pen pen = new Pen(Color.FromArgb(79, 195, 247), 1.5f))
                    {
                        e.Graphics.DrawPath(pen, thumbPath);
                    }
                }
            };

            // ====================================================================
            // 7. DASHBOARD BACKGROUND GAGE (계기판 투명 동화 및 네온 필터)
            // ====================================================================
            string gagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "2번.png");
            if (File.Exists(gagePath))
            {
                Bitmap gageImg = new Bitmap(gagePath);
                Bitmap result = new Bitmap(picture_Gage.Width, picture_Gage.Height);

                using (Graphics g = Graphics.FromImage(result))
                {
                    g.Clear(Color.FromArgb(13, 13, 24)); // 폼 배경색과 동화
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    // 계기판 그래픽 위에 네온 필터 효과 추가 (사이언 블루 광원 효과 원인 해결)
                    using (System.Drawing.Drawing2D.GraphicsPath circlePath = new System.Drawing.Drawing2D.GraphicsPath())
                    {
                        circlePath.AddEllipse(new Rectangle(0, 0, picture_Gage.Width, picture_Gage.Height));

                        // 괄호 안에 circlePath를 명시하여 CS0121 오류를 해결합니다.
                        using (System.Drawing.Drawing2D.PathGradientBrush glowBrush = new System.Drawing.Drawing2D.PathGradientBrush(circlePath))
                        {
                            // SetSurroundColors 대신 SurroundColors 속성에 배열을 직접 대입하여 CS1061 오류를 해결합니다.
                            glowBrush.SurroundColors = new Color[] { Color.FromArgb(0, 79, 195, 247) };
                            glowBrush.CenterColor = Color.FromArgb(30, 79, 195, 247); // 은은한 사이언 블루 글로우
                            glowBrush.FocusScales = new PointF(0.8f, 0.8f);
                            g.FillPath(glowBrush, circlePath);
                        }
                    }
                    g.DrawImage(gageImg, 0, 0, picture_Gage.Width, picture_Gage.Height);
                }

                picture_Gage.Image = result;
                picture_Gage.SizeMode = PictureBoxSizeMode.Normal;
                picture_Gage.BackColor = Color.Transparent;
            }

            string imageFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images"); // 실행 파일 기준으로 images 폴더 경로 생성

            if (Directory.Exists(imageFolderPath))
            {
                imageFiles = Directory.GetFiles(imageFolderPath, "*.jpg");
            }
            else
            {
                MessageBox.Show("images 폴더를 찾을 수 없습니다. 속성창에서 '출력 디렉터리로 복사'를 확인하세요.");
                imageFiles = new string[0];
            }

            foreach (string file in imageFiles)   // 이미지 파일을 리스트에 추가
            {
                listImages.Items.Add(
                    Path.GetFileName(file)
                );
            }

            if (listImages.Items.Count > 0)
            {
                listImages.SelectedIndex = 0;
            }
            // 카탈로그 로드
            try
            {
                LoadCatalog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("카탈로그 로드 오류: " + ex.Message);
            }
            trackBar_frame.Minimum = 0; //트랙바 초기화 코드
            trackBar_frame.Maximum = imageFiles.Length - 1;
            trackBar_frame.Value = 0;
        }

        void LoadImage()
        {
            Mat frame = CvInvoke.Imread(
                imageFiles[currentIndex]
            );

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
            // 전처리용 Mat
            Mat edge = new Mat();

            Mat hsv = new Mat();

            CvInvoke.CvtColor(
                frame,
                hsv,
                ColorConversion.Bgr2Hsv
            );

            Mat whiteMask = new Mat();

            CvInvoke.InRange(
                hsv,
                new ScalarArray(new MCvScalar(0, 0, 160)),  // 흰색 범위 하한
                new ScalarArray(new MCvScalar(180, 80, 255)),   // 흰색 범위 상한
                whiteMask
            );

            int centerX = frame.Width / 2;

            List<Point> leftPoints =
                new List<Point>();

            List<Point> rightPoints =
                new List<Point>();

            // 엣지 검출
            CvInvoke.Canny(
                whiteMask,
                edge,
                50,
                150
            );

            Mat mask = new Mat(     // 인식 영역
                edge.Size,
                DepthType.Cv8U,
                1
            );
            mask.SetTo(new MCvScalar(0));

            Point[] points =    // 사다리꼴 모양
            {
                new Point(100, edge.Rows),
                new Point(250, edge.Rows / 2),
                new Point(edge.Cols - 250, edge.Rows / 2),
                new Point(edge.Cols - 100, edge.Rows)
            };

            VectorOfPoint polygon =
                new VectorOfPoint(points);

            CvInvoke.FillConvexPoly(
                mask,
                polygon,
                new MCvScalar(255)
            );

            Mat roiEdge = new Mat();

            CvInvoke.BitwiseAnd(
                edge,
                mask,
                roiEdge
            );

            // 직선 검출
            LineSegment2D[] lines =
                CvInvoke.HoughLinesP(
                    roiEdge,
                    1,
                    Math.PI / 180,
                    15, // threshold
                    15, // minLineLength
                    10  // maxGap
                );

            // 검출된 선 그리기
            foreach (LineSegment2D line in lines)
            {
                double dx = line.P2.X - line.P1.X;
                double dy = line.P2.Y - line.P1.Y;

                if (dx == 0)
                    continue;

                double slope = dy / dx;

                Point p1 = line.P1;
                Point p2 = line.P2;

                // 왼쪽 차선
                if (slope < -0.3 &&
                    p1.X < centerX &&
                    p2.X < centerX)
                {
                    leftPoints.Add(p1);
                    leftPoints.Add(p2);
                }

                // 오른쪽 차선
                else if (slope > 0.3 &&
                    p1.X > centerX &&
                    p2.X > centerX)
                {
                    rightPoints.Add(p1);
                    rightPoints.Add(p2);
                }
            }

            DrawFitLine(
                frame,
                leftPoints,
                new MCvScalar(255, 0, 0),
                true
            );

            DrawFitLine(
                frame,
                rightPoints,
                new MCvScalar(0, 0, 255),
                false
            );

            if (leftDetected && rightDetected)
            {
                bottomWidth =
                    rightBottomX - leftBottomX;

                topWidth =
                    rightTopX - leftTopX;
            }

            if (leftDetected && !rightDetected) // 한쪽 차선이 안 보이는 경우
            {
                int fakeRightBottom =
                    leftBottomX + bottomWidth;

                int fakeRightTop =
                    leftTopX + topWidth;

                CvInvoke.Line(
                    frame,
                    new Point(fakeRightBottom, frame.Rows),
                    new Point(fakeRightTop, frame.Rows / 2),
                    new MCvScalar(0, 255, 255),
                    5
                );
            }
            else if (!leftDetected && rightDetected)
            {
                int fakeLeftBottom =
                    rightBottomX - bottomWidth;

                int fakeLeftTop =
                    rightTopX - topWidth;

                CvInvoke.Line(
                    frame,
                    new Point(fakeLeftBottom, frame.Rows),
                    new Point(fakeLeftTop, frame.Rows / 2),
                    new MCvScalar(0, 255, 255),
                    3
                );
            }

            //if (leftDetected && rightDetected)
            //{
            //    int newWidth =
            //        rightLaneX - leftLaneX;

            //    if (newWidth > 200 &&
            //        newWidth < 500)
            //    {
            //        laneWidth = newWidth;
            //    }
            //}

            // 마지막 가로선
            //CvInvoke.Line(
            //    frame,
            //    new Point(leftLaneX, frame.Rows - 50),
            //    new Point(rightLaneX, frame.Rows - 50),
            //    new MCvScalar(0, 255, 255),
            //    3
            //);

            //txtLeftLaneX.Text = $"Left : {leftBottomX}";
            //txtRightLaneX.Text = $"Right : {rightBottomX}";
            //txtLaneWidth.Text = $"Width : {topWidth}";


            // 출력
            picImage.Image = frame.ToBitmap();
            picEdge.Image = roiEdge.ToBitmap();
        }

        void DrawFitLine(Mat frame, List<Point> points, MCvScalar color, bool isLeft)
        {
            if (points.Count < 2)
                return;

            PointF[] pts =
                points.Select(p =>
                    new PointF(p.X, p.Y)
                ).ToArray();

            VectorOfPointF vec =
                new VectorOfPointF(pts);

            Mat line = new Mat();

            CvInvoke.FitLine(
                vec,
                line,
                DistType.L2,
                0,
                0.01,
                0.01
            );

            float[] data = new float[4];

            line.CopyTo(data);

            float vx = data[0];
            float vy = data[1];
            float x = data[2];
            float y = data[3];

            if (Math.Abs(vy) < 0.0001)
                return;

            int y1 = frame.Rows;
            int y2 = frame.Rows / 2;

            int x1 = (int)(x + (y1 - y) * vx / vy);
            int x2 = (int)(x + (y2 - y) * vx / vy);

            if (isLeft) // 왼쪽
            {
                if (leftX1Ema == 0)
                {
                    leftX1Ema = x1;
                    leftX2Ema = x2;
                }

                leftX1Ema =
                    alpha * x1 +
                    (1 - alpha) * leftX1Ema;

                leftX2Ema =
                    alpha * x2 +
                    (1 - alpha) * leftX2Ema;

                x1 = (int)leftX1Ema;
                x2 = (int)leftX2Ema;
            }
            else    // 오른쪽
            {
                if (rightX1Ema == 0)
                {
                    rightX1Ema = x1;
                    rightX2Ema = x2;
                }

                rightX1Ema =
                    alpha * x1 +
                    (1 - alpha) * rightX1Ema;

                rightX2Ema =
                    alpha * x2 +
                    (1 - alpha) * rightX2Ema;

                x1 = (int)rightX1Ema;
                x2 = (int)rightX2Ema;
            }

            if (isLeft)
            {
                leftBottomX = x1;
                leftTopX = x2;
                leftDetected = true;
            }
            else
            {
                rightBottomX = x1;
                rightTopX = x2;
                rightDetected = true;
            }


            CvInvoke.Line(
                frame,
                new Point(x1, y1),
                new Point(x2, y2),
                color,
                5
            );
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {


        }



        private void button3_Click(object sender, EventArgs e)
        {
            currentIndex--;

            if (currentIndex < 0)
                currentIndex = imageFiles.Length - 1;

            listImages.SelectedIndex =
                currentIndex;
        }

        private void panelCustomSlider_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void panelCustomSlider_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void panelCustomSlider_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void panelCustomSlider_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            currentIndex++;

            if (currentIndex >= imageFiles.Length)
            {
                currentIndex = 0;
            }

            listImages.SelectedIndex =
                currentIndex;
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Stop();
                btnPlay.Text = "재생";
            }
            else
            {
                timer1.Start();
                btnPlay.Text = "정지";
            }
        }

        private void listImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentIndex =
                listImages.SelectedIndex;
            trackBar_frame.Value = currentIndex;

            LoadImage();

            // catalog 데이터 연동
            if (catalogData.ContainsKey(currentIndex))
            {
                var entry = catalogData[currentIndex];
                text_throttle.Text = $"{entry.user_throttle:F3}";
                text_angle.Text = $"{entry.user_angle:F3}";
            }
        }

        private void btn_imgnext_Click(object sender, EventArgs e)
        {
            currentIndex++;

            if (currentIndex >= imageFiles.Length)
                currentIndex = 0;

            listImages.SelectedIndex =
                currentIndex;
        }
        // catalog 데이터 저장할 딕셔너리
        Dictionary<int, CatalogEntry> catalogData = new Dictionary<int, CatalogEntry>();

        // catalog 파일 읽는 함수
        void LoadCatalog()
        {
            // 프로젝트 파일(.csproj) 위치 기준으로 data 폴더 찾기
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string dataPath = Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..", "data"));
            //예외처리: data 폴더가 없을 때
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

        private void trackBar_frame_Scroll(object sender, EventArgs e)
        {
            currentIndex = trackBar_frame.Value;
            listImages.SelectedIndex = currentIndex;
        }

        private void btn_train_Click(object sender, EventArgs e)
        {
            // catalog 데이터에서 angle=0 이거나 throttle<=0인 데이터는 학습에서 제외하도록 필터링
            var validTrainingData = catalogData
                .Where(entry => entry.Value.user_angle != 0 && entry.Value.user_throttle > 0)
                .ToList();

        }
    }
    // 데이터 구조 정의
    public class CatalogEntry
    {
        public int _index { get; set; }
        public string cam_image_array { get; set; }
        public double user_angle { get; set; }
        public double user_throttle { get; set; }
    }
}
