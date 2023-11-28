using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PeacockCooldownBar : MonoBehaviour
{
    public Gradient gradient;
    public Slider slider;
    public Image fill;

    public void SetMaxValue(float val)
    {
        slider.maxValue = val;
        slider.value = val;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetCooldownValue(float val)
    {
        slider.value = val;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
