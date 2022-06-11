using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecurityTask : MonoBehaviour
{
    [SerializeField] Transform ZoomPos;
    Transform CameraPos;
    bool Zoomed;
    Vector3 OGcam;
    IEnumerator ZoomInumerator, ZoomBackIenumerator,SecondNumerator;
    [SerializeField] GameObject Crosshairs;
   
    [SerializeField] GameObject CameraOFF;
    bool active;

    [SerializeField] Text secondText,Cameratext,PercentageText;

    [SerializeField] GameObject VideoQuad,CameraSwitchSFX;
    int cameraIndex;
    [SerializeField] GameObject[] Cameras;
    [SerializeField] AudioSource CameraMoveSound,CamRecordSound;
    int Progress;
    [SerializeField] float progressTickTimer;
    float Timer;
    bool wonAlready;
    private void OnEnable()
    {
        wonAlready = false;
        PercentageText.text = "0%";
        Progress = 0;
        CamRecordSound.Stop();
        Timer = 0;

        cameraIndex = 0;
        VideoQuad.SetActive(false);
     
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
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<StepSounds>().enabled = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().isKinematic = false;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<HeadBop>().enabled = true;
            GameObject.FindGameObjectWithTag("Janitor").GetComponent<JanitorBasic>().blind = false;

            Zoomed = false;
            
            VideoQuad.SetActive(false);
           
        }
    }
    public void TaskComplete(bool failed)
    {
        if (active)
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

        if (!Zoomed)
        {

            Zoomed = true;

            OGcam = GameObject.FindGameObjectWithTag("MainCamera").transform.position; //get cam pos to return later
            CameraPos = GameObject.FindGameObjectWithTag("MainCamera").transform;

            //the actives / deactives
            Crosshairs.SetActive(false);
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<StepSounds>().enabled = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().isKinematic = true;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<HeadBop>().enabled = false;
            GameObject.FindGameObjectWithTag("Janitor").GetComponent<JanitorBasic>().blind = true;

            //end


            ZoomInumerator = ZoomCoroutine();
            SecondNumerator = MiliSecondCountCoroutine();



            StartCoroutine(ZoomInumerator);

            StartCoroutine(SecondNumerator);


        }
    }

    private void Update()
    {
        if (Zoomed)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SwitchCamera(true);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                SwitchCamera(false);
            }
            if(Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(ZoomReturnCoroutine());
            }

            if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) )
            {
                if(!CameraMoveSound.isPlaying)
                {
                    CameraMoveSound.Play();
                }
              
               
            }
            else
            {
                CameraMoveSound.Stop();

            }
        }
    }

    void SwitchCamera(bool nextcam)
    {
        Cameras[cameraIndex].SetActive(false);
        if(nextcam)
        {

            cameraIndex++;
            if (cameraIndex >= Cameras.Length)
            {
                cameraIndex = 0;

            }
          

        }
        else
        {
            cameraIndex--;
            if (cameraIndex < 0)
            {
                cameraIndex = Cameras.Length - 1;
            }
        }
        Instantiate(CameraSwitchSFX, transform.position, Quaternion.identity);
        Cameras[cameraIndex].SetActive(true);
        Cameratext.text = Cameras[cameraIndex].name;
    }

    public void RecordingIncrement()
    {
        if (Progress == 100)
        {
            if(!wonAlready)
            {
                wonAlready = true;
                StartCoroutine(TaskDone(false));
            }
            return;

        }

        if (Progress == 0)
        {
            CamRecordSound.Play();
            Progress++;
        }
        else
        {
            CamRecordSound.UnPause();
        }

        Timer += Time.deltaTime;
        if(Timer > progressTickTimer)
        {
            Progress++;
            Timer = 0;
            PercentageText.text = Progress + "%";
        }
     
    }
    public void RecordingStop()
    {
        CamRecordSound.Pause();

    }
    IEnumerator MiliSecondCountCoroutine()
    {
        while(true)
        {
            yield return null;
            float t = Time.time;

            string seconds = (t % 60).ToString("f2");
        
                secondText.text = "00:00:" + seconds;
          
          

        }
    }

    public void ReturnPlayer()
    {
        ZoomBackIenumerator = ZoomReturnCoroutine();
        StartCoroutine(ZoomBackIenumerator);

    }

    IEnumerator ZoomReturnCoroutine()
    {
        VideoQuad.SetActive(false);
        Cameras[cameraIndex].SetActive(false);

        while (Vector3.Distance(CameraPos.position, OGcam) > .2f)
        {

            yield return new WaitForSeconds(.05f);
            CameraPos.position = Vector3.Lerp(CameraPos.position, OGcam, Time.deltaTime * 15);
        }
        CameraPos.position = OGcam;

        Crosshairs.SetActive(true);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<StepSounds>().enabled = true;
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

        VideoQuad.SetActive(true);

        Cameras[cameraIndex].SetActive(true);
        Cameratext.text = Cameras[cameraIndex].name;

    }
}
