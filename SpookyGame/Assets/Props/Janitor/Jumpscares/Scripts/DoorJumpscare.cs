using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorJumpscare : MonoBehaviour
{
    [SerializeField] GameObject JumpscareObject;
    bool busy;
  [SerializeField]  Renderer JanitorRenderer;

    [SerializeField] float jumpscareDelay;
    float delay;
    [SerializeField] Animator janitoranimator;
    [SerializeField] GameObject JumpscareSound;

    [SerializeField] Collider visionCollider;
    Transform playerpos;
    Transform campos;
    
  public void EnableJumpscare()
    {
        if(!busy)
        {
            janitoranimator.Play("Idle");
            delay = jumpscareDelay;
            JumpscareObject.SetActive(true);

        }
    }

    private void Start()
    {
        playerpos = GameObject.FindGameObjectWithTag("Player").transform;
        campos = Camera.main.transform;
    }



    private void Update()
    {
        if(JumpscareObject.activeSelf)
        {
            RaycastHit hit1,hit2;

            bool check1 = Physics.Raycast(visionCollider.transform.position, playerpos.position - visionCollider.transform.position,out hit1,30);
            bool check2 = Physics.Raycast(campos.transform.position, campos.forward , out hit2, 30);
           

            if (hit1.collider.CompareTag("Player") && hit2.collider == visionCollider)
            {
                delay -= Time.deltaTime;
                if (delay <= 0 && !busy)
                {
                    busy = true;
                    StartCoroutine(ScaringNumerator());
                }
            }
            else
            {
                delay = jumpscareDelay;
            }

        }



    }

   IEnumerator ScaringNumerator()
    {
        janitoranimator.Play("Jumpscare");
        Instantiate(JumpscareSound, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(.43f);
        JumpscareObject.SetActive(false);
        yield return new WaitForSeconds(2);
        busy = false;
        
    }


    public void DisableJumpscare()
    {
        if(!busy)
        {
            JumpscareObject.SetActive(false);

        }
    }
}
