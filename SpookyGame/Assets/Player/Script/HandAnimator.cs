using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimator : MonoBehaviour
{
    public VentScript vent;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    public void PlaySlap()
    {
        animator.SetTrigger("slap");
    }
    public void PlayPunch()
    {
        animator.SetTrigger("punch");

    }


    public void OpenVent()
    {
        vent.Open();
    }
}
