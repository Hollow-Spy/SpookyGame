using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSacrificeTask : MonoBehaviour
{
    bool WaitingSacrifice,Sacrificing;
   
    [SerializeField] Animator InstructionDisplay;
    [SerializeField] GameObject[] Meats;
    [SerializeField] Light RedLight;
    [SerializeField] Transform CenterCircle;
    Transform MeatTransform;
   [SerializeField] CameraShake shaker;
    [SerializeField] GameObject Arms,Blackout;

    [SerializeField] Animator Gramoanimator;
    [SerializeField] AudioSource GramoMusic,SpawnSound;
 
  
    private void OnEnable()
    {
        GramoMusic.pitch = -1;
        Gramoanimator.SetFloat("Speed", -1);
        GramoMusic.Play();
       

        for(int i=0;i<Meats.Length;i++)
        {
            Meats[i].layer = 8;
        }

        WaitingSacrifice = true;
        Sacrificing = false;
    }


    IEnumerator TaskDone(bool failed)
    {
        while (GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<TaskOrganizer>().busy)
        {
            yield return null;
        }
        GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<TaskOrganizer>().RemoveTask(gameObject, failed);

    }

    void DisableMaterials()
    {
        Arms.SetActive(false);
        RedLight.gameObject.SetActive(false);
        SpawnSound.Stop();


        if(!MeatTransform && Sacrificing)
        {
            GameObject.FindGameObjectWithTag("Janitor").GetComponent<JanitorBasic>().RitualWarp();
        }

        for (int i = 0; i < Meats.Length; i++)
        {
            Meats[i].layer = 0;
        }
        if(FindObjectOfType<TinyMeat>())
        {
            Destroy(FindObjectOfType<TinyMeat>().gameObject);
        }
       

    }

    private void OnDisable()
    {
        Blackout.SetActive(false);

        GramoMusic.pitch = 1;
        Gramoanimator.SetFloat("Speed", 1);
        GramoMusic.Play();
        DisableMaterials();
    }
    private void OnTriggerStay(Collider other)
    {
        if (WaitingSacrifice && other.CompareTag("Janitor") && Vector3.Distance(other.transform.position,CenterCircle.position ) < 2 )
        {
            InstructionDisplay.SetBool("Show", false);
            other.GetComponent<JanitorBasic>().RitualKill();
            WaitingSacrifice = false;
            StartCoroutine(RitualNumerator());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(WaitingSacrifice)
        {
            if (other.CompareTag("Player"))
            {
                InstructionDisplay.SetBool("Show", true);
            }
            if(other.CompareTag("TinyMeat") )
            {
                InstructionDisplay.SetBool("Show", false);
                MeatTransform = other.transform;
                MeatTransform.gameObject.layer = 0;
                WaitingSacrifice = false;
                StartCoroutine(RitualNumerator());
            }
           

        }
     
    }

   

    IEnumerator RotateMeatNumerator()
    {
        int RotationIncrement=1;
        float tick=0;
        while (Sacrificing)
        {
            yield return null;
            MeatTransform.Rotate(Random.Range(2, 3) + RotationIncrement, Random.Range(2, 6) + RotationIncrement, Random.Range(4, 5) + RotationIncrement);

            tick += Time.deltaTime;
            if(tick > .3f)
            {
                RotationIncrement+=2;
                tick = 0;
            }
        }
    }
    IEnumerator LightFlickerNumerator()
    {
        RedLight.gameObject.SetActive(true);
        RedLight.intensity = 0;
        while (RedLight.intensity < 30)
        {
            RedLight.intensity+=2;
            yield return null;
        }

        while(Sacrificing)
        {
            RedLight.intensity = Random.Range(10, 30);
            yield return new WaitForSeconds(Random.Range(.04f,.08f));
        }
    }

    IEnumerator RitualNumerator()
    {
        Sacrificing = true;
        Gramoanimator.SetFloat("Speed", 0);
        GramoMusic.Stop();
        if(MeatTransform)
        {
            MeatTransform.GetComponent<Rigidbody>().isKinematic = true;

        
        while (Vector3.Distance(MeatTransform.position,CenterCircle.position) > .2f)
        {
            yield return null;
           MeatTransform.position = Vector3.MoveTowards(MeatTransform.position, CenterCircle.position, 3 * Time.deltaTime);
        }
        yield return new WaitForSeconds(.5f);
      
            StartCoroutine(RotateMeatNumerator());

        }
        
        StartCoroutine(LightFlickerNumerator());
        SpawnSound.Play();
        shaker.ShakeScreen(0.26f, 0.021f, 5);
        yield return new WaitForSeconds(1);
        Arms.SetActive(true);
        yield return new WaitForSeconds(3);

        Blackout.SetActive(true);
        yield return new WaitForSeconds(1);
        StartCoroutine(TaskDone(false));


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && WaitingSacrifice)
        {
            InstructionDisplay.SetBool("Show", false);

        }
    }
}
