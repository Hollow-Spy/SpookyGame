using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraserPlayer : MonoBehaviour
{
    [SerializeField] float Speed;
    Rigidbody2D body;
    bool Alive=true;

    private void OnEnable()
    {
        Alive = true;
    }

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Alive)
        {
            body.velocity = transform.up * Speed;
          
            if(Input.GetKey(KeyCode.A))
            {
                transform.Rotate(0, 0, 6);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(0, 0, -6);


            }
        }
        
    }
}
