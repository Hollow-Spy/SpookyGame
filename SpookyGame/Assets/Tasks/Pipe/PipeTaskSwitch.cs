using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeTaskSwitch : MonoBehaviour
{
    bool Flipped=false;
    [SerializeField] Material FlippedOnMat, FlippedOffMat;
    [SerializeField] MeshRenderer switchRenderer;
    [SerializeField] PipeTaskSwitchManager manager;
    [SerializeField] GameObject FlipSoundOn, FlipSoundOff;
   public void Interaction()
    {
        if(!Flipped)
        {
            Flipped = true;
            switchRenderer.material = FlippedOnMat;
            gameObject.layer = 0;
            manager.SwitchFlip();
            Instantiate(FlipSoundOn, transform.position, Quaternion.identity);
        }
    }


    public void UnflipSwitch()
    {
        if(Flipped)
        {
            Flipped = false;
            gameObject.layer = 8;
            switchRenderer.material = FlippedOffMat;
            Instantiate(FlipSoundOff, transform.position, Quaternion.identity);

        }
    }


}
