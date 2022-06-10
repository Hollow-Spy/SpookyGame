using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamMove : MonoBehaviour
{

    [SerializeField] Vector3 RotationLockRight;
    [SerializeField] Vector3 RotationLockLeft, RotationLockDown;
    [SerializeField] LayerMask RayMasks;

    [SerializeField] SecurityTask securitytask;
    

    void Update()
    {
     
        if(Input.GetKey(KeyCode.D))
        {
          if(transform.eulerAngles.y < RotationLockRight.y)
            {
              
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 1, 0);
            }
              
          
           
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (transform.eulerAngles.y > RotationLockLeft.y)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 1, 0);
            }
               
          
        }
        if (Input.GetKey(KeyCode.W) )
        {
           if(transform.eulerAngles.x > 0 )
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x - 1, transform.eulerAngles.y , 0);
            }
         

        }
        if (Input.GetKey(KeyCode.S))
        {
            if(transform.eulerAngles.x < RotationLockDown.x)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x + 1, transform.eulerAngles.y, 0);

            }

        }

        if (transform.eulerAngles.x >= 350)
        {
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        }
        RaycastHit hit;
        if(Physics.SphereCast(transform.position,.2f,transform.forward ,out hit, 35, RayMasks))
        {
            bool counting = false;
            if (hit.collider.gameObject.CompareTag("Janitor"))
            {
                counting = true;
                securitytask.RecordingIncrement();
            }
            if(!counting)
            {
                securitytask.RecordingStop();

            }
           
        }

    }
}
