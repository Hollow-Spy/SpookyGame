using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeeTask : MonoBehaviour
{
    [SerializeField] GameObject Flush;
    bool active;
    [SerializeField] Transform checkpos;
    private void OnEnable()
    {
        active = true;

    }


    private void OnDisable()
    {
            active = false;
    }

    public void Interaction()
    {
        if(active)
        {
            active = false;
            Instantiate(Flush, transform.position, Quaternion.identity);
            GameObject.FindGameObjectWithTag("Janitor").GetComponent<JanitorBasic>().Investigate(checkpos.position);
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


   
}
