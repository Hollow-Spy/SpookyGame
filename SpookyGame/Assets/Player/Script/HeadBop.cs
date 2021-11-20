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

    float shakepower;
    float shakeduration;
    float shakefrequency;

    bool shaking;

    void Start()
    {
        ogfrequency = frequency;
        timebop = frequency;
        ogPos = transform.localPosition;
        body = controller.body;
    }

    public void Shake(float power, float time, float frequency)
    {
        shakepower = power;
        shakefrequency = frequency;
        shakeduration = time;

        IEnumerator shakecoroutine;
        shakecoroutine = ShakeNumerator();
        StartCoroutine(shakecoroutine);
    }
 IEnumerator ShakeNumerator()
    {
        shaking = true;
        while(shakeduration > 0)
        {
            
            yield return new WaitForSeconds(shakefrequency);
            shakeduration -= Time.deltaTime;
            transform.localPosition = new Vector3(transform.localPosition.x + Random.Range(-1, 1), transform.localPosition.y + Random.Range(-1, 1), transform.localPosition.z) * shakepower;
            yield return new WaitForSeconds(shakefrequency);
            transform.localPosition = ogPos;
        }
        shaking = false;


    }


    // Update is called once per frame
    void Update()
    {
        if(!shaking)
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
}
