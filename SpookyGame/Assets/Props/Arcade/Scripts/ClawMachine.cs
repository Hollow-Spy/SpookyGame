using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawMachine : MonoBehaviour
{
    [SerializeField] GameObject movableObject;
    [SerializeField] Animator ClawAnimator;
    [SerializeField] float posLeft, posRight;
    bool moving=true;
    [SerializeField] bool goingleft;
    [SerializeField] float speed;
    bool busy;
    [SerializeField] GameObject Soda;
    [SerializeField] float leftLimit, rightLimit;

    public void Check()
    {
        if(movableObject.transform.localPosition.z < leftLimit && movableObject.transform.localPosition.z > rightLimit)
        {
            Soda.transform.parent = ClawAnimator.gameObject.transform;
        }
        else
        {
            StartCoroutine(WaitCooldown());
        }
    }

    IEnumerator WaitCooldown()
    {
        yield return new WaitForSeconds(.5f);
        busy = false;
        moving = true;
    }

    public void Interaction()
    {
        if(!busy)
        {
            busy = true;
            moving = false;

            ClawAnimator.SetTrigger("Pick");


        }
    }




    private void Update()
    {
        if(moving)
        {
            if(goingleft)
            {
               
                if(movableObject.transform.localPosition.z < posLeft)
                {
                    movableObject.transform.localPosition = new Vector3(movableObject.transform.localPosition.x, movableObject.transform.localPosition.y, movableObject.transform.localPosition.z + speed * Time.deltaTime);
                }
                else
                {
                    goingleft = false;
                }

            }
            else
            {
                Debug.Log(movableObject.transform.localPosition.z);
                if (movableObject.transform.localPosition.z > posRight)
                {
                    movableObject.transform.localPosition = new Vector3(movableObject.transform.localPosition.x, movableObject.transform.localPosition.y, movableObject.transform.localPosition.z + -speed * Time.deltaTime);
                }
                else
                {
                    goingleft = true;
                }
            }
        }
        
    }


}
