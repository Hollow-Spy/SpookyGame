using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPitch : MonoBehaviour
{
    [SerializeField] float minPitch, maxPitch;
    void Start()
    {
        GetComponent<AudioSource>().pitch = Random.Range(minPitch, maxPitch);
    }

   
  
}
