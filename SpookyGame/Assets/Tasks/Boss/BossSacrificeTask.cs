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
   

    private void OnEnable()
    {
        for(int i=0;i<Meats.Length;i++)
        {
            Meats[i].layer = 8;
        }

        WaitingSacrifice = true;
    }

    void DisableMaterials()
    {
        for (int i = 0; i < Meats.Length; i++)
        {
            Meats[i].layer = 0;
        }
        if(GameObject.Find("TinyMeat"))
        {
            Destroy(GameObject.Find("TinyMeat").gameObject);
        }
       

    }

    private void OnDisable()
    {
        DisableMaterials();
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
        RedLight.range = 0;
        while (RedLight.range < 30)
        {
            RedLight.range+=2;
            yield return null;
        }

        while(Sacrificing)
        {
            RedLight.range = Random.Range(10, 30);
            yield return new WaitForSeconds(Random.Range(.04f,.08f));
        }
    }

    IEnumerator RitualNumerator()
    {
        Sacrificing = true;
        MeatTransform.GetComponent<Rigidbody>().isKinematic = true;
        while(Vector3.Distance(MeatTransform.position,CenterCircle.position) > .2f)
        {
            yield return null;
           MeatTransform.position = Vector3.MoveTowards(MeatTransform.position, CenterCircle.position, 3 * Time.deltaTime);
        }
        yield return new WaitForSeconds(.5f);
        StartCoroutine(RotateMeatNumerator());
        StartCoroutine(LightFlickerNumerator());
        shaker.ShakeScreen(0.26f, 0.021f, 5);

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && WaitingSacrifice)
        {
            InstructionDisplay.SetBool("Show", false);

        }
    }
}
