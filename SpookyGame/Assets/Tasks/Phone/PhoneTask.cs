using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneTask : MonoBehaviour
{
    [SerializeField] AudioSource phonering;
    [SerializeField] GameObject PhoneCanvas;
    bool PickedUp;
    
    private void OnEnable()
    {
        PickedUp = false;
        phonering.Play();
    }

    public void Interaction()
    {
        if(!PickedUp && !GameObject.FindGameObjectWithTag("Janitor").GetComponent<JanitorBasic>().Chasing)
        {
            PickedUp = true;

            GameObject.FindGameObjectWithTag("Player").GetComponentInParent<PlayerController>().enabled = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().isKinematic = true;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<HeadBop>().enabled = false;
            GameObject.FindGameObjectWithTag("Janitor").GetComponent<JanitorBasic>().blind = true;
            PhoneCanvas.SetActive(true);

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
