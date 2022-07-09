using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorJumpscareTrigger : MonoBehaviour
{
    [SerializeField] float TriggerChance;
    [SerializeField] DoorJumpscare doorjumpscare;
    [SerializeField] bool RemoveJumpscare;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (RemoveJumpscare)
            {
                doorjumpscare.DisableJumpscare();
            }
            else
            {
                int rand = Random.Range(0, 100);
                if (rand < TriggerChance)
                {
                    doorjumpscare.EnableJumpscare();
                }
            }
                
        }
      
       
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Janitor"))
        {
            doorjumpscare.DisableJumpscare();
        }
    }
}
