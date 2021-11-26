using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerZoom : MonoBehaviour
{
    [SerializeField] Transform ZoomPos;
    Transform CameraPos;
    bool Zoomed;
    Vector3 OGcam;
    IEnumerator ZoomInumerator,ZoomBackIenumerator;
    [SerializeField] GameObject Crosshairs;
    public GameObject[] puzzles;
    [SerializeField] GameObject computeroff;
    bool active;

    private void OnEnable()
    {
        active = true;
        computeroff.SetActive(false);

    }

    private void OnDisable()
    {

        computeroff.SetActive(true);
        if(Zoomed)
        {
            if(CameraPos)
            {
                CameraPos.position = OGcam;
                Crosshairs.SetActive(true);
            }
           
            GameObject.FindGameObjectWithTag("Player").GetComponentInParent<PlayerController>().enabled = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().isKinematic = false;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<HeadBop>().enabled = true;
            Zoomed = false;
        }
    }
    public void TaskComplete(bool failed)
    {
        if(active)
        {
            active = false;

            StartCoroutine(TaskDone(failed));
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


    public void Interaction()
    {
       
        if(!Zoomed)
        {
            
            Zoomed = true;

            OGcam = GameObject.FindGameObjectWithTag("MainCamera").transform.position; //get cam pos to return later
            CameraPos = GameObject.FindGameObjectWithTag("MainCamera").transform;

            //the actives / deactives
            Crosshairs.SetActive(false);
            GameObject.FindGameObjectWithTag("Player").GetComponentInParent<PlayerController>().enabled = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().isKinematic = true;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<HeadBop>().enabled = false;
            //end


            ZoomInumerator = ZoomCoroutine();

            StartCoroutine( ZoomInumerator );
            
        }
    }
    public void ReturnPlayer()
    {
        ZoomBackIenumerator = ZoomReturnCoroutine();
        StartCoroutine(ZoomBackIenumerator);

    }

    IEnumerator ZoomReturnCoroutine()
    {
        while (Vector3.Distance(CameraPos.position, OGcam) > .2f)
        {

            yield return new WaitForSeconds(.05f);
            CameraPos.position = Vector3.Lerp(CameraPos.position, OGcam, Time.deltaTime * 15);
        }
        CameraPos.position = OGcam;

        Crosshairs.SetActive(true);
        GameObject.FindGameObjectWithTag("Player").GetComponentInParent<PlayerController>().enabled = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().isKinematic = false;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<HeadBop>().enabled = true;
        Zoomed = false;

    }

    IEnumerator ZoomCoroutine()
    {
        Instantiate(puzzles[Random.Range(0, puzzles.Length)], transform.position, Quaternion.identity);
        //zoom in
       while(Vector3.Distance(CameraPos.position,ZoomPos.position) > .2f )
        {
           
            yield return new WaitForSeconds(.05f);
            CameraPos.position = Vector3.Lerp(CameraPos.position,ZoomPos.position,Time.deltaTime * 15);
        }
        



    }

}
