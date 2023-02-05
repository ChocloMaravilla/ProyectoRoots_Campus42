using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSettings : MonoBehaviour
{
    [SerializeField] public Slider brightness;
    [SerializeField] public Slider volume;
    [SerializeField] public TMP_Dropdown quality;

    private 

    void Start()
    {
        
    }

    public void SetVolumePref()
    {
        //Debug.Log(volume.value);
        PlayerPrefs.SetFloat("volumeAudio", volume.value);
    }

    public void SetBrightnessPref()
    {
        PlayerPrefs.SetFloat("brillo", brightness.value);
    }

    public void SetQualityPref()
    {
        PlayerPrefs.SetInt("numeroDeCalidad", quality.value);
    }
}
