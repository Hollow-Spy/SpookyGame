using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WetSignSlow : MonoBehaviour
{
    PlayerController controller;
    bool SlipCooldown=false;
    JanitorBasic Jani;
    Transform CamTrans;
    [SerializeField] GameObject SlipSound;

    Transform playerbody;
    private void Start()
    {
         controller = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<PlayerController>();
        Jani = GameObject.FindGameObjectWithTag("Janitor").GetComponent<JanitorBasic>();
        CamTrans = GameObject.FindGameObjectWithTag("MainCamera").transform;
        playerbody = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(DissapearCoroutine());

    }


    IEnumerator DissapearCoroutine()
    {
        yield return new WaitForSeconds(25);

        while (Vector3.Distance(transform.position,playerbody.position) < 20 )
        {
            yield return null;

        }

        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player") )
        {
            controller.speed = controller.SlowSpeed;
            other.GetComponentInChildren<Footprint>().TimeActive = 10;
           
       

            if(controller.is_sprinting && !SlipCooldown)
            {
                SlipCooldown = true;
                controller.enabled = false;

                StartCoroutine(SlipNumerator());
            }
        }
    }

    IEnumerator SlipNumerator()
    {
        Rigidbody body = controller.GetComponentInChildren<Rigidbody>();
        body.drag = 3;
        CamTrans.GetComponent<HeadBop>().enabled = false;
        Jani.ChaseSlip();
        Instantiate(SlipSound, transform.position, Quaternion.identity);

        while( !(CamTrans.transform.eulerAngles.x > 28 && CamTrans.transform.eulerAngles.x < 32) )
        {
            yield return null;
            CamTrans.transform.localEulerAngles = Vector3.Lerp(CamTrans.transform.localEulerAngles, new Vector3(30, 0, 0), 20 * Time.deltaTime);
          

        }
      
        while ( !(CamTrans.transform.eulerAngles.x > 290 && CamTrans.transform.eulerAngles.x < 310) )
        {
            

            yield return null;
            CamTrans.Rotate(-12, 0, 0);

        }

        while(CamTrans.localPosition.y > -1.2f )
        {
            yield return null;
            CamTrans.Rotate(-2, 0, 4);
            CamTrans.localPosition = Vector3.MoveTowards(CamTrans.localPosition, new Vector3(CamTrans.localPosition.x, -1.2f, CamTrans.localPosition.z), 20 * Time.deltaTime );
        }

        
        Vector3 OGPos = CamTrans.localPosition;
        float TimeLapsed = 0;
        float FrequencyTick = 0;
        float magnitude = 0.15f;
        float duration = .15f;
        float frequency = 0.026f;



        while (TimeLapsed < duration)
        {
            yield return null;
            TimeLapsed += Time.deltaTime;

            FrequencyTick += Time.deltaTime;
            if (FrequencyTick >= frequency)
            {
                FrequencyTick = 0;
                CamTrans.localPosition = new Vector3(OGPos.x + Random.Range(-1, 1) * magnitude, OGPos.y + Random.Range(-1, 1) * magnitude, OGPos.z);
            }
        }
        CamTrans.localPosition = OGPos;

        yield return new WaitForSeconds(.15f);

        while (!(CamTrans.transform.eulerAngles.x > 25 && CamTrans.transform.eulerAngles.x < 35))
        {

            yield return null;
            CamTrans.Rotate(8, 0, -4);

        }

        yield return new WaitForSeconds(.15f);

        while(CamTrans.localEulerAngles.y > 5 && CamTrans.localEulerAngles.z > 5)
        {
            yield return null;
            CamTrans.Rotate(0, 2, -2);

           // CamTrans.transform.localEulerAngles = Vector3.Lerp(CamTrans.transform.localEulerAngles, new Vector3(0, 0, 0), 20 * Time.deltaTime);
        }
        CamTrans.localEulerAngles = new Vector3(CamTrans.localEulerAngles.x, 0, 0);

        while (CamTrans.localPosition.y < 0.415f)
        {
            yield return null;
           
            CamTrans.localPosition = Vector3.MoveTowards(CamTrans.localPosition, new Vector3(CamTrans.localPosition.x, 0.415f, CamTrans.localPosition.z), 3 * Time.deltaTime);

            CamTrans.Rotate(-1, 0, 0);
        }

        body.drag = 0;
        CamTrans.GetComponent<HeadBop>().enabled = true;
        controller.enabled = true;



        yield return new WaitForSeconds(8);
        SlipCooldown = false;

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            controller.speed = controller.OGSpeed;
        }

    }
}
