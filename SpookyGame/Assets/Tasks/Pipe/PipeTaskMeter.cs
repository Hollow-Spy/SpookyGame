using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeTaskMeter : MonoBehaviour
{
    [SerializeField] bool active;
    [SerializeField] float GreenZoneMin, GreenZoneMax, RedZoneMax, YellowZoneMax;
    [SerializeField] int CurrentRot;
    [SerializeField] Transform PointerRot, PointerObj;
    [SerializeField] float rotSpeed;

    [SerializeField] float decaySpeed;

    float CurrentLevel=1;

    [SerializeField] float frequencyShake, MagnitudeShake;
 

    private void OnEnable()
    {
        StartCoroutine(DecayMeterNumerator());
        StartCoroutine(PointerShacker());
    }

    IEnumerator PointerShacker()
    {
        while(true)
        {
            
            yield return new WaitForSeconds(frequencyShake / CurrentLevel);
           
            PointerObj.localEulerAngles = new Vector3(0, 0, Random.Range(-1, 2) * MagnitudeShake * CurrentLevel);
            yield return new WaitForSeconds(frequencyShake / CurrentLevel);
            PointerObj.localEulerAngles = new Vector3(0, 0, 0);


        }
    }

    IEnumerator DecayMeterNumerator()
    {
        while (true)
        {
            yield return new WaitForSeconds(decaySpeed);
            if(CurrentRot != GreenZoneMax + 1)
            {
                CurrentRot--;
            }
           
            RotClamp();

        }

    }
    private void RotClamp()
    {
        if(CurrentRot > 360)
        {
            CurrentRot = 0;
        }

        if(CurrentRot < 0)
        {
            CurrentRot = 359;
        }

    }

    void Update()
    {
        if(active)
        {
            Vector3 NewRot = new Vector3(0, 0, CurrentRot);
            PointerRot.localEulerAngles = Vector3.Slerp(NewRot, PointerRot.localEulerAngles, rotSpeed * Time.deltaTime);


            if(CurrentRot >= GreenZoneMin && CurrentRot < GreenZoneMax)
            {
               
                CurrentLevel = 1;
            }
            else
            {
                if(CurrentRot < YellowZoneMax && CurrentRot > RedZoneMax)
                {
                   
                    CurrentLevel = 2;
                }
                else
                {
                
                    CurrentLevel = 3;

                }
            }

        }

    }
}
