using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraserPlayer : MonoBehaviour
{
    [SerializeField] float Speed;
    Rigidbody2D body;
    bool Alive=true;
    [SerializeField] float MaxSpeed;
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


            if (Input.GetKey(KeyCode.W))
            {
               
                body.AddForce(transform.up * Speed);
                
            }

            if (Input.GetKey(KeyCode.S))
            {

                body.AddForce(-transform.up * Speed);
            }

            if (Input.GetKey(KeyCode.A))
            {

                body.AddForce(-transform.right * Speed);
            }
            if (Input.GetKey(KeyCode.D))
            {

                body.AddForce(transform.right * Speed);


            }

         
            if (body.velocity.magnitude > MaxSpeed)
            {
                body.velocity = new Vector2(Mathf.Lerp(body.velocity.x, MaxSpeed, Time.deltaTime), Mathf.Lerp(body.velocity.y, MaxSpeed, Time.deltaTime));
            }

        }
        
    }
}
