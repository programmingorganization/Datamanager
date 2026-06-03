import argparse
import json
import os

import numpy as np
import tensorflow as tf
from PIL import Image


def parse_args():
    parser = argparse.ArgumentParser()

    parser.add_argument("--data", default="data")
    parser.add_argument("--model", default="cnn")
    parser.add_argument("--epochs", type=int, default=10)
    parser.add_argument("--batch_size", type=int, default=32)

    return parser.parse_args()


def create_cnn():
    model = tf.keras.Sequential([
        tf.keras.layers.Input(shape=(120, 160, 1)),

        tf.keras.layers.Conv2D(
            24,
            (5, 5),
            strides=(2, 2),
            activation="relu"
        ),

        tf.keras.layers.Conv2D(
            32,
            (5, 5),
            strides=(2, 2),
            activation="relu"
        ),

        tf.keras.layers.Flatten(),

        tf.keras.layers.Dense(
            50,
            activation="relu"
        ),

        tf.keras.layers.Dense(2)
    ])

    model.compile(
        optimizer="adam",
        loss="mse"
    )

    return model


def load_dataset(data_dir):

    catalog_path = os.path.join(
        data_dir,
        "training_data.catalog"
    )

    X = []
    Y = []

    count = 0

    with open(catalog_path,"r",encoding="utf-8-sig") as f:

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

            if not os.path.exists(image_path):
                continue

            img = Image.open(image_path)

            img = img.convert("L")

            img = img.resize(
                (160, 120)
            )

            img = np.array(
                img,
                dtype=np.float32
            ) / 255.0

            img = np.expand_dims(
                img,
                axis=-1
            )

            angle = float(
                record["user/angle"]
            )

            throttle = float(
                record["user/throttle"]
            )

            X.append(img)
            Y.append([angle, throttle])

            count += 1

    X = np.array(
        X,
        dtype=np.float32
    )

    Y = np.array(
        Y,
        dtype=np.float32
    )

    print(f"Loaded {count} samples")
    print("X shape =", X.shape)
    print("Y shape =", Y.shape)

    return X, Y


def main():

    args = parse_args()

    X, Y = load_dataset(
        args.data
    )

    model = create_cnn()

    history = model.fit(
        X,
        Y,
        epochs=args.epochs,
        batch_size=args.batch_size,
        validation_split=0.2
    )

    model.save("model.h5")

    print("model saved")

    with open(
        "history.json",
        "w",
        encoding="utf-8"
    ) as f:

        json.dump(
            history.history,
            f,
            indent=4
        )


if __name__ == "__main__":
    main()