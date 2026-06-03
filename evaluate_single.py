import sys
import json
import numpy as np
from PIL import Image
import tensorflow as tf

def predict(image_path):
    model = tf.keras.models.load_model("model.h5")
    
    img = Image.open(image_path).convert("L")
    img = img.resize((160, 120))
    img = np.array(img, dtype=np.float32) / 255.0
    img = np.expand_dims(img, axis=-1)
    img = np.expand_dims(img, axis=0)
    
    prediction = model.predict(img, verbose=0)
    angle = float(prediction[0][0])
    throttle = float(prediction[0][1])
    
    result = {"angle": angle, "throttle": throttle}
    print(json.dumps(result))

if __name__ == "__main__":
    predict(sys.argv[1])