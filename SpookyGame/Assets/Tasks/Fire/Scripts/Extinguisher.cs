using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extinguisher : MonoBehaviour
{

    bool holding;
    PlayerInteract interact;
    Rigidbody body;
    [SerializeField] Transform ExtinnguishPos;
    [SerializeField] Transform SpawnPos;
    [SerializeField] ParticleSystem Particles;

    float delay;

    private void OnEnable()
    {
        transform.position = SpawnPos.position;

        transform.SetParent(null);
        GetComponent<BoxCollider>().enabled = true;

        body = GetComponent<Rigidbody>();
        Physics.IgnoreCollision(GetComponent<BoxCollider>(), GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider>(), true);
        interact = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerInteract>();

        
       
        holding = false;

    }


 

    IEnumerator TaskDone(bool failed)
    {
        while (GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<TaskOrganizer>().busy)
        {
            yield return null;
        }
        GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<TaskOrganizer>().RemoveTask(gameObject, failed);

    }


    private void OnDisable()
    {
        if (holding)
        {

            holding = false;

            interact.active = true;
        }


    }

    public void Update()
    {
        if (holding)
        {
            transform.position = ExtinnguishPos.position;
            


            if (Input.GetMouseButton(0))
            {
               
                GetComponent<BoxCollider>().enabled = true;

                Particles.Play();


            }
            else
            {
                Particles.Stop();
            }
          

        }


    }

    public void Interaction()
    {
        if (!holding)
        {
          
            
            transform.SetParent(ExtinnguishPos);
            GetComponent<BoxCollider>().enabled = false;
              /*
            transform.position = new Vector3(0.145f, -0.192f, 0.337f);
            transform.eulerAngles = new Vector3(0f, -23.473f, 75.793f);
              */
            interact.active = false;

            holding = true;
            

        }

    }
}


