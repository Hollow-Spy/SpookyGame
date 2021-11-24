using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class VignettePulse : MonoBehaviour
{
    PostProcessVolume m_Volume;
    public static Vignette m_Vignette;

    void Start()
    {
        m_Vignette = ScriptableObject.CreateInstance<Vignette>();
        m_Vignette.enabled.Override(true);
        m_Vignette.intensity.Override(1f);

        m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Vignette);
    }
}

//Manipulating the Stack | Package Manager UI website. (n.d.). Unity. https://docs.unity3d.com/Packages/com.unity.postprocessing@2.0/manual/Manipulating-the-Stack.html