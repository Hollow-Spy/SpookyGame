using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
   

    [SerializeField] float secondsWorth;
    [SerializeField] Transform secondPointer;
    [SerializeField] Transform hourPointer;
    [SerializeField] GameObject Camera;
   public int seconds;
   public int minutes;
    float time=0;
    bool active=true;
    bool ending;
    [SerializeField]bool MainClock;
    [SerializeField] GameObject ticksound;
    [SerializeField] int scoreToWin;
    [SerializeField] string currentLevel;
    [SerializeField] string levelToUnlock;

    [SerializeField] TaskOrganizer taskorganizer;
     void Start()
    {

        if(secondsWorth <= 0)
        {
            secondsWorth =  0.0101010101f;
        }

        PlayerPrefs.SetInt(currentLevel, 1);

        //12 mins
        //  secondsWorth = 0.0303030303f;

        //5 around mins
        //secondsWorth = 0.0303030303f / 2.1f;
        //0.01443001442f


        //3 minutes?
        //  secondsWorth = 0.0303030303f / 3f;
        //0.0101010101

    }


    public void EndGame()
    {
        ending = true;
        PlayerPrefs.SetInt("Score", TaskOrganizer.Score);
        GameObject.FindGameObjectWithTag("Player").transform.parent.gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("Janitor").SetActive(false);
        GameObject.Find("UICanvas").SetActive(false);
        Camera.SetActive(true);
        Camera.tag = "MainCamera";
        secondPointer.transform.localRotation = new Quaternion(0.706077278f, -0.0679001883f, 0.0678997561f, -0.703839719f);

        if(PlayerPrefs.GetInt("Score") >= scoreToWin)
        {
            PlayerPrefs.SetInt(levelToUnlock, 1);
        }

        taskorganizer.RemoveAllTaks();

        StartCoroutine(EndNumerator());

    }
    IEnumerator EndNumerator()
    {
        yield return new WaitForSeconds(2.3f);
        active = false;
        yield return new WaitForSeconds(2);
        GameObject.Find("Sceneloader").GetComponent<SceneLoader>().LoadScene("EndGame");
    }
    void Update()
    {
     
     

       
       if(hourPointer.transform.eulerAngles.x  >= 88 && hourPointer.transform.eulerAngles.x <= 94 && MainClock && !ending)
        {
           
            EndGame();
            Debug.Log(TaskOrganizer.Score);
        }
       

            if(time >= secondsWorth)
           {
           while(time > 0)
            {
                time -= secondsWorth;
                seconds++;
            }
           
            if (seconds >= 60)
            {
                seconds = 0;

                minutes++;

                secondPointer.transform.Rotate(0, -6f, 0);
                Instantiate(ticksound, transform.position, Quaternion.identity);


                hourPointer.transform.Rotate(0, -0.5f, 0);
            }

           }


            if(active)
        {
            time += Time.deltaTime;
        }
     
        
    }
       


}
