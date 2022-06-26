using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            gameObject.GetComponentInParent<AmbienceManager>().SwitchAmbience(GetComponent<AudioSource>());
        }
    }
}
