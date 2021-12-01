using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingSmash : MonoBehaviour
{
   [SerializeField] float shakepower;
    [SerializeField] float shakeduration;
    [SerializeField] float shakefrequency;
    float shaketime;
    Vector3 ogPos;
    bool shaking;
    public GameObject vendingMachine;
    public GameObject Drank;
    private Transform drankPos;
    public int canAmount = 3;
    [SerializeField] Animator handanim;
    [SerializeField] Transform inchild;
    AudioSource audioplayer;
   [SerializeField] AudioClip[] hitsounds;

    private void OnEnable()
    {
        audioplayer = GetComponent<AudioSource>();
        audioplayer.pitch = .8f;
    }

    private void Start()
    {
      
        shaketime = shakeduration;        //unimportante, just for shaking
        ogPos = vendingMachine.transform.localPosition;
        drankPos = GameObject.Find ("RealSodaPoint").transform;
    }

    public void Interaction()    //as long as we are active we can interact with the Vending machine
    {             
        audioplayer.clip = hitsounds[Random.Range(0, hitsounds.Length)];
        audioplayer.Play();
        handanim.SetTrigger("fastpunch");
        GameObject.FindGameObjectWithTag("Janitor").GetComponent<JanitorBasic>().Investigate(inchild.position);

        Shake();    
        
        SpawnCan();
            
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

    public void SpawnCan()
    {
      int randomNumber = Random.Range(1, 500);

      if(randomNumber == 1)
      {        
         Instantiate(Drank, drankPos.position, Quaternion.identity);        
      }
    }

    IEnumerator ShakeNumerator()
    {
        shaking = true;
        while (shaketime > 0)
        {

            yield return new WaitForSeconds(shakefrequency);
            shaketime -= Time.deltaTime;
            vendingMachine.transform.localPosition = new Vector3(vendingMachine.transform.localPosition.x + Random.Range(-1, 1) * shakepower , vendingMachine.transform.localPosition.y + Random.Range(-1, 1) * shakepower, vendingMachine.transform.localPosition.z + Random.Range(-1, 1) * shakepower) ;
            yield return new WaitForSeconds(shakefrequency);
           vendingMachine.transform.localPosition = ogPos;
        }
        shaking = false;

      
    }
    

}