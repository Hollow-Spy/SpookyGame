using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSoundTrigger : MonoBehaviour
{
    StepSounds sounds;
    [SerializeField] int index;
    private void Start()
    {
        sounds = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<StepSounds>();
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            sounds.SFXIndex = index;
          
        }
              
    }
}
