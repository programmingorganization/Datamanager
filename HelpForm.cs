using System;
using System.Drawing;
using System.Windows.Forms;

public partial class HelpForm : Form
{
    public HelpForm()
    {
        InitializeComponent();

        this.Text = "도움말";
        this.Size = new Size(600, 700);
        this.BackColor = Color.FromArgb(13, 13, 24);
        this.ForeColor = Color.FromArgb(204, 204, 204);
        this.StartPosition = FormStartPosition.CenterParent;

        richTextBox_Help.BackColor = Color.FromArgb(7, 7, 15);
        richTextBox_Help.ForeColor = Color.FromArgb(204, 204, 204);
        richTextBox_Help.Font = new Font("Consolas", 10F);
        richTextBox_Help.ReadOnly = true;
        richTextBox_Help.BorderStyle = BorderStyle.None;
        richTextBox_Help.ScrollBars = RichTextBoxScrollBars.Vertical;

        richTextBox_Help.Text =
@"=== 데이터 매니저 도움말 ===

[ 데이터 매니저 탭 ]

▶ 폴더 열기
  - 이미지 폴더를 선택하여 프레임을 로드합니다.

▶ 프레임 이동
  - 프레임 ◀ / ▶ 버튼으로 이전/다음 프레임 이동
  - 트랙바로 원하는 프레임으로 빠르게 이동
  - 썸네일 클릭으로 해당 프레임으로 이동

▶ 재생
  - ▶ 버튼으로 자동 재생/정지
  - 배속 콤보박스로 재생 속도 조절

▶ 프레임 삭제
  1. 시작 프레임 버튼으로 삭제 시작 지점 설정
  2. 끝 프레임 버튼으로 삭제 끝 지점 설정
  3. 프레임 삭제 버튼으로 해당 구간 삭제
  - 삭제된 프레임은 삭제 프레임 목록에 표시됩니다.

▶ 프레임 복구
  - 삭제 프레임 목록에서 복구할 항목 선택 (다중 선택 가능)
  - 프레임 복구 버튼 클릭

▶ 이상 탐지
  - 깨진 이미지나 비정상 카탈로그 데이터를 탐지합니다.
  - 이상 탐지된 프레임은 목록에서 빨간색으로 표시됩니다.

▶ 프레임 화질 조정
  - JPEG 압축 품질을 조절합니다. (10~100)

▶ 필터링 / 라인 표시 / 예측 경로
  - 체크박스로 각 기능을 ON/OFF 합니다.

▶ 속도 / 앵글 차트
  - 체크박스로 Throttle / Angle 데이터 표시를 ON/OFF 합니다.

▶ 휴지통
  - 삭제된 이미지 파일 목록을 폴더별로 확인합니다.

────────────────────────────

[ 학습 탭 ]

▶ 가상환경 선택
  - WSL conda 환경 목록에서 학습에 사용할 환경을 선택합니다.

▶ 모델 선택
  - CNN / LSTM 모델 중 학습할 모델을 선택합니다.

▶ 학습 버튼
  - 데이터 전처리 후 선택한 모델로 학습을 시작합니다.
  - 학습 로그가 실시간으로 표시됩니다.
  - 학습 진행률이 프로그레스바에 표시됩니다.

▶ 학습 중단
  - 진행 중인 학습을 즉시 중단합니다.

▶ AI 수치 비교
  - 콤보박스에서 이미지를 선택하면
    실제 속도/앵글과 AI 예측 속도/앵글을 비교합니다.
  - 오차가 0.1 미만이면 초록색, 이상이면 빨간색으로 표시됩니다.

▶ 학습 품질 점수
  - 학습 완료 후 val_loss 기반으로 점수와 등급을 표시합니다.
  - S(90↑) / A(75↑) / B(60↑) / C(40↑) / D(40↓)";
    }

    private void InitializeComponent()
    {
        richTextBox_Help = new RichTextBox();
        SuspendLayout();

        richTextBox_Help.Dock = DockStyle.Fill;
        richTextBox_Help.Location = new Point(0, 0);
        richTextBox_Help.Name = "richTextBox_Help";
        richTextBox_Help.Size = new Size(584, 661);
        richTextBox_Help.TabIndex = 0;
        richTextBox_Help.Text = "";

        ClientSize = new Size(584, 661);
        Controls.Add(richTextBox_Help);
        Name = "HelpForm";
        ResumeLayout(false);
    }

    private RichTextBox richTextBox_Help;
}