using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ScreenBrightness : MonoBehaviour
{
     PostProcessVolume volume;
    AutoExposure exposure;


    private void Start()
    {
        volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out exposure);
        exposure.keyValue.value = PlayerPrefs.GetFloat("Brightness");

    }
    public void ChangeBrightness(float brightness)
    {
        volume.profile.TryGetSettings(out exposure);
        exposure.keyValue.value = brightness;

    }


}
    

