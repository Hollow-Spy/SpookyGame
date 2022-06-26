using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RatAi : MonoBehaviour
{
    [SerializeField] float RunDistance;
    NavMeshAgent agent;
    Transform playerpos;
    [SerializeField] float ratspeed;
    [SerializeField] Transform spawnpos;
    bool active;
    AudioSource stepsound;
    [ SerializeField] GameObject[] ratsqueal;
    [SerializeField] float screamdelay;
    float delay;
    [SerializeField] GameObject ratcute;
   
    private void OnEnable()
    {
        GetComponentInChildren<MeshRenderer>().enabled = true;
        stepsound = GetComponent<AudioSource>();
        delay = .3f;
        playerpos = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        active = true;
        transform.position = spawnpos.position;
        agent.speed = ratspeed;
    }

    public void Interaction()
    {
        if(active)
        {
            agent.speed = 0;
            active = false;
            Instantiate(ratcute, transform.position, Quaternion.identity);
            GetComponentInChildren<MeshRenderer>().enabled = false;
            StartCoroutine(TaskDone(false));
  
        }

    }

    IEnumerator TaskDone(bool failed)
    {
        while (GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<TaskOrganizer>().busy)
        {
            yield return null;
        }
        GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<TaskOrganizer>().RemoveTask(gameObject, failed);

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(playerpos.position,transform.position);
       

        if(distance < RunDistance && active)
        {
            Vector3 dirplayer = transform.position - GameObject.FindGameObjectWithTag("Player").transform.position;

            Vector3 newpos = transform.position + dirplayer;

            agent.SetDestination(newpos);
            if(!stepsound.isPlaying)
            {
                stepsound.Play();
                stepsound.pitch = Random.Range(.8f, 1.1f);
            }


            delay -= Time.deltaTime;
            if(delay <0)
            {
                delay = screamdelay;
                Instantiate(ratsqueal[Random.Range(0, ratsqueal.Length)], transform.position, Quaternion.identity);
            }
        }
        else
        {
            stepsound.Stop();
        }
    }
}
