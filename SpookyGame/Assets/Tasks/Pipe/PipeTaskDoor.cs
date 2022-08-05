using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeTaskDoor : MonoBehaviour
{
    bool Open;
    [SerializeField] Transform DoorRotTarget;
    [SerializeField] GameObject PipeObjs;
    Quaternion InitialRot;
    [SerializeField] GameObject DoorSoundOpen,DoorSoundClose;
    private void Start()
    {
        InitialRot = transform.rotation;
        
    }
    public void Interaction()
    {
        if(!Open)
        {
            Open = true;
            Instantiate(DoorSoundOpen, transform.position, Quaternion.identity);
            gameObject.layer = 0;
            StartCoroutine(OpenDoorNumerator());
        }

    }
    public void CloseDoor()
    {
        Instantiate(DoorSoundClose, transform.position, Quaternion.identity);

        gameObject.layer = 0;
        Open = false;
        StopAllCoroutines();
        StartCoroutine(CloseDoorNumerator());
    }

    IEnumerator CloseDoorNumerator()
    {
        while (transform.rotation.normalized != InitialRot.normalized)
        {
            yield return null;
            transform.rotation = Quaternion.Slerp(transform.rotation, InitialRot, 5 * Time.deltaTime);
        }

        PipeObjs.SetActive(false);
    }


    IEnumerator OpenDoorNumerator()
    {
        while(transform.rotation.normalized != DoorRotTarget.transform.rotation.normalized)
        {
            yield return null;
            transform.rotation = Quaternion.Slerp(transform.rotation, DoorRotTarget.rotation, 5 * Time.deltaTime);
        }
    }
}
