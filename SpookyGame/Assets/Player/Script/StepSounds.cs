using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSounds : MonoBehaviour
{
    PlayerController controller;
    [SerializeField] GameObject[] StepSFX;
    public int SFXIndex;
    public float delay;
     float cooldown,ogdelay;
    void Start()
    {
        ogdelay = delay;
        controller = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<PlayerController>();  
    }

 
    void Update()
    {
        
        if(controller.is_walking && !controller.is_airborn)
        {
           
            delay -= Time.deltaTime;

            if(delay <0)
            {
              GameObject sound =  Instantiate(StepSFX[SFXIndex], transform.position, Quaternion.identity);
                sound.GetComponent<AudioSource>().pitch = Random.Range(.8f, 1.1f);

                if (controller.is_sprinting)
                {
                    delay = ogdelay / 1.5f;
                }
                else
                {
                    delay = ogdelay;
                }
            }
            


        }


    }
}
