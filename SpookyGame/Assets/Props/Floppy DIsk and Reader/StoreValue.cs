using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreValue : MonoBehaviour
{
    //This script is designed to store the FloppyDisk once it gets picked up by the player. 
    //the object is destroyed 
    public bool ObjectStored = false;    
    public GameObject floppyDisk;
    public Transform Target;
    public Transform Startpoint;
    public bool canInteract = false;
    public int cap = 1;
    public int current = 0;  
  
    // Start is called before the first frame update  
    void Start()
    {
        

    }
   
    void Update()
    {
        if (ObjectStored == true)//this if statement is used to prevent the object from floppy disk from spawning infinitely.
        {          
                if (canInteract)//if statement
                {
                    SpawnObject();
                }                                                  
        }
    }   
    // Update is called once per frame       
    public void SpawnObject()
    {  
        
            Instantiate(floppyDisk, Startpoint.position, Quaternion.identity);          
            ObjectStored = false;
            canInteract = false;
    }
    public void Interaction()
    {
        canInteract = true;
    }
}
