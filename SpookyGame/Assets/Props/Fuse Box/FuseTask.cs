using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FuseTask : MonoBehaviour
{
    public bool active = true;
    public GameObject fuse;
    public GameObject handle;
    public GameObject lightHandler;
    public GameObject Fake;
    private Lights Lit;
    AudioSource audioplayer;
   [SerializeField] AudioClip[] hitsounds;




    void Update()
    {
        lightHandler = GameObject.Find("RotationPoint");
        Lit = lightHandler.GetComponent<Lights>();
        if(active)
        {
            foreach(GameObject obj in Lit.Lightz)
          {      
            Fake.SetActive(true);
            obj.SetActive(true);    
          }  
        }
        if(!active)
        {
          foreach(GameObject obj in Lit.Lightz)
          {      
            Fake.SetActive(false);
            obj.SetActive(false);      
          }           
        }
    }
    private void OnEnable()
    {       
        rotate();
        active = false;  
    }
    public void Interaction()    //as long as we are active we can flick the fuse box
    {
        Debug.Log(active);
        if(active == false)
        {
           
            rotate();
            StartCoroutine(TaskDone(false));    
            active = true;     
            audioplayer.clip = hitsounds[Random.Range(0, hitsounds.Length)];
            audioplayer.Play();
               
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
    
    void rotate()
    {
        transform.Rotate(0, 0 , -110);

    }
}
