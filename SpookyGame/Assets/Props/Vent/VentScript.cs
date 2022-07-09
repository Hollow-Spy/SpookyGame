using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentScript : MonoBehaviour
{
    float timeout;
    Rigidbody body;
     ConfigurableJoint joint;
    HandAnimator animator;
    bool active;
    [SerializeField] GameObject VentOpenSound;
   private void Start()
    {
        joint = GetComponent<ConfigurableJoint>();
        body = GetComponent<Rigidbody>();
        animator = GameObject.FindGameObjectWithTag("MainCamera").GetComponentInChildren<HandAnimator>();
    }

    public void Interaction()
    {
        if(!active)
        {
            Instantiate(VentOpenSound, transform.position, Quaternion.identity);
            timeout = 1;
            active = true;
            gameObject.layer = 0;
            animator.vent = this;
            int rand = Random.Range(0, 2);
            if (rand == 0)
            {
                animator.PlaySlap();
            }
            else
            {
                animator.PlayPunch();
            }
        }

       
       

    }
    public void Open()
    {
        timeout = 1;
        joint.angularYMotion = ConfigurableJointMotion.Limited;
      
        body.AddTorque( GameObject.FindGameObjectWithTag("MainCamera").transform.right  * -150, ForceMode.Impulse);
    }



    void Update()
    {

      
        if(transform.eulerAngles.x < 30 && transform.eulerAngles.x > -30)
        {

            timeout -= Time.deltaTime;
            if(timeout <0)
            {
                active = false;
                gameObject.layer = 8;
                joint.angularYMotion = ConfigurableJointMotion.Locked;
            }
        }
        else
        {
            timeout = 1;
        }
    }




}
