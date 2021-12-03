using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PocketLighter : MonoBehaviour
{
    [SerializeField] float fuel=10;
    float maxfuel;
    bool busy;
    [SerializeField] Animator LighterAnimator;
    [SerializeField] Animator lighterbarAnimator;
    RectTransform lighterBarTransform;
    [SerializeField] Animator nofuelAlert;
    // Update is called once per frame
    private void Start()
    {
        maxfuel = fuel;
        lighterBarTransform = lighterbarAnimator.gameObject.GetComponent<RectTransform>();
    }


    void Update()
    {
        if(Input.GetMouseButtonDown(1) && !busy)
        {
            if (!LighterAnimator.GetBool("on"))
            {
                if (fuel > maxfuel * .3f)
                {
                    LighterAnimator.SetBool("on", true);
                    fuel -= maxfuel * .1f;
                }
                else
                {
                    nofuelAlert.SetTrigger("Show");
                }

            }
            else
            {
                LighterAnimator.SetBool("on", false);

            }


        }

        if(fuel < maxfuel && LighterAnimator.GetBool("on")   )
        {
            fuel -= Time.deltaTime;
            lighterbarAnimator.SetBool("Show", true);
            lighterBarTransform.localScale = new Vector3(fuel / maxfuel * .1f, lighterBarTransform.localScale.y, lighterBarTransform.localScale.z); 
       
            if(fuel <=0)
            {
                fuel = 0;
                LighterAnimator.SetBool("on", !LighterAnimator.GetBool("on"));
                nofuelAlert.SetTrigger("Show");
            }

        }
        else
        {
            fuel += 1.5f * Time.deltaTime;
            lighterBarTransform.localScale = new Vector3(fuel / maxfuel * .1f, lighterBarTransform.localScale.y, lighterBarTransform.localScale.z);

            if (fuel >= maxfuel)
            {
                fuel = maxfuel;
                lighterbarAnimator.SetBool("Show", false);
            }
           

        }
       

    }

    public void BusyAnim()
    {
        if(busy)
        {
            busy = false;
        }
        else
        {
            busy = true;

        }
    }

}
