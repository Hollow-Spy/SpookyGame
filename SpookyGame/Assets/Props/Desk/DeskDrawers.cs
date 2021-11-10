using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskDrawers : MonoBehaviour
{

    bool moving = false;

    public float speed;

    // move by 0.308f
    // axis depends from rotation of the desk
    Vector3 openPos, closedPos, targetPos;

    void Start()
    {
        closedPos = transform.position;
        openPos = closedPos + new Vector3(0.6f, 0f, 0f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            moving = true;

        }
        
        if (transform.position == openPos)
        {
            targetPos = closedPos;
        }
        else if (transform.position == closedPos)
        {
            targetPos = openPos;
        }

        if (moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);

            if (transform.position == targetPos)
            {
                moving = false;
            }
        }


    }

 
}
