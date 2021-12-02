using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PocketLighter : MonoBehaviour
{
    [SerializeField] float fuel;
    bool busy;
    [SerializeField] Animator LighterAnimator;

    // Update is called once per frame
    

    void Update()
    {
        if(Input.GetMouseButtonDown(1) && !busy)
        {
            LighterAnimator.SetBool("on",!LighterAnimator.GetBool("on")) ;       
        }

    }

    public void BusyAnim()
    {
        if(busy)
        {
            busy = false;
        }
        else
        {
            busy = true;

        }
    }

}
