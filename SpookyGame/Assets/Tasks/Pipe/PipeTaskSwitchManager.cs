using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeTaskSwitchManager : MonoBehaviour
{

    int flippedSwitches = 0;
    [SerializeField] PipeTaskSwitch[] switches;
    [SerializeField] CameraShake switchFrame;
    [SerializeField] PipeTaskMeter meter;

    private void OnEnable()
    {
        for(int i=0;i<switches.Length;i++)
        {
            switches[i].UnflipSwitch();
        }
        flippedSwitches = 0;

    }

    public void SwitchFlip()
    {
        flippedSwitches++;
        if (flippedSwitches == 3)
        {
            StartCoroutine(FlipTimer());
        }
    }
    public void PressureBroke()
    {
        meter.busy = false;
        switchFrame.ShakeScreen(0.16f, 0.021f, 0.35f);
        meter.ResetPressureTimer();
        StopAllCoroutines();
        for (int i = 0; i < 3; i++)
        {
            switches[i].UnflipSwitch();
            flippedSwitches--;
        }

    }

        IEnumerator FlipTimer()
    {
        float TimerValue = Random.Range(1, 2);
        while (TimerValue > 0)
        {
            meter.ReducePressure();
            meter.busy = true;

            TimerValue -= Time.deltaTime;
            yield return null;
        }
      
        int randomFlips = Random.Range(0, 3);
        switch(randomFlips)
        {
            case 0:
                switches[Random.Range(0, 3)].UnflipSwitch();
                flippedSwitches--;
                break;
            case 1:
                int safebut = Random.Range(0, 3);
                for(int i=0;i<3;i++)
                {
                    if(i != safebut)
                    {
                        switches[i].UnflipSwitch();
                        flippedSwitches--;
                    }
                }
                break;
            case 2:

                for (int i = 0; i < 3; i++)
                {
                        switches[i].UnflipSwitch();
                        flippedSwitches--;
                }

                break;
        }
        meter.ResetPressureTimer();
        meter.busy = false;


    }



}
