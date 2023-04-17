using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerSlider : MonoBehaviour {
    public Slider slider;

    public void setMaxValue(float max) {
        slider.maxValue = max;
        slider.value = max;
    }

    public void setValue(float value) {
        slider.value = value;
    }
}
