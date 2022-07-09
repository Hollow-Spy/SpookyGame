using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ventcreak : MonoBehaviour
{
    Rigidbody body;
    AudioSource doorcreek;
    [SerializeField] float VolumeMultiplier=1;
 
    void Start()
    {
    
        doorcreek = GetComponent<AudioSource>();
       
        body = GetComponent<Rigidbody>();
    }


    void Update()
    {
      

        if (Mathf.Abs(body.angularVelocity.magnitude) > 0f)
        {
            doorcreek.volume = body.angularVelocity.magnitude * .1f * VolumeMultiplier;
            if (!doorcreek.isPlaying)
            {
                doorcreek.pitch = Random.Range(.8f, 1.1f);
                doorcreek.Play();

            }

 
          
        }
        else
        {
            doorcreek.Stop();
        }

    }
}
