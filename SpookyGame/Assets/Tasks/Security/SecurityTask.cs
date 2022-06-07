using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityTask : MonoBehaviour
{
    [SerializeField] Transform ZoomPos;
    Transform CameraPos;
    bool Zoomed;
    Vector3 OGcam;
    IEnumerator ZoomInumerator, ZoomBackIenumerator;
    [SerializeField] GameObject Crosshairs;
    public GameObject[] puzzles;
    [SerializeField] GameObject CameraOFF;
    bool active;

    GameObject currentpuzzle;
    private void OnEnable()
    {
        active = true;
        CameraOFF.SetActive(false);

    }

    private void OnDisable()
    {

        CameraOFF.SetActive(true);
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
            Destroy(currentpuzzle);
        }
    }
    public void TaskComplete(bool failed)
    {
        if (active)
        {
            active = false;
            Destroy(currentpuzzle);
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

        if (!Zoomed)
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


            ZoomInumerator = ZoomCoroutine();

            StartCoroutine(ZoomInumerator);

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
       // currentpuzzle = Instantiate(puzzles[Random.Range(0, puzzles.Length)], transform.position, Quaternion.identity);

        //zoom in
        while (Vector3.Distance(CameraPos.position, ZoomPos.position) > .58f)
        {

            yield return new WaitForSeconds(.05f);
            CameraPos.position = Vector3.Lerp(CameraPos.position, ZoomPos.position, Time.deltaTime * 8);
        }




    }
}
