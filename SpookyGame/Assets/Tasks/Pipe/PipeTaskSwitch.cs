using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeTaskSwitch : MonoBehaviour
{
    bool Flipped=false;
    [SerializeField] Material FlippedOnMat, FlippedOffMat;
    [SerializeField] MeshRenderer switchRenderer;
    [SerializeField] PipeTaskSwitchManager manager;
   public void Interaction()
    {
        if(!Flipped)
        {
            Flipped = true;
            switchRenderer.material = FlippedOnMat;
            gameObject.layer = 0;
            manager.SwitchFlip();
        }
    }


    public void UnflipSwitch()
    {
        if(Flipped)
        {
            Flipped = false;
            gameObject.layer = 8;
            switchRenderer.material = FlippedOffMat;
        }
    }


}
