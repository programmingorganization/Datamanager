import sys
import json
import os

import numpy as np
import tensorflow as tf
from PIL import Image

SEQUENCE_LENGTH = 3

model_type = sys.argv[1].lower()

if model_type == "cnn":
    model_path = "model.h5"

elif model_type == "lstm":
    model_path = "model_lstm.h5"

else:
    raise Exception(
        f"unknown model type: {model_type}"
    )

model = tf.keras.models.load_model(
    model_path
)

records = []

with open(
    "data/training_data.catalog",
    "r",
    encoding="utf-8-sig"
) as f:

    for line in f:
        line = line.strip()

        if not line:
            continue

        records.append(json.loads(line))

results = []

if model_type == "cnn":

    for record in records:

        image_path = os.path.join(
            "data",
            record["cam/image_wb"]
        )

        img = Image.open(image_path)

        img = img.convert("L")
        img = img.resize((160,120))

        img = np.array(
            img,
            dtype=np.float32
        ) / 255.0

        img = np.expand_dims(img,-1)
        img = np.expand_dims(img,0)

        pred = model.predict(
            img,
            verbose=0
        )[0]

        results.append({
            "image":
                os.path.basename(image_path),

            "real_angle":
                float(record["user/angle"]),

            "real_throttle":
                float(record["user/throttle"]),

            "pred_angle":
                float(pred[0]),

            "pred_throttle":
                float(pred[1])
        })

else:

    images = []

    for record in records:

        image_path = os.path.join(
            "data",
            record["cam/image_wb"]
        )

        img = Image.open(image_path)

        img = img.convert("L")
        img = img.resize((160,120))

        img = np.array(
            img,
            dtype=np.float32
        ) / 255.0

        img = np.expand_dims(
            img,
            axis=-1
        )

        images.append(img)

    for i in range(
        SEQUENCE_LENGTH - 1,
        len(images)
    ):

        seq = images[
            i - SEQUENCE_LENGTH + 1:
            i + 1
        ]

        seq = np.array(
            seq,
            dtype=np.float32
        )

        seq = np.expand_dims(
            seq,
            axis=0
        )

        pred = model.predict(
            seq,
            verbose=0
        )[0]

        record = records[i]

        results.append({
            "image":
                os.path.basename(
                    record["cam/image_wb"]
                ),

            "real_angle":
                float(record["user/angle"]),

            "real_throttle":
                float(record["user/throttle"]),

            "pred_angle":
                float(pred[0]),

            "pred_throttle":
                float(pred[1])
        })

with open(
    "predictions.json",
    "w",
    encoding="utf-8"
) as f:

    json.dump(
        results,
        f,
        indent=4
    )

print(
    f"saved predictions: {len(results)}"
)