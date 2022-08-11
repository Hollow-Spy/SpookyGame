using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorJumpscare : MonoBehaviour
{
    [SerializeField] GameObject JumpscareObject;
    bool busy;
  [SerializeField]  Renderer JanitorRenderer;

    [SerializeField] Animator janitoranimator;
    [SerializeField] GameObject JumpscareSound;

    [SerializeField] Collider visionCollider;
  
    [SerializeField] JumpscareFOV fov;
    public void EnableJumpscare()
    {
        if(!busy)
        {
            janitoranimator.Play("Idle");
   
            JumpscareObject.SetActive(true);

        }
    }



    private void Update()
    {
        if (JumpscareObject.activeSelf && fov.Refs.Count > 0)
        {
            if (fov.CheckVision(visionCollider.transform) && !busy)
            {
                busy = true;
                StartCoroutine(ScaringNumerator());
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
