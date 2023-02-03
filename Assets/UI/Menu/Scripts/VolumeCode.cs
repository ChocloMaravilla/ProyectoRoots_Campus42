using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeCode : MonoBehaviour
{
    public Slider slider;
    public float sliderValue;
    public Image imageSilence;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("volumeAudio", 0.5f);
        AudioListener.volume = slider.value;
        CheckIfIAmMuted();
    }

    public void ChengeSlider(float valor)
    {
        sliderValue = valor;
        PlayerPrefs.SetFloat("volumeAudio", sliderValue);
        AudioListener.volume = slider.value;
        CheckIfIAmMuted();
    }

    public void CheckIfIAmMuted()
    {
        if (sliderValue == 0)
        {
            imageSilence.enabled = true;
        } 
        else 
        {
            imageSilence.enabled = false;
        }
    }
}