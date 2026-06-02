import argparse
import tensorflow as tf
from donkeycar.parts.datastore import TubGroup

def parse_args():
    parser = argparse.ArgumentParser()
    parser.add_argument("--data", type=str, default="data")
    parser.add_argument("--model", type=str, default="cnn")
    parser.add_argument("--epochs", type=int, default=10)
    parser.add_argument("--batch_size", type=int, default=32)
    return parser.parse_args()


def build_model(model_type):
    if model_type == "cnn":
        return create_cnn()
    elif model_type == "lstm":
        return create_lstm()
    else:
        raise ValueError("unknown model")


def create_cnn():
    model = tf.keras.Sequential([
        tf.keras.layers.Input(shape=(120,160,3)),
        tf.keras.layers.Conv2D(24,5,2,activation="relu"),
        tf.keras.layers.Conv2D(32,5,2,activation="relu"),
        tf.keras.layers.Flatten(),
        tf.keras.layers.Dense(50, activation="relu"),
        tf.keras.layers.Dense(2)
    ])

    model.compile(optimizer="adam", loss="mse")
    return model


def load_data(path):
    return TubGroup(path)


def train(model, data, args):
    model.fit(data, epochs=args.epochs, batch_size=args.batch_size)
    model.save("model.h5")


def main():
    args = parse_args()
    data = load_data(args.data)
    model = build_model(args.model)
    train(model, data, args)


if __name__ == "__main__":
    main()