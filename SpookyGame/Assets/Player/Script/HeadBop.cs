using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBop : MonoBehaviour
{
    [SerializeField] PlayerController controller;
    Rigidbody body;
    [SerializeField] float frequency;
    [SerializeField] float magnitude;
    bool up;
    Vector3 ogPos;
    float timebop;
    float ogfrequency;

    void Start()
    {
        ogfrequency = frequency;
        timebop = frequency;
        ogPos = transform.localPosition;
        body = controller.body;
    }

    // Update is called once per frame
    void Update()
    {
     if(controller.is_sprinting)
        {
            frequency = ogfrequency / 2;
        }
        else
        {
            frequency = ogfrequency;
        }

        
            if (!controller.is_airborn && controller.is_walking)
            {
                if (up)
                {
                    transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, transform.localPosition.y + magnitude, transform.localPosition.z), Time.deltaTime);
                }
                else
                {
                    transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, transform.localPosition.y - magnitude, transform.localPosition.z), Time.deltaTime);

                }
                timebop -= Time.deltaTime;
                if (timebop <= 0)
                {
                
                    timebop = frequency;
                    up = !up;
                }

            }
           else
            {
             
                transform.localPosition =  Vector3.Lerp(transform.localPosition,new Vector3(transform.localPosition.x,ogPos.y,transform.localPosition.z),Time.deltaTime * 10);
            }


        
    }
}
