# 🚗 Datamanager — Donkeycar 자율주행 데이터 관리 UI

> Donkeycar 시뮬레이터 기반 자율주행 데이터를 수집·정제하고 AI 모델 학습을 관리하는 **C# WinForms 데스크탑 애플리케이션**입니다.

---

## 📋 목차

- [프로젝트 개요](#프로젝트-개요)
- [주요 기능](#주요-기능)
- [기술 스택](#기술-스택)
- [탭별 기능 상세](#탭별-기능-상세)
  - [데이터 매니저 탭](#1-데이터-매니저-탭)
  - [학습 탭](#2-학습-탭)
  - [파일럿 아레나 탭](#3-파일럿-아레나-탭)
- [핵심 구현 상세](#핵심-구현-상세)
- [시스템 요구사항](#시스템-요구사항)
- [설치 및 실행](#설치-및-실행)
- [프로젝트 구조](#프로젝트-구조)
- [구현 단계 로드맵](#구현-단계-로드맵)
- [팀원 소개](#팀원-소개)
- [회의록](#회의록)

---

## 프로젝트 개요

본 프로젝트는 Unity 기반 **Donkeycar 시뮬레이터**와 연동하여 자율주행 데이터를 효율적으로 확인·정제하고 AI 모델 학습을 관리할 수 있는 Windows UI 애플리케이션입니다.

### 전체 동작 흐름

```
시뮬레이터 주행 → 데이터 수집 (catalog JSON) → C# UI 데이터 정제 → AI 모델 학습 → 자율주행 검증
```

---

## 주요 기능

- 📂 주행 데이터(이미지 + catalog JSON) 로드 및 시각화
- 🔍 OpenCV 기반 차선 인식 결과 실시간 표시 (HSV 흰색 마스크 + Canny 엣지 + HoughLinesP)
- 🗑️ 불량 프레임 삭제 및 복구 (단일 / 구간 / 다중 선택 + 휴지통 관리)
- 🎛️ 네온 계기판 바늘 GDI+ 드로잉 애니메이션 (속도 / 조향각 동기화)
- 🤖 Python CNN/LSTM 모델 학습 실행 (WSL subprocess 연동) 및 실시간 로그 모니터링
- 📊 학습 Loss 그래프 실시간 시각화 (Train Loss / Val Loss)
- 🏁 AI 예측값과 실제값 비교 분석 (파일럿 아레나)
- 🔎 이상 탐지: 손상 이미지 및 비정상 catalog 데이터 자동 감지
- 💾 흑백 이미지 전처리 및 training_data.catalog 자동 생성

---

## 기술 스택

| 분류 | 기술 |
|------|------|
| UI 프레임워크 | C# WinForms (.NET 10.0) |
| 이미지 처리 | Emgu.CV (OpenCV .NET wrapper) |
| 데이터 파싱 | System.Text.Json (catalog JSONL 파싱) |
| AI 연동 | Newtonsoft.Json, WSL subprocess (ProcessStartInfo) |
| 그래프 | WinForms.DataVisualization (Chart 컨트롤) |
| 그래픽 렌더링 | GDI+ (Graphics, Bitmap, PathGradientBrush) |
| 학습 백엔드 | Python (WSL2 + conda), train.py / train_lstm.py |

---

## 탭별 기능 상세

### 1. 데이터 매니저 탭

> 주행 데이터를 확인하고 불량 프레임을 정제하는 화면입니다.

<img width="1513" height="987" alt="데이터 매니저 탭" src="https://github.com/user-attachments/assets/a1d262de-d358-49ec-bc11-e753887e9c68" />

#### 이미지 및 차선 인식 뷰
- 좌측: 원본 주행 이미지 출력 (체크박스로 차선 오버레이 토글)
- 우측: OpenCV 기반 흰색 차선 인식 결과 (ROI 엣지 검출) 출력
- 차선 인식 알고리즘: `BGR → HSV 변환 → 흰색 마스크 → Canny 엣지 → 사다리꼴 ROI → HoughLinesP → FitLine + EMA 스무딩`
- 한쪽 차선이 미검출될 경우 이전 폭 데이터로 가상 차선(노란색) 자동 추정

<img width="876" height="382" alt="이미지 및 차선 인식" src="https://github.com/user-attachments/assets/5875a149-4c16-469d-af0f-c21aa2b4be05" />

#### 계기판 시각화
- **속도(Throttle)**: 네온 그린 바늘이 현재 프레임의 속도값(0.0 ~ 1.0)에 맞게 실시간 회전
- **조향각(Angle)**: 네온 블루 바늘이 현재 프레임의 조향각(-1.0 ~ +1.0)에 맞게 실시간 회전
- GDI+ `PathGradientBrush`로 바늘 끝이 뾰족한 네온 글로우 효과 렌더링
- `picNeedleSpeed`, `picNeedleAngle`을 `picture_Gage` 위에 투명 오버레이로 배치
- 하단 `label_throttle`, `label_angle`에 F3 포맷 수치 표시

<img width="500" height="342" alt="계기판 시각화" src="https://github.com/user-attachments/assets/38b607cf-c402-4f2f-a88c-86c43b62bf93" />

#### 프레임 탐색 및 재생
- **TrackBar 슬라이더**: `validIndices` 기반으로 실제 존재하는 프레임만 탐색
- **이전/다음 버튼**: 이진 탐색(`BinarySearch`) 기반으로 빠른 다음 유효 프레임 이동
- **재생/정지 버튼**: `Timer` 기반 자동재생, `comboBox_play`로 배속(x0.5 ~ x4.0) 조절 가능
- **썸네일 패널**: 현재 프레임 기준 앞뒤 각 10장씩 동적 로드 (메모리 최적화)
- **조향각/속도 차트**: `chart_data`에 전체 구간의 Throttle / Angle 꺾은선 그래프 표시

#### 데이터 정제 기능

| 기능 | 설명 |
|------|------|
| 구간 삭제 | `btnSetStart` / `btnSetEnd`로 시작·끝 프레임 지정 후 한 번에 대량 삭제, `panelTrackBarProgress`에 빨간 구간 시각화 |
| 다중 선택 삭제 | `listImages` MultiExtended 모드로 여러 프레임 선택 후 삭제 |
| 복구 | `originalCatalogData` 백업 딕셔너리로 삭제된 프레임 메모리 복원, `listBox_delete`에서 선택 후 복구 |
| 이상 탐지 | `btnDetect` 클릭 시 파일 크기·OpenCV 로드·catalog 매칭·수치 유효성 검증, 이상 항목을 리스트에서 빨간색으로 표시 |
| 화질 조절 | JPEG 품질(10~100) 입력 후 전체 이미지 압축, `backup/` 폴더에 원본 자동 백업 |
| 필터링 | `checkBox_filter` 체크 시 angle=0 또는 throttle≤0 데이터를 학습에서 제외 |

---

### 2. 학습 탭

> Python AI 모델 학습을 실행하고 결과를 모니터링하는 화면입니다.

<img width="1513" height="978" alt="학습 탭" src="https://github.com/user-attachments/assets/51caef5b-326f-4ebf-8ef4-88d1d05c19e3" />

#### 학습 설정 및 실행
- **모델 선택**: CNN(`train.py`) / LSTM(`train_lstm.py`) 중 선택
- **가상환경 선택**: WSL `conda env list` 자동 실행 후 `comboBox_venv`에 목록 표시
- **Train 버튼** 클릭 시 다음 순서로 처리:
  1. 전체 이미지 검증 및 흑백 ROI 전처리 (`wbimages/` 폴더 생성)
  2. `training_data.catalog` JSONL 파일 자동 생성
  3. WSL subprocess로 Python 학습 프로세스 비동기 실행
- **학습 중단**: `trainProcess.Kill()` 또는 `Ctrl+C` 신호 전송

#### 실시간 모니터링
- **로그 창** (`AutoScrollListBox`): `OutputDataReceived` 이벤트로 Epoch별 로그 실시간 수집
- **진행률**: 로그에서 `Epoch N/M` 정규식 파싱 후 `progressBar_learn` 업데이트
- **Loss 그래프**: `loss: X - val_loss: Y` 정규식 파싱 후 `chart_loss`에 실시간 꺾은선 추가

<img width="801" height="926" alt="Loss 그래프 및 AI 비교" src="https://github.com/user-attachments/assets/cb341845-9853-494c-a2b1-24734f37190b" />

#### AI 학습 확인 (수치 비교)
- `combo_compare`에서 랜덤 5개 프레임 선택 후 `evaluate_single.py`를 WSL subprocess로 실행
- Python 출력에서 JSON `{"angle": X, "throttle": Y}` 파싱 후 실제값과 비교 표시
- 오차가 0.1 미만이면 초록색, 이상이면 빨간색으로 `label_ocha` 색상 변경
- `val_loss` 기반으로 학습 품질 점수(0~100) 및 등급(S/A/B/C/D) 자동 산출

---

### 3. 파일럿 아레나 탭

> 학습 완료된 AI 모델의 예측 결과를 실제 주행 데이터와 비교 분석하는 화면입니다.

<img width="1514" height="991" alt="파일럿 아레나 탭" src="https://github.com/user-attachments/assets/1e99f70f-f4ea-43fc-aa40-09028ab21eaf" />

#### 프레임별 비교 분석
- `predictions.json` 로드 후 각 프레임의 `real_angle`, `real_throttle`, `pred_angle`, `pred_throttle` 비교
- `picboxImage_Paint` 이벤트에서 GDI+로 **실제 주행 경로(초록색)**와 **AI 예측 경로(빨간색)** 화살표 오버레이
- 화살표 길이는 throttle 값(0.0~1.0)에 비례, 방향은 angle 값(-1.0~+1.0)에 따라 계산
- 프레임 이동 및 `timer_pilot` 기반 자동 재생 지원
- 하단 `flowLayout_arena`에 현재 프레임 기준 앞뒤 각 10장 썸네일 동적 로드

#### 수치 비교 대시보드

| 항목 | 실제값 | AI 예측값 |
|------|--------|-----------|
| 속도 (Throttle) | 파란색 수치 + `progressAngle` | 주황색 수치 + `progressSpeedAI` |
| 조향각 (Angle) | 파란색 수치 + `progressSpeed` | 주황색 수치 + `progressAngleAI` |

- **속도 오차 / 앵글 오차**: `Math.Abs(real - pred)` 수치를 `lblSpeedError`, `lblAngleError`에 표시
- **현재 프레임 번호**: `lblCurrentFrame2`에 실시간 표시

<img width="446" height="558" alt="수치 비교 대시보드" src="https://github.com/user-attachments/assets/2c6ab532-cc00-4755-8e6b-08b0cef96e71" />

---

## 핵심 구현 상세

### catalog 파싱 (JSONL 형식)
```
{"_index": 0, "cam/image_array": "0_cam_image_array_.jpg", "user/angle": 0.007, "user/throttle": 0.214}
{"_index": 1, ...}
```
- 각 줄이 독립적인 JSON 객체인 JSONL 형식
- `System.Text.Json.JsonDocument`로 한 줄씩 파싱 → `Dictionary<int, CatalogEntry>` 저장
- `validIndices` 리스트로 살아있는 프레임만 관리, 삭제 후 이진 탐색으로 빠른 다음 프레임 탐색

### 차선 인식 파이프라인
```
원본 이미지
  → BGR to HSV 변환
  → 흰색 범위 마스크 (H:0~180, S:0~80, V:160~255)
  → Canny 엣지 검출 (50, 150)
  → 사다리꼴 ROI 마스크 적용
  → HoughLinesP 직선 검출 (threshold=15, minLength=15, maxGap=10)
  → 기울기 부호로 좌/우 차선 분리
  → FitLine으로 대표 직선 추출
  → EMA 스무딩 (α=0.1)으로 떨림 감소
```

### Python 학습 연동 (WSL subprocess)
```csharp
ProcessStartInfo psi = new ProcessStartInfo();
psi.FileName = "wsl";
psi.Arguments = $"bash -c \"cd {wslBase} && {pythonPath} train.py --data data --epochs 10\"";
psi.RedirectStandardOutput = true;
psi.RedirectStandardError = true;
trainProcess.OutputDataReceived += (s, args) => { /* 실시간 로그 파싱 */ };
```

### 메모리 최적화
- 썸네일 패널: 현재 인덱스 기준 앞뒤 각 10장만 `Image.FromFile()`로 동적 로드
- 이미지 전환 시 `picImage.Image.Dispose()` 명시적 해제로 GC 부하 최소화
- 4GB 환경에서도 10,000장 데이터셋 안정적 탐색 가능

---

## 시스템 요구사항

| 항목 | 요구사항 |
|------|----------|
| OS | Windows 10 / 11 |
| .NET | .NET 10.0 (Windows) |
| WSL | WSL2 + Ubuntu (Python 학습 실행 시 필요) |
| Python | 3.11 (conda 가상환경 권장) |
| RAM | 4GB 이상 (권장 8GB) |
| 주요 NuGet | Emgu.CV, Newtonsoft.Json, WinForms.DataVisualization |

---

## 설치 및 실행

### 1. 레포지토리 클론
```bash
git clone https://github.com/programmingorganization/Datamanager.git
cd Datamanager
```

### 2. 데이터 준비
```
Datamanager/
├── data/
│   ├── images/               ← 주행 이미지 파일 (.jpg)
│   ├── catalog_0.catalog     ← JSONL 형식 주행 레이블
│   ├── catalog_1.catalog
│   └── manifest.json
```

### 3. 빌드 및 실행
- Visual Studio 2022에서 `Datamanager.slnx` 열기
- NuGet 패키지 복원 (`dotnet restore`)
- `F5` 또는 `▶ Datamanager` 버튼으로 실행

### 4. Python 환경 설정 (학습 기능 사용 시)
```bash
# WSL에서
conda create -n e2e_env python=3.11
conda activate e2e_env
pip install donkeycar tensorflow
```

---

## 프로젝트 구조

```
Datamanager/
├── Form.cs                  ← 메인 폼 로직 (차선 인식, catalog 파싱, 학습 연동, 파일럿 아레나)
├── Form.Designer.cs         ← UI 컨트롤 레이아웃 (자동 생성)
├── Form.resx                ← 리소스 (계기판 이미지 등)
├── Program.cs               ← 진입점
├── data/                    ← 주행 데이터 (catalog + images)
├── trash/                   ← 삭제된 프레임 보관 (날짜별 서브폴더)
├── backup/                  ← 화질 압축 전 원본 이미지 백업
├── wbimages/                ← 흑백 전처리 이미지 (학습 시 자동 생성)
├── predictions.json         ← AI 예측 결과 (학습 완료 후 생성)
├── score.json               ← 학습 품질 점수 (학습 완료 후 생성)
└── README.md
```

---

## 구현 단계 로드맵

| 단계 | 내용 |
|------|------|
| 1 | catalog 파일 읽기: JSONL 구조 분석 및 `Dictionary<int, CatalogEntry>` 파싱 |
| 2 | 이미지 + 데이터 표시: PictureBox에 이미지 로드, label에 angle/throttle 수치 동기화 |
| 3 | 프레임 슬라이더 구현: `validIndices` 기반 TrackBar 연동, 이진 탐색으로 빠른 이동 |
| 4 | 그래프/자동재생 구현: `chart_data` 꺾은선 그래프, Timer 기반 자동재생 및 배속 조절 |
| 5 | 데이터 필터링: angle=0 / throttle≤0 조건부 제외, 이상 탐지 자동 감지 |
| 6 | 데이터 삭제 기능: 구간 지정 삭제, 다중 선택 삭제, 휴지통 복구 |
| 7 | 리스트 선택: MultiExtended 리스트박스, 썸네일 패널 동적 로드 |
| 8 | 학습 실행 및 연동: WSL subprocess, 실시간 로그/Loss 그래프, 파일럿 아레나 |

---

## 팀원 소개

| 역할 | 이름 | 
|------|------|
| 팀장 / 연동 | 김가희 |
| UI | 강현우 |
| 데이터 | 이정하 |
| 기능 | 임종찬 |

---

## 회의록

### 회의 개요

| 회차 | 날짜 | 시간 |
|------|------|------|
| 1차 | 2026년 05월 09일 (토) | 오후 2:07 ~ 2:27 |
| 2차 | 2026년 05월 16일 (토) | 오후 2:03 ~ 2:30 |
| 3차 | 2026년 05월 30일 (토) | 오후 2:00 ~ 2:45 |

**참석자**: 김가희(팀장), 강현우, 이정하, 임종찬

### 최종 개발 일정

| 목표일 | 작업 내용 | 담당자 |
|--------|-----------|--------|
| ~ 05월 27일 | 데이터 매니저 탭 기초 완성 (계기판, 단일 정제) | 공통 |
| ~ 06월 01일 | 필터링 기능 반영 및 도움말 UI 레이아웃 확정 | 이정하, 강현우 |
| ~ 06월 03일 | 전체 UI 디자인 마감, 예측 경로 및 실시간 그래프 연동 | 김가희, 임종찬 |
| 06월 05일 | **[CRITICAL] 전 팀원 기능 구현 완료 및 코드 통합** | 공통 |
| 06월 06~07일 | 통합 빌드 테스트, 4GB 사양 예외 처리 및 디버깅 | 공통 |
| 06월 08~10일 | 최종 발표 자료 구성, 발표 대본 제작 | 공통 |
| **06월 11일** | **🚀 졸업 프로젝트 최종 발표** | 전원 |

### 기술적 결정사항

- **메모리 최적화**: 10,000장 이미지를 한꺼번에 로드하면 4GB 환경에서 크래시 발생 → 현재 인덱스 기준 **주변 20장만 가변 로드**하는 방식으로 최적화
- **catalog 삭제 방식**: 파일을 직접 삭제하지 않고 `catalogData` 딕셔너리에서 제거하는 **논리적 삭제** 방식 채택 → `originalCatalogData` 백업으로 언제든 복구 가능
