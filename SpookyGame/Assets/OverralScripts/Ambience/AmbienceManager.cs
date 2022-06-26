using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceManager : MonoBehaviour
{
   
    AudioSource currentsource;

    public void SwitchAmbience(AudioSource current)
    {
        if(currentsource != null)
        {
            currentsource.Stop();
        }
        currentsource = current;
        current.Play();

    }

}
