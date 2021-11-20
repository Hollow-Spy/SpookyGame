using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskDrawers : MonoBehaviour
{

    bool moving = false;

    public float speed;

    public Vector3 openPos, closedPos, targetPos;

    void Start()
    {
        closedPos = transform.localPosition;    // I'm using localPosition so that it work if object is rotated 
        openPos = closedPos + new Vector3(0f, -0.00377f, 0f);
    }

    void Update()
    {

        
        if (transform.localPosition == openPos)
        {
            targetPos = closedPos;
        }
        else if (transform.localPosition == closedPos)
        {
            targetPos = openPos;
        }

        if (moving)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, Time.deltaTime * speed);

           // transform.position -= transform.up * Time.deltaTime * speed;

            if (transform.localPosition == targetPos)
            {
                moving = false;
            }
        }


    }



    public void Interaction()
    {
        moving = true;
    }


}
