using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCreek : MonoBehaviour
{
    ConfigurableJoint joint;
    Rigidbody body;
    float lastrot;
    AudioSource doorcreek;
    void Start()
    {
        doorcreek = GetComponent<AudioSource>();
        joint = GetComponent<ConfigurableJoint>();
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(body.angularVelocity);

        if(Mathf.Abs (body.angularVelocity.y) > .35f)
        {
            doorcreek.volume = body.angularVelocity.magnitude * .2f;
           if(!doorcreek.isPlaying)
            {
                doorcreek.Play();

            }
            
            if((lastrot >= 0 && body.angularVelocity.y < 0) || (lastrot <= 0 && body.angularVelocity.y  > 0))
            {
                Debug.Log("step");
            }
                lastrot = body.angularVelocity.y;
        }  
        else
        {
            doorcreek.Stop();
        }
        
    }
}
