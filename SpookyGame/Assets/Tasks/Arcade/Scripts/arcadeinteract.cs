using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arcadeinteract : MonoBehaviour
{


    [SerializeField] Transform ZoomPos;
    Transform CameraPos;
    bool Zoomed;
    Vector3 OGcam;
    IEnumerator ZoomInumerator, ZoomBackIenumerator;
    [SerializeField] GameObject Crosshairs;
    [SerializeField] GameObject canvasGroup;
 
    bool active;
    private void OnEnable()
    {
        active = true;
   
    }

    public void TaskComplete(bool failed)
    {
        if (active)
        {
            active = false;
            canvasGroup.SetActive(false);
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

    private void OnDisable()
    {
        if (Zoomed)
        {
            if (CameraPos)
            {
                CameraPos.position = OGcam;
                Crosshairs.SetActive(true);
            }
            Cursor.lockState = CursorLockMode.Locked;
            GameObject.FindGameObjectWithTag("Player").GetComponentInParent<PlayerController>().enabled = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().isKinematic = false;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<HeadBop>().enabled = true;
            GameObject.FindGameObjectWithTag("Janitor").GetComponent<JanitorBasic>().blind = false;

            Zoomed = false;
            canvasGroup.SetActive(false);
        }
    }


    public void Interaction()
    {

        if (!Zoomed && !GameObject.FindObjectOfType<JanitorBasic>().Chasing)
        {

            Zoomed = true;

            OGcam = GameObject.FindGameObjectWithTag("MainCamera").transform.position; //get cam pos to return later
            CameraPos = GameObject.FindGameObjectWithTag("MainCamera").transform;

            //the actives / deactives
            Crosshairs.SetActive(false);
            GameObject.FindGameObjectWithTag("Player").GetComponentInParent<PlayerController>().enabled = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().isKinematic = true;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<HeadBop>().enabled = false;
            GameObject.FindGameObjectWithTag("Janitor").GetComponent<JanitorBasic>().blind = true;

            //end

            canvasGroup.SetActive(true);
            ZoomInumerator = ZoomCoroutine();
            
            StartCoroutine(ZoomInumerator);


           
        }
    }

    private void Update()
    {
        if(Zoomed)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                canvasGroup.SetActive(false);
                ReturnPlayer();
            }
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
        GameObject.FindGameObjectWithTag("Janitor").GetComponent<JanitorBasic>().blind = false;
        Zoomed = false;

    }

    IEnumerator ZoomCoroutine()
    {
      
        //zoom in
        while (Vector3.Distance(CameraPos.position, ZoomPos.position) > .2f)
        {

            yield return new WaitForSeconds(.05f);
            CameraPos.position = Vector3.Lerp(CameraPos.position, ZoomPos.position, Time.deltaTime * 15);
        }




    }

}
