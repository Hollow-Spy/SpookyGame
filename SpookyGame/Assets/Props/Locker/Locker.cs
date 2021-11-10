using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locker : MonoBehaviour
{
    public Transform otherObject;
    public float speed = 1f;
    bool rotate = false;

    Quaternion startRotation;

    float t;
    Quaternion targetRotation;


    void Start() 
    {
        startRotation = transform.rotation;
    
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            rotate = true;

        }

        if (transform.rotation.normalized == otherObject.rotation.normalized)
        {
            targetRotation = startRotation;
        }
        else if (transform.rotation == startRotation)
        {
            targetRotation = otherObject.rotation;
        }

        if (rotate)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, t += speed * Time.deltaTime);

            if (transform.rotation.normalized == targetRotation.normalized)
            {
                rotate = false;
            }
        }


      //  t += speed * Time.deltaTime







    }
}
