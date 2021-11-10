using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] float RaycastLengh;
    [SerializeField] LayerMask InteractiveLayer;


    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.forward, out hit , RaycastLengh,InteractiveLayer))
        {
            if(Input.GetMouseButtonDown(0))
            {
                hit.transform.gameObject.SendMessage("Interaction");
            }
        }
    }
}
