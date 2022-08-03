using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeTaskMeter : MonoBehaviour
{
    [SerializeField] bool active;
    [SerializeField] float GreenZoneMin, GreenZoneMax, RedZoneMax, YellowZoneMax;
    [SerializeField] int CurrentRot;
    [SerializeField] Transform PointerRot, PointerObj,PipeObj;
    [SerializeField] ParticleSystem SmokeParticles;
    Vector3 OGPipePos;
    [SerializeField] float rotSpeed;
    [SerializeField] AudioSource SmokeSound, PipeSound;
    [SerializeField] float decaySpeed;

    float CurrentLevel=1;

    [SerializeField] float frequencyShake, MagnitudeShake;
    [SerializeField] float PipeShakeDivider;
    [SerializeField] float pipevolume;
    [SerializeField] GameObject CurrenctActivatorObj;

    float PressureIncrement=1;
    int SolidPressureIncrement=1;

    [SerializeField] float TickTimer;
    float Tick;

    float lastLevelCheck=0;

    private void Start()
    {
        OGPipePos = PipeObj.transform.localPosition;
    }

    private void OnEnable()
    {
        ResetPressureTimer();
        StartCoroutine(DecayMeterNumerator());
        StartCoroutine(PointerShacker());
        StartCoroutine(PipeShaker());
    }


    public void ReducePressure()
    {
      
        if(Tick <= 0)
        {
           if(lastLevelCheck==0)
            {
                lastLevelCheck = CurrentLevel;
            }
            Tick = TickTimer;



            CurrentRot += SolidPressureIncrement;
            PressureIncrement += Time.deltaTime * 3;
            SolidPressureIncrement += (int)PressureIncrement;
           
           
            if (CurrentLevel == 3 && lastLevelCheck == 1)
            {
                CurrenctActivatorObj.SendMessage("PressureBroke");
            }
            lastLevelCheck = CurrentLevel;
        }
        else
        {
            Tick -= Time.deltaTime;
        }
      
    }

    public void ResetPressureTimer()
    {
        SolidPressureIncrement = 1;
        PressureIncrement = 1;
        lastLevelCheck = 0;
    }

    IEnumerator PipeShaker()
    {
        while (true)
        {
            float power = MagnitudeShake / PipeShakeDivider;
            yield return new WaitForSeconds(frequencyShake * 2 / CurrentLevel);
            PipeObj.localPosition = new Vector3(PipeObj.localPosition.x + Random.Range(-1, 2) * power * CurrentLevel, PipeObj.localPosition.y + Random.Range(-1, 2) * power * CurrentLevel, PipeObj.localPosition.z);
            yield return new WaitForSeconds(frequencyShake * 2 / CurrentLevel);
            PipeObj.localPosition = OGPipePos;

            if(!PipeSound.isPlaying)
            {
                PipeSound.pitch = Random.Range(.8f, 1.2f);
                PipeSound.volume = pipevolume + CurrentLevel / 7;
                PipeSound.Play();

            }



            if(CurrentLevel == 3)
            {
                if (!SmokeParticles.isPlaying)
                {
                    SmokeParticles.Play();
                }

                if(!SmokeSound.isPlaying)
                {
                    SmokeSound.pitch = Random.Range(.8f, 1.2f);
                    SmokeSound.Play();
                }

            }
            else
            {
                SmokeParticles.Stop();
                SmokeSound.Stop();
                
            }
        }
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
         
            if (CurrentRot != GreenZoneMax + 1)
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
            RotClamp();
         

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
                    Debug.Log("red");

                    CurrentLevel = 3;
                   

                }
            }

        }

    }
}
