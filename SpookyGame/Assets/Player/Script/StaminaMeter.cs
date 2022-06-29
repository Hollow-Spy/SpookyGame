using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StaminaMeter : MonoBehaviour
{
   
    [SerializeField] GameObject FillerPoint,NoStaminaText;
    float StaminaValue=1;
   [SerializeField] float StaminaIncreaseRate,StaminaDecreaserate, ExhaustLock;
    [SerializeField] Image fillerImage;
    bool ZeroHit;
    public bool CanRun=true;

    [SerializeField] Animator Baranimator;
    [SerializeField] JanitorBasic Janitor;
    [SerializeField] GameObject PlayerBody;
    Vector3 OGPos;
    [SerializeField] float ShakePower,ShakeDelay;
    float delay;
    private void Start()
    {
        OGPos = transform.position;
        
    }



    private void Update()
    {
        if(Janitor.Chasing)
        {
           if(delay < 0)
            {
               
                transform.position = new Vector3(OGPos.x + Random.Range(-ShakePower, ShakePower), OGPos.y + Random.Range(-ShakePower, ShakePower), OGPos.z);
                delay = ShakeDelay;
            }
           else
            {
                delay -= Time.deltaTime;

                ShakeDelay = .01f * Vector3.Distance(Janitor.transform.position, PlayerBody.transform.position);
                ShakePower = 30 / Vector3.Distance(Janitor.transform.position, PlayerBody.transform.position);
            }


        }

        if (Input.GetKey(KeyCode.LeftShift) && !ZeroHit)
        {
            if (StaminaValue > 0)
            {
                Baranimator.SetBool("Sprinting", true);
                StaminaValue -= StaminaDecreaserate * Time.deltaTime;
                FillerPoint.transform.localScale = new Vector3(StaminaValue, 1, 1);
            }
            else
            {
                ZeroHit = true;
                fillerImage.color = Color.red;
                NoStaminaText.SetActive(true);
                CanRun = false;
                StaminaValue = 0;
                FillerPoint.transform.localScale = new Vector3(0, 1, 1);
            }

        }
        else
        {
            if(ZeroHit && StaminaValue > ExhaustLock)
            {
                CanRun = true;
                ZeroHit = false;
                fillerImage.color = Color.white;
                NoStaminaText.SetActive(false);
                CanRun = true;
            }
         

            if (StaminaValue < 1)
            {
              
                StaminaValue += StaminaIncreaseRate * Time.deltaTime;
                FillerPoint.transform.localScale = new Vector3(StaminaValue, 1, 1);
            }
            else
            {
                Baranimator.SetBool("Sprinting", false) ;
                StaminaValue = 1;
                FillerPoint.transform.localScale = new Vector3(1, 1, 1);
            }
        }

    }
}
