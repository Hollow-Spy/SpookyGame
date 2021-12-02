using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowpointer : MonoBehaviour
{
   [SerializeField] Transform a,b,end,start;

    bool up;
    bool shoot;
    bool back;
    [SerializeField] int amout;
    [SerializeField] GameObject win;
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(shoot)
        {
            amout--;
            Destroy(collision.gameObject);
            if (amout <= 0)
            {
                win.SetActive(true);
            }
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if(!shoot && !back)
        {
            if (up)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 9f , transform.position.z);
                if (transform.position.y >= a.transform.position.y)
                {
                    up = false;
                }
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 9f , transform.position.z);
                if (transform.position.y <= b.transform.position.y)
                {
                    up = true;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
               
                
                    shoot = true;

                
            }
        }

        if(shoot)
        {
            transform.position = new Vector3(transform.position.x + 15 , transform.position.y, transform.position.z);
            if(transform.position.x > end.transform.position.x)
            {
                transform.position = start.position;
                shoot = false;
                back = true;
            }


        }
       if(back)
        {
            transform.position = new Vector3(transform.position.x + 10, transform.position.y, transform.position.z);

            if (transform.position.x >= a.transform.position.x)
            {
                back = false;
            }
        }

    }
}
