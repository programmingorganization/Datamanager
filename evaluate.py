import tensorflow as tf
import json
import numpy as np
from donkeycar.parts.datastore import TubGroup
from PIL import Image

def preprocess(img):
    img = Image.fromarray(img)
    img = img.resize((160,120))
    return np.array(img) / 255.0


model = tf.keras.models.load_model("model.h5")
data = TubGroup("data")

results = []
total_error = 0

for sample in data:
    img = sample["cam/image_array"]

    actual_angle = sample["user/angle"]
    actual_throttle = sample["user/throttle"]

    img = preprocess(img)

    pred = model.predict(img[None, ...])[0]

    error = abs(actual_throttle - pred[1])

    results.append({
        "actual": actual_throttle,
        "pred": float(pred[1]),
        "error": float(error)
    })

    total_error += error

score = max(0, 100 - (total_error / len(results)) * 100)

with open("score.json", "w") as f:
    json.dump({
        "score": score,
        "avg_error": total_error / len(results),
        "results": results
    }, f, indent=4)