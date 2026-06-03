import json
import os

import numpy as np
import tensorflow as tf
from PIL import Image


DATA_DIR = "data"


def load_image(image_path):

    img = Image.open(image_path)

    img = img.convert("L")

    img = img.resize((160, 120))

    img = np.array(
        img,
        dtype=np.float32
    ) / 255.0

    img = np.expand_dims(
        img,
        axis=-1
    )

    return img


model = tf.keras.models.load_model(
    "model.h5"
)

catalog_path = os.path.join(
    DATA_DIR,
    "training_data.catalog"
)

results = []

total_angle_error = 0
total_throttle_error = 0

MAX_SAMPLES = 100;
count = 0

with open(
    catalog_path,
    "r",
    encoding="utf-8-sig"
) as f:

    for line in f:

        line = line.strip()

        if not line:
            continue

        record = json.loads(line)

        image_rel = record["cam/image_wb"]

        image_path = os.path.join(
            DATA_DIR,
            image_rel
        )

        if not os.path.exists(image_path):
            continue

        img = load_image(image_path)

        pred = model.predict(
            img[None, ...],
            verbose=0
        )[0]

        actual_angle = float(
            record["user/angle"]
        )

        actual_throttle = float(
            record["user/throttle"]
        )

        pred_angle = float(pred[0])
        pred_throttle = float(pred[1])

        angle_error = abs(
            actual_angle - pred_angle
        )

        throttle_error = abs(
            actual_throttle - pred_throttle
        )

        total_angle_error += angle_error
        total_throttle_error += throttle_error

        results.append({
            "actual_angle": actual_angle,
            "pred_angle": pred_angle,
            "angle_error": angle_error,

            "actual_throttle": actual_throttle,
            "pred_throttle": pred_throttle,
            "throttle_error": throttle_error
        })

        count += 1

        if count % 10 == 0:
            print(f"{count} samples processed")

        if count >= MAX_SAMPLES:
            break


avg_angle_error = (
    total_angle_error / count
)

avg_throttle_error = (
    total_throttle_error / count
)

avg_error = (
    avg_angle_error +
    avg_throttle_error
) / 2

score = max(
    0,
    100 - avg_error * 100
)

with open(
    "score.json",
    "w",
    encoding="utf-8"
) as f:

    json.dump(
        {
            "score": round(score, 2),

            "avg_angle_error":
                round(avg_angle_error, 4),

            "avg_throttle_error":
                round(avg_throttle_error, 4),

            "samples": count,

            "results": results
        },
        f,
        indent=4
    )

print(
    f"Score = {score:.2f}"
)

