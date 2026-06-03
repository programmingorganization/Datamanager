import json
import os
import numpy as np
import tensorflow as tf
from PIL import Image

SEQUENCE_LENGTH = 5
MAX_SAMPLES = 100
DATA_DIR = "data"


def load_image(path):
    img = Image.open(path)
    img = img.convert("L")
    img = img.resize((160, 120))
    img = np.array(img, dtype=np.float32) / 255.0
    img = np.expand_dims(img, axis=-1)
    return img


catalog_path = os.path.join(DATA_DIR, "training_data.catalog")

model = tf.keras.models.load_model("model_lstm.h5")

images = []
labels = []

# 1. 데이터 로드
with open(catalog_path, "r", encoding="utf-8-sig") as f:
    for line in f:
        record = json.loads(line)

        image_rel = record["cam/image_wb"]
        image_path = os.path.join(DATA_DIR, image_rel)

        if not os.path.exists(image_path):
            continue

        images.append(load_image(image_path))

        labels.append([
            float(record["user/angle"]),
            float(record["user/throttle"])
        ])


# 2. 평가
results = []

total_angle_error = 0
total_throttle_error = 0
count = 0

for i in range(SEQUENCE_LENGTH - 1, len(images)):

    seq = np.array(images[i-SEQUENCE_LENGTH+1:i+1])

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


# 3. 평균 계산 (이게 빠져 있었음)
if count == 0:
    raise Exception("No samples processed")

avg_angle_error = total_angle_error / count
avg_throttle_error = total_throttle_error / count

avg_error = (avg_angle_error + avg_throttle_error) / 2
score = max(0, 100 - avg_error * 100)


# 4. 저장
with open("score.json", "w", encoding="utf-8") as f:
    json.dump({
        "score": round(score, 2),
        "avg_angle_error": round(avg_angle_error, 4),
        "avg_throttle_error": round(avg_throttle_error, 4),
        "samples": count,
        "results": results
    }, f, indent=4)


print(f"Score = {score:.2f}")