using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeTaskManager : MonoBehaviour
{

    [SerializeField] GameObject Door,TaskObjects;
     bool busy;
    [SerializeField] PipeTaskMeter meter1, meter2, meter3;
  
    private void OnEnable()
    {
        busy = false;
        Door.layer = 8;
        TaskObjects.SetActive(true);
        StartCoroutine(Checking());
    }

    IEnumerator Checking()
    {
        while(true)
        {
            yield return new WaitForSeconds(.5f);
            if (!busy && meter1.CurrentLevel == 1 && meter2.CurrentLevel == 1 && meter3.CurrentLevel == 1  && !meter1.busy && !meter2.busy && !meter3.busy)
            {
                busy = true;
                StartCoroutine(TaskDone(false));
            }
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

    private void OnDisable()
    {
        Door.layer = 0;
        Door.GetComponent<PipeTaskDoor>().CloseDoor();

    }
}
