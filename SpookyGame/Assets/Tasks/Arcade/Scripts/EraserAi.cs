using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EraserAi : MonoBehaviour
{
    [SerializeField] float Speed;
    Rigidbody2D body;
    bool Alive = true;
    int index=0;

    [System.Serializable]
    public class Instructions
    {
       public float Time;
        public bool dirRight;
        public float PressTime;
    }

    public Instructions[] instructions;

    private void OnEnable()
    {
        Alive = true;
        StartCoroutine(StartRun());
    }

    private void Start()
    {

        
        body = GetComponent<Rigidbody2D>();


    }
    IEnumerator StartRun()
    {

        while(index < instructions.Length)
        {
            yield return new WaitForSeconds(instructions[index].Time );

            float delay = instructions[index].PressTime;

            if(instructions[index].dirRight)
            {
                while (delay > 0)
                {
                  
                        transform.Rotate(0, 0, -6);

                    delay -= Time.deltaTime;
                    yield return null;
                }
            }
            else
            {
                while (delay > 0)
                {
                        transform.Rotate(0, 0, 6);

                    delay -= Time.deltaTime;
                    yield return null;
                }
            }

            index++;
           
        }
       
    }

   
    void Update()
    {
        if (Alive)
        {
            body.velocity = transform.up * Speed;
        }

    }

}
