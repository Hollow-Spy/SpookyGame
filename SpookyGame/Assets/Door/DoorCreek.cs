using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCreek : MonoBehaviour
{
    ConfigurableJoint joint;
    Rigidbody body;
    float lastrot;
    AudioSource doorcreek;
    public GameObject doorstepsfx;
    Vector3 oldrot;
    GameObject doorsfx;
    void Start()
    {
        oldrot = transform.eulerAngles;
        doorcreek = GetComponent<AudioSource>();
        joint = GetComponent<ConfigurableJoint>();
        body = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.relativeVelocity.magnitude > 3)
        {
            Instantiate(doorstepsfx, transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
       

        if(Mathf.Abs (body.angularVelocity.y) > .35f)
        {
            doorcreek.volume = body.angularVelocity.magnitude * .1f;
           if(!doorcreek.isPlaying)
            {
                doorcreek.pitch = Random.Range(.8f, 1.1f);
                doorcreek.Play();

            }
          
            if ( Vector3.Distance(transform.eulerAngles,oldrot) < 20f && body.angularVelocity.y > .65f)
            {
                
                if(doorsfx == null )
                {
                    doorsfx = Instantiate(doorstepsfx, transform.position, Quaternion.identity);
                    doorsfx.GetComponent<AudioSource>().volume *= 1 * Mathf.Abs( body.angularVelocity.y * .5f);

                }
            }
                lastrot = body.angularVelocity.y;
        }  
        else
        {
            doorcreek.Stop();
        }
        
    }
}
