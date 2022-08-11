using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footprint : MonoBehaviour
{
    PlayerController controller;
    [SerializeField] GameObject Print, StepSFX;
    [SerializeField] Transform PlayerBod;
 
    public float delay;
    float cooldown, ogdelay;
    bool alternate;

    public float TimeActive=10;

 

    void Start()
    {
        ogdelay = delay;
        controller = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<PlayerController>();
    }


    void Update()
    {

        if (TimeActive > 0 && controller.is_walking && !controller.is_airborn  ) 
        {

            delay -= Time.deltaTime;



            if (delay < 0)
            {

                GameObject sound = Instantiate(StepSFX, transform.position, Quaternion.identity);
                sound.GetComponent<AudioSource>().pitch = Random.Range(.8f, 1.1f);

                GameObject print = Instantiate(Print, transform.position, Quaternion.identity  );
                print.transform.eulerAngles = new Vector3(90, PlayerBod.transform.eulerAngles.y, 0);
                if(alternate)
                {
                    print.transform.position = new Vector3(print.transform.position.x + 0.36f, transform.position.y, print.transform.position.z);
                    print.transform.localScale = new Vector3(-print.transform.localScale.x,print.transform.localScale.y,print.transform.localScale.z);

                    alternate = false;
                    
                }
                else
                {
                    alternate = true;


                }


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

        if(TimeActive > 0)
        {
            TimeActive -= Time.deltaTime;
        }
      


    }
}
