using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locker : MonoBehaviour
{
    public Transform otherObject;
    public Transform objToRotate;
    public float speed = 1f;
     bool rotate = false;

   

    Quaternion startRotation;
    Quaternion targetRotation;
    //handle hidden trigger
    public BoxCollider HideTrigger;
    [SerializeField] GameObject lockerSound;

    void Start() 
    {
        startRotation = objToRotate.transform.rotation;
    
    }


    void Update()
    {


        if (objToRotate.transform.rotation.normalized == otherObject.rotation.normalized)
        {
          
            targetRotation = startRotation;
           
        }
        else if (objToRotate.transform.rotation == startRotation)
        {
            targetRotation = otherObject.rotation;
         
        }

        if (rotate)
        {
            objToRotate.transform.rotation = Quaternion.SlerpUnclamped(objToRotate.transform.rotation, targetRotation, speed * Time.deltaTime);
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
            if (objToRotate.transform.rotation.normalized == targetRotation.normalized)
            {
                rotate = false;
            }
        }


    }

    public void Interaction()
    {
        if(!rotate)
        {
            Instantiate(lockerSound, transform.position, Quaternion.identity);
            if (HideTrigger.enabled)
            {
                HideTrigger.enabled = false;
                GameObject.FindGameObjectWithTag("Player").GetComponentInParent<PlayerController>().NotHiding();

            }
            else
            {
                HideTrigger.enabled = true;

            }
        }
      


        rotate = true;
    }



}
