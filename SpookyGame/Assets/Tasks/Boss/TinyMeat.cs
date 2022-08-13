using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinyMeat : MonoBehaviour
{
    bool holding;
    PlayerInteract interact;
    Rigidbody body;
    [SerializeField] Transform ChocoPos;
    [SerializeField] float throwstrength;
    [SerializeField] GameObject MeatPickSFX;
  

    float delay;

    private void Awake()
    {
       

        body = GetComponent<Rigidbody>();
        Physics.IgnoreCollision(GetComponent<BoxCollider>(), GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider>(), true);
        holding = false;
     
        interact = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerInteract>();
        gameObject.tag = "Untagged";
        GameObject[] OtherMeats = GameObject.FindGameObjectsWithTag("TinyMeat");
        for(int i=0;i<OtherMeats.Length;i++)
        {
                Destroy(OtherMeats[i]);
        }
        ChocoPos = GameObject.Find("ChocoPos").transform;
        gameObject.tag = "TinyMeat";
        Interaction();

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
    private void OnDestroy()
    {
        if (holding)
        {
            interact.active = true;
        }
    }


    public void Update()
    {
        if (holding)
        {
            transform.position = ChocoPos.position;
          //  transform.localScale = new Vector3(7, 7, 7);


            if (Input.GetMouseButtonDown(0) && delay <= 0)
            {

                gameObject.tag = "TinyMeat";
                GetComponent<BoxCollider>().enabled = true;

                transform.SetParent(null);
                body.isKinematic = false;
                body.AddForce(GameObject.FindGameObjectWithTag("MainCamera").transform.forward * throwstrength, ForceMode.Impulse);
                holding = false;
                interact.active = true;

            }
            if (delay > 0)
            {
                delay -= Time.deltaTime;
            }

        }


    }

    public void Interaction()
    {
        if (!holding)
        {
            Instantiate(MeatPickSFX, transform.position, Quaternion.identity);

            body.isKinematic = true;
            gameObject.tag = "Untagged";
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
