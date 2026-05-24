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
            //UI 디자인
            // Form1 속성
            this.BackColor = Color.FromArgb(13, 13, 24);      // #0d0d18
            this.ForeColor = Color.FromArgb(204, 204, 204);   // #cccccc
            this.Font = new Font("Courier New", 9F);

            // 탭 컨트롤 속성
            tabControl.BackColor = Color.FromArgb(8, 8, 15);
            tabControl.ForeColor = Color.FromArgb(79, 195, 247);  // 파란색
            tabControl.Font = new Font("Courier New", 9F);
            tabControl.Appearance = TabAppearance.FlatButtons;

            // 탭 페이지 배경
            tab_data.BackColor = Color.FromArgb(13, 13, 24);
            tab_train.BackColor = Color.FromArgb(13, 13, 24);

            // 원본 / 전처리 이미지 공통
            picImage.BackColor = Color.FromArgb(7, 7, 15);
            picImage.BorderStyle = BorderStyle.FixedSingle;
            picImage.SizeMode = PictureBoxSizeMode.Zoom;

            picEdge.BackColor = Color.FromArgb(7, 7, 15);
            picEdge.BorderStyle = BorderStyle.FixedSingle;
            picEdge.SizeMode = PictureBoxSizeMode.Zoom;

            // 이미지 리스트 속성
            listImages.BackColor = Color.FromArgb(7, 7, 15);
            listImages.ForeColor = Color.FromArgb(85, 85, 85);
            listImages.BorderStyle = BorderStyle.FixedSingle;
            listImages.Font = new Font("Courier New", 9F);

            // 버튼 스타일 공통
            void StyleButton(Button btn, Color borderColor)
            {
                btn.BackColor = Color.FromArgb(13, 13, 24);
                btn.ForeColor = borderColor;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderColor = borderColor;
                btn.FlatAppearance.BorderSize = 1;
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(20, 20, 40);
                btn.Font = new Font("Courier New", 8F);
                btn.Cursor = Cursors.Hand;
            }

            // 적용
            StyleButton(btn_delete, Color.FromArgb(239, 83, 80));   // 빨강 - 프레임 삭제
            StyleButton(btn_restore, Color.FromArgb(204, 204, 204)); // 회색 - 프레임 복구
            StyleButton(btnPlay, Color.FromArgb(102, 187, 106)); // 초록 - 재생
            StyleButton(btn_openfolder, Color.FromArgb(204, 204, 204)); // 회색 - 폴더 열기
            StyleButton(btnSetStart, Color.FromArgb(204, 204, 204)); // 회색 - 시작 프레임 설정
            StyleButton(btnSetEnd, Color.FromArgb(204, 204, 204)); // 회색 - 끝 프레임 설정
            StyleButton(btn_changquality, Color.FromArgb(255, 167, 38));  // 주황 - 화질 조정
            StyleButton(btn_train, Color.FromArgb(79, 195, 247));  // 파랑 - train
            StyleButton(btn_stopTrain, Color.FromArgb(239, 83, 80));   // 빨강 - 학습중단
            StyleButton(btn_before, Color.FromArgb(102, 187, 106)); // 초록 - 이전 프레임
            StyleButton(btn_imgnext, Color.FromArgb(102, 187, 106)); // 초록 - 다음 프레임
            
            // 트랙바 스타일
            trackBar_frame.BackColor = Color.FromArgb(13, 13, 24);
            trackBar_frame.ForeColor = Color.FromArgb(79, 195, 247);
            trackBar_frame.TickStyle = TickStyle.None;

            // Form1 생성자에서 PictureBox 설정
            string gagePath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "속도_앵글_바늘없는버전.png"
            );

            if (File.Exists(gagePath))
            {
                Bitmap gageImg = new Bitmap(gagePath);

                // picture_Gage의 실제 크기로 새 비트맵 생성
                Bitmap result = new Bitmap(
                    picture_Gage.Width,
                    picture_Gage.Height
                );

                using (Graphics g = Graphics.FromImage(result))
                {
                    g.Clear(Color.Black); // 완전 검정으로 시도
                    g.DrawImage(gageImg, 0, 0, picture_Gage.Width, picture_Gage.Height);
                }

                picture_Gage.Image = result;
                picture_Gage.SizeMode = PictureBoxSizeMode.Normal; // Normal로 변경!
                picture_Gage.BackColor = Color.Black;
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
                text_throttle.Text = $"속도: {entry.user_throttle:F3}";
                text_angle.Text = $"앵글: {entry.user_angle:F3}";
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
