using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
   

    float secondsWorth;
    [SerializeField] Transform secondPointer;
    [SerializeField] Transform hourPointer;

   public int seconds;
   public int minutes;
    float time=0;
     void Start()
    {

        //10 mins
        secondsWorth = 0.0303030303f;
       

     
    }
  

    void Update()
    {
       
       if(hourPointer.transform.eulerAngles.x  >= 88 && hourPointer.transform.eulerAngles.x <= 94)
        {
            Debug.Log(TaskOrganizer.Score);
        }
       
            if(time >= secondsWorth)
           {
           
            time -= secondsWorth;
            seconds++;
            if (seconds >= 60)
            {
                seconds = 0;

                minutes++;

                secondPointer.transform.Rotate(0, -6f, 0);

                hourPointer.transform.Rotate(0, -0.5f, 0);
            }

           }



        time += Time.deltaTime;
        
    }
       


}
