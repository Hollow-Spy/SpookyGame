using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuStartUp : MonoBehaviour
{
    public Slider BrightnessSlider,AudioSlider;
    [SerializeField] ScreenBrightness brightness;
    [SerializeField] GameObject[] Locks;
    [SerializeField] string unlockablename; // LevelUnlocked
  
    private void Start()
    {

      //  PlayerPrefs.SetInt("LevelUnlocked0", 0);  //1 for active

        if(PlayerPrefs.GetFloat("Volume") == 0)
        {
            PlayerPrefs.SetFloat("Volume", .5f);
        }
        BrightnessSlider.value = PlayerPrefs.GetFloat("Brightness");

        Cursor.lockState = CursorLockMode.None;

        for(int i=0;i<Locks.Length;i++)
        {
            string name = unlockablename + i;

            if(PlayerPrefs.GetInt(name) == 1)
            {
                Locks[i].SetActive(false);
            }
        }

    }
    public void ChangeBrightness()
    {
      
        brightness.ChangeBrightness(BrightnessSlider.value);

    }
    public void ChangeAudio()
    {
        AudioListener.volume = AudioSlider.value;
        PlayerPrefs.SetFloat("Volume", AudioSlider.value);
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
