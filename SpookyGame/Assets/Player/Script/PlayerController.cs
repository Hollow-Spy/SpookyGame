using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //reference https://www.youtube.com/watch?v=b1uoLBp2I1w
    Vector3 MoveInput;
    Vector2 MouseInput;
    float xRot;
    public bool is_walking;
    public bool is_airborn;
    public bool is_sprinting;
   

    [SerializeField] LayerMask GroundLayer;
    [SerializeField] Transform GroundCheck;

    [SerializeField] Transform PlayerCam;
    public Rigidbody body;
    public bool is_crouched;
     public float speed;
    public float sprintmultiplier;
    [SerializeField] float sensitivity;
    [SerializeField] float jumpforce;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;


        //dISTORTION SETTING
       // RenderSettings.ambientLight = new Color(RenderSettings.ambientLight.r, RenderSettings.ambientLight.g, RenderSettings.ambientLight.b, .0f);
    }
    
    
    
    // Update is called once per frame
    void Update()
    {
        MoveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        MouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") );
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            is_crouched = true;
            transform.localScale = new Vector3(1, .5f, 1);
        }

    

       
        if (Input.GetKeyUp(KeyCode.LeftControl) && !Physics.Raycast(transform.position, Vector3.up,1)) 
        {
           
            is_crouched = false;
            transform.localScale = new Vector3(1, 1, 1);

        }
     
        if (!Physics.CheckSphere(GroundCheck.position, .1f, GroundLayer))
        {
            is_airborn = true;

        }
        else
        {
            is_airborn = false;
        }


        MovePlayer();
        MoveCamera();
    }
    
    void MovePlayer()
    {
        if(MoveInput.x != 0 || MoveInput.z != 0)
        {
            is_walking = true;
        }
       else
        {
            is_walking = false;
        }


        float movespeed = speed;
        if(Input.GetKey(KeyCode.LeftShift))
        {
            movespeed *= sprintmultiplier;
            is_sprinting = true;
        }
        else
        {
            is_sprinting = false;
        }

        if(is_crouched)
        {
            movespeed = speed * .5f;

        }

        Vector3 MoveVector = body.transform.TransformDirection(MoveInput) * movespeed;


        body.velocity = new Vector3(MoveVector.x,body.velocity.y,MoveVector.z);

        if(Input.GetKeyDown(KeyCode.Space) && !is_crouched)
        {
            if(Physics.CheckSphere(GroundCheck.position,.1f,GroundLayer ))
            {
                body.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);

            }
        }

    }
    
    void MoveCamera()
    {
        xRot -= MouseInput.y * sensitivity;

        xRot = Mathf.Clamp(xRot, -50, 50);

        body.transform.Rotate(0, MouseInput.x * sensitivity, 0);
        PlayerCam.transform.localRotation = Quaternion.Euler(xRot,0,0);
    }

}
