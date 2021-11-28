using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extinguisher : MonoBehaviour
{

    bool holding;
    PlayerInteract interact;
    
    [SerializeField] Transform ExtinnguishPos;
    [SerializeField] Transform SpawnPos;
    [SerializeField] ParticleSystem Particles;
    [SerializeField] GameObject Fire;

    private void OnEnable()
    {
        Particles.gameObject.SetActive(true);
        Particles.Stop();

        transform.position = SpawnPos.position;
        Fire.SetActive(true);
        transform.SetParent(null);
        GetComponent<CapsuleCollider>().enabled = true;

        Physics.IgnoreCollision(GetComponent<CapsuleCollider>(), GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider>(), true);
        interact = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerInteract>();

        holding = false;
    }


    public void FireGone()
    {
        StartCoroutine(TaskDone(false));
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
            
            Fire.SetActive(false);

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

                Debug.Log("shooting");

                if(!Particles.isPlaying)
                {
                    Particles.Play();
                }
              


                RaycastHit hit;

                if (Physics.Raycast(GameObject.FindGameObjectWithTag("MainCamera").transform.position, GameObject.FindGameObjectWithTag("MainCamera").transform.forward, out hit, 4))
                {
                    if(hit.transform.gameObject == Fire)
                    {
                        Fire.GetComponent<Fire>().LoseHealth();
                    }
                }

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
            GetComponent<CapsuleCollider>().enabled = false;
            transform.eulerAngles = new Vector3(0, 200, 0f);
            /*
          transform.position = new Vector3(0.145f, -0.192f, 0.337f);
          transform.eulerAngles = new Vector3(0f, -23.473f, 75.793f);
            */
            interact.active = false;

            holding = true;
            

        }

    }
}


