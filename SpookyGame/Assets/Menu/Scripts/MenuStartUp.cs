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
        if(PlayerPrefs.GetFloat("Volume") == 0)
        {
            PlayerPrefs.SetFloat("Volume", .5f);
        }
        BrightnessSlider.value = PlayerPrefs.GetFloat("Brightness");

        Cursor.lockState = CursorLockMode.None;

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
