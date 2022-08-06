using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WetSignSlow : MonoBehaviour
{
    PlayerController controller;
    private void Start()
    {
         controller = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<PlayerController>();

    }
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player") )
        {
            controller.speed = controller.SlowSpeed;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            controller.speed = controller.OGSpeed;
        }

    }
}
