using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChocoTask : MonoBehaviour
{
    bool holding;
    PlayerInteract interact;
    Rigidbody body;
    [SerializeField] Transform ChocoPos;
    [SerializeField] float throwstrength;
    Vector3 ogpos;
    [SerializeField] Transform[] SpawnPos;

    float delay;

    private void Awake()
    {
        ogpos = ChocoPos.localPosition;
        interact = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerInteract>();
    }

    private void OnEnable()
    {
        transform.position = SpawnPos[Random.Range(0, SpawnPos.Length)].position;
        ChocoPos.localPosition = ogpos;
        transform.SetParent(null);
        GetComponent<BoxCollider>().enabled = true;

        body = GetComponent<Rigidbody>();
        Physics.IgnoreCollision(GetComponent<BoxCollider>(), GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider>(),true);
       

        body.isKinematic = false;
        gameObject.name = "zok";
        holding = false;

    }

    public void Ate()
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
        if(holding)
        {

            holding = false;

            interact.active = true;
        }
       

    }

    public void Update()
    {
       if(holding)
        {
            transform.position = ChocoPos.position;
            transform.localScale = new Vector3(7, 7, 7);


            if (Input.GetMouseButtonDown(0) && delay <= 0)
            {
                
                gameObject.name = "Zork";
                GetComponent<BoxCollider>().enabled = true;

                transform.SetParent(null);
                body.isKinematic = false;
                body.AddForce(GameObject.FindGameObjectWithTag("MainCamera").transform.forward * throwstrength, ForceMode.Impulse);
                holding = false;
                interact.active = true;

            }
            if(delay > 0)
            {
                delay -= Time.deltaTime;
            }

        }

      
    }

    public void Interaction()
    {
        if(!holding)
        {
            body.isKinematic = true;
            gameObject.name = "zok";
            transform.SetParent(ChocoPos);
            GetComponent<BoxCollider>().enabled = false;

            transform.position = new Vector3(0.145f, -0.192f, 0.337f);
            transform.eulerAngles = new Vector3(0f, -23.473f, 75.793f);

            interact.active = false;

            holding = true;
            delay = .1f;

        }
        
    }
}
