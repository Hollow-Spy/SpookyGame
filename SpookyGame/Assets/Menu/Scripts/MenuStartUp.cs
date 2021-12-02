using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuStartUp : MonoBehaviour
{
    public Slider BrightnessSlider;
    [SerializeField] ScreenBrightness brightness;

    private void Start()
    {
        BrightnessSlider.value = PlayerPrefs.GetFloat("Brightness");
    }
    public void ChangeBrightness()
    {
      
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
