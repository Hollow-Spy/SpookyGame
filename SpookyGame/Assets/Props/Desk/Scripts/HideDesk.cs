using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideDesk : MonoBehaviour
{

    public Transform checkpos;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 10 )
        {
            Debug.Log("hi");

            GameObject.FindGameObjectWithTag("Player").GetComponentInParent<PlayerController>().Hiding(checkpos.position);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            Debug.Log("bye");

            GameObject.FindGameObjectWithTag("Player").GetComponentInParent<PlayerController>().NotHiding();
        }
    }
}
