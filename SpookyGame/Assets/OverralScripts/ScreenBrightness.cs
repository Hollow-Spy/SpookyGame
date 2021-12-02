using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ScreenBrightness : MonoBehaviour
{
  


    private void Start()
    {
        Screen.brightness = PlayerPrefs.GetFloat("Brightness");

    }
    public void ChangeBrightness(float brightness)
    {

        RenderSettings.ambientLight = new Color(brightness, brightness, brightness, 1.0f);
        
        PlayerPrefs.SetFloat("Brightness", brightness);
        Debug.Log(brightness);

    }


}
    

