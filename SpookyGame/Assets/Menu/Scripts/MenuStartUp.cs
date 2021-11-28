using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuStartUp : MonoBehaviour
{
    public Slider BrightnessSlider;
    [SerializeField] ScreenBrightness brightness;
    public void ChangeBrightness()
    {
        PlayerPrefs.SetFloat("Brightness", BrightnessSlider.value);
        brightness.ChangeBrightness(BrightnessSlider.value);

    }

    public void Distortion(bool with)
    {
        if(with)
        {
            PlayerPrefs.SetInt("Distortion", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Distortion", 0);

        }
    }

}
