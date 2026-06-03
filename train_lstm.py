import argparse
import json
import os

import numpy as np
import tensorflow as tf
from PIL import Image

def parse_args():
    parser = argparse.ArgumentParser()

    parser.add_argument("--data", default="data")
    parser.add_argument("--epochs", type=int, default=10)
    parser.add_argument("--batch_size", type=int, default=32)

    return parser.parse_args()

SEQUENCE_LENGTH = 3

def create_lstm():

    seq_input = tf.keras.Input(shape=(SEQUENCE_LENGTH,120,160,1))

    x = tf.keras.layers.TimeDistributed(
        tf.keras.layers.Conv2D(24,(5,5),strides=2,activation="relu")
    )(seq_input)

    x = tf.keras.layers.TimeDistributed(
        tf.keras.layers.Conv2D(32,(5,5),strides=2,activation="relu")
    )(x)

    x = tf.keras.layers.TimeDistributed(
        tf.keras.layers.GlobalAveragePooling2D()
    )(x)

    x = tf.keras.layers.LSTM(64)(x)
    x = tf.keras.layers.Dropout(0.2)(x)

    x = tf.keras.layers.Dense(32, activation="relu")(x)
    output = tf.keras.layers.Dense(2)(x)

    model = tf.keras.Model(seq_input, output)
    model.compile(optimizer="adam", loss="mse")

    print("LSTM MODEL BUILT SAFE VERSION")

    return model


def load_dataset(data_dir):

    print("loading dataset...")

    catalog_path = os.path.join(
        data_dir,
        "training_data.catalog"
    )

    # 🔥 여기 추가
    print("catalog path:", catalog_path)
    print("exists:", os.path.exists(catalog_path))

    images = []
    labels = []

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
                data_dir,
                image_rel
            )

            # 🔥 여기 추가
            if not os.path.exists(image_path):
                print("missing image:", image_path)
                continue

            if not os.path.exists(image_path):
                continue

            img = Image.open(image_path)

            img = img.convert("L")

            img = img.resize(
                (160,120)
            )

            img = np.array(
                img,
                dtype=np.float32
            ) / 255.0

            img = np.expand_dims(
                img,
                axis=-1
            )

            images.append(img)

            labels.append([
                float(record["user/angle"]),
                float(record["user/throttle"])
            ])

    X = []
    Y = []

    for i in range(
        SEQUENCE_LENGTH - 1,
        len(images)
    ):

        seq = images[
            i - SEQUENCE_LENGTH + 1 :
            i + 1
        ]

        X.append(seq)

        Y.append(labels[i])

    X = np.array(
        X,
        dtype=np.float32
    )

    Y = np.array(
        Y,
        dtype=np.float32
    )

    print("FINAL images:", len(images))
    print("FINAL X shape:", X.shape)
    print("FINAL Y shape:", Y.shape)

    print("FINAL images:", len(images))
    print("FINAL X shape:", X.shape)

    if len(X) == 0:
        raise Exception("No training samples generated. Check dataset or image paths.")

        return X, Y

def main():

    print("MAIN START")  # 🔥 추가

    args = parse_args()

    X, Y = load_dataset(args.data)

    model = create_lstm()

    print("DEBUG X:", X.shape)
    print("DEBUG Y:", Y.shape)

    # ✔ 시계열 split (가장 중요)
    split = int(len(X) * 0.8)

    X_train, X_val = X[:split], X[split:]
    Y_train, Y_val = Y[:split], Y[split:]

    print("training start")
    print("X_train:", X_train.shape)
    print("X_val:", X_val.shape)

    history = model.fit(
        X_train,
        Y_train,
        epochs=args.epochs,
        batch_size=args.batch_size,
        validation_data=(X_val, Y_val),
        shuffle=False   # ✔ LSTM 핵심
    )



    model.save("model_lstm.h5")
    print("model saved")
    print("saved:", os.path.exists("model_lstm.h5"))

    with open("history_lstm.json", "w", encoding="utf-8") as f:
        json.dump(history.history, f, indent=4)