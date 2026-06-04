import argparse
import json
import os
import numpy as np
import tensorflow as tf
from PIL import Image

def parse_args():
    parser = argparse.ArgumentParser()
    parser.add_argument("--data", default="data") # 🔥 C# 인자 연동 대응
    return parser.parse_args()

# 🔥 train_lstm.py의 대동단결 3프레임 매칭
SEQUENCE_LENGTH = 3
MAX_SAMPLES = 100

def load_image(path):
    img = Image.open(path).convert("L").resize((160, 120))
    img = np.array(img, dtype=np.float32) / 255.0
    img = np.expand_dims(img, axis=-1)
    return img

def main():
    args = parse_args()
    DATA_DIR = args.data
    
    catalog_path = os.path.join(DATA_DIR, "training_data.catalog")
    
    if not os.path.exists("model_lstm.h5"):
        raise Exception("학습된 model_lstm.h5 파일이 존재하지 않습니다.")
        
    model = tf.keras.models.load_model("model_lstm.h5")

    images = []
    labels = []

    print("평가 데이터 로드 중...")
    with open(catalog_path, "r", encoding="utf-8-sig") as f:
        for line in f:
            line = line.strip()
            if not line:
                continue
            record = json.loads(line)

            image_rel = record["cam/image_wb"]
            if image_rel.startswith("data/"):
                image_path = os.path.join(os.path.dirname(DATA_DIR), image_rel)
            else:
                image_path = os.path.join(DATA_DIR, image_rel)

            if not os.path.exists(image_path):
                continue

            images.append(load_image(image_path))
            labels.append([
                float(record["user/angle"]),
                float(record["user/throttle"])
            ])

    results = []
    total_angle_error = 0
    total_throttle_error = 0
    count = 0

    print("LSTM 추론 평가 시작...")
    for i in range(SEQUENCE_LENGTH - 1, len(images)):
        # 🔥 정확하게 3개의 시퀀스 덩어리를 뜯어서 전송
        seq = np.array(images[i-SEQUENCE_LENGTH+1 : i+1])
        
        # 모델 예측 (Batch 차원 추가: [1, SEQUENCE_LENGTH, 120, 160, 1])
        pred = model.predict(seq[None, ...], verbose=0)[0]
        actual = labels[i]

        angle_error = abs(actual[0] - pred[0])
        throttle_error = abs(actual[1] - pred[1])

        total_angle_error += angle_error
        total_throttle_error += throttle_error

        results.append({
            "actual_angle": actual[0],
            "pred_angle": float(pred[0]),
            "angle_error": float(angle_error),
            "actual_throttle": actual[1],
            "pred_throttle": float(pred[1]),
            "throttle_error": float(throttle_error)
        })

        count += 1
        if count >= MAX_SAMPLES:
            break

    if count == 0:
        raise Exception("No samples processed")

    avg_angle_error = total_angle_error / count
    avg_throttle_error = total_throttle_error / count

    avg_error = (avg_angle_error + avg_throttle_error) / 2
    score = max(0, 100 - avg_error * 100)

    # 4. JSON 저장 (C#에서 읽어갈 핵심 결과물)
    with open("score.json", "w", encoding="utf-8") as f:
        json.dump({
            "score": round(score, 2),
            "avg_angle_error": round(avg_angle_error, 4),
            "avg_throttle_error": round(avg_throttle_error, 4),
            "samples": count,
            "results": results
        }, f, indent=4)
        
    print(f"Score = {score:.2f}")

if __name__ == "__main__":
    main()