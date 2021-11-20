using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideDesk : MonoBehaviour
{

    public Transform checkpos;
    public bool NotLocker;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") )
        {
           

          other.GetComponentInParent<PlayerController>().Hiding(checkpos.position,NotLocker);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {


            other.GetComponentInParent<PlayerController>().NotHiding();
        }
    }
}
