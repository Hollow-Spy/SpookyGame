using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    bool pickup;
    AudioSource ring;
    [SerializeField] GameObject PickUpSFX;

    void Start()
    {
        StartCoroutine(Phone());
    }
    IEnumerator Phone()
    {
        yield return new WaitForSeconds(1);
        while(!pickup)
        {
            yield return null;
        }



    }

  public void Interact()
    {
        if(!pickup)
        {
            Instantiate(PickUpSFX, transform.position, Quaternion.identity);
            pickup = true;
            ring.Stop();
        }
    }
}
