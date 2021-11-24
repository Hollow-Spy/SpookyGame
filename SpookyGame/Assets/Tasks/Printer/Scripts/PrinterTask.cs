using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrinterTask : MonoBehaviour
{
    [SerializeField] float delay;
    float cooldown;
    bool active;

   [SerializeField] float shakepower;
    [SerializeField] float shakeduration;
    [SerializeField] float shakefrequency;
    float shaketime;
    Vector3 ogPos;
    bool shaking;
    public GameObject printer;
    public float Luck;
    public int TryLimit;
    int attempts=0;
    [SerializeField] Animator handanim;

    AudioSource audioplayer;
   [SerializeField] AudioClip[] hitsounds;

    private void OnEnable()
    {
        audioplayer = GetComponent<AudioSource>();
        attempts = 0;
        audioplayer.pitch = .8f;
        active = true;    //kinda important, we want to let the object know once he is activated by the task organizer he will be now active, both script wise and inspector wise
    }

    private void Start()
    {
      
        shaketime = shakeduration;        //unimportante, just for shaking

        ogPos = printer.transform.localPosition;
    }

    public void Interaction()    //as long as we are active we can interact with the printer
    {
        if(active && cooldown <= 0)
        {
            
            float shot = Random.Range(0, 100); //every time the player hits this he has X percentage to get it, if he dosent it builts up attempts and when it reaches the limit
                                               //the task will be done automatically
           audioplayer.clip = hitsounds[Random.Range(0, hitsounds.Length)];
            audioplayer.Play();
            audioplayer.pitch += 0.04f;
            handanim.SetTrigger("fastpunch");
            if(shot <= Luck)
               {
                audioplayer.pitch -= .3f;
                active = false;
                StartCoroutine(TaskDone(false));
                return;
            }
            else
            {
                attempts++;
                if (attempts >= TryLimit )
                {
                    audioplayer.pitch += .3f;
                    active = false;
                    StartCoroutine(TaskDone(false));
                    return;
                }
            }
           

            cooldown = delay;
            Shake();
               
            }


        }

    IEnumerator TaskDone(bool failed)
    {
        while(GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<TaskOrganizer>().busy)
        {
            yield return null;
        }
        GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<TaskOrganizer>().RemoveTask(gameObject, failed);

    }

    public void Shake() 
    {
     
        
        IEnumerator shakecoroutine;
        if(shaking)
        {
            StopAllCoroutines();
        }
        shaketime = shakeduration;

        shakecoroutine = ShakeNumerator();
        StartCoroutine(shakecoroutine);
    }

    IEnumerator ShakeNumerator()
    {
        shaking = true;
        while (shaketime > 0)
        {

            yield return new WaitForSeconds(shakefrequency);
            shaketime -= Time.deltaTime;
            printer.transform.localPosition = new Vector3(printer.transform.localPosition.x + Random.Range(-1, 1) * shakepower , printer.transform.localPosition.y + Random.Range(-1, 1) * shakepower, printer.transform.localPosition.z + Random.Range(-1, 1) * shakepower) ;
            yield return new WaitForSeconds(shakefrequency);
           printer.transform.localPosition = ogPos;
        }
        shaking = false;

      
    }

    private void Update()
    {
        if(cooldown > 0)     
        {
            cooldown -= Time.deltaTime;
        }
    }

}
