using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatSueak : MonoBehaviour
{
    public Transform[] SquealPos;
    public GameObject[] Squeak;
    ParticleSystem particles;
    float spawntime;
    int index=0;


    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        spawntime = particles.main.duration;
        StartCoroutine(SquealNumerator());
    }
    IEnumerator SquealNumerator()
    {
        while(true)
        {
            yield return new WaitForSeconds(spawntime + 0.00000001f);
            if (particles.particleCount > 0)
            {
                StartCoroutine(SqueakSpot());


            }

        }
        

    }

    IEnumerator SqueakSpot()
    {

        for (int i = 0; i < 3; i++)
        {
           
            yield return new WaitForSeconds(1.6f);
           
            if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, SquealPos[index].position) < 3.5f)
            {
              
                Instantiate(Squeak[Random.Range(0, Squeak.Length)], SquealPos[index].position, Quaternion.identity);


            }
            index++;


        }
        index = 0;
    }

 

  
}
