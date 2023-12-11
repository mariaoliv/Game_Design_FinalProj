using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI numberText;
    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
        SetNumberText(slider.value);
        LoadValues();
    }

    public void SetNumberText(float value)
    {
        numberText.text = value.ToString() + "%";
    }

    public void SaveVolume()
    {
        float volumeValue = slider.value;
        PlayerPrefs.SetFloat("VolumeValue", volumeValue);
        LoadValues();
    }
    
    void LoadValues()
    {
        float volumeValue = PlayerPrefs.GetFloat("VolumeValue");
        slider.value = volumeValue;
        AudioListener.volume = volumeValue/100;
    }
}