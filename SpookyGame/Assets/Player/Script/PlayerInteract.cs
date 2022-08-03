using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] float RaycastLengh;
    [SerializeField] LayerMask InteractiveLayer;
    public GameObject normalCrosshair;
    public GameObject handInteractionUI;
   public bool active=true;

    void Update()
    {
        if(active)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, RaycastLengh, InteractiveLayer))
            {
                // change icon
                normalCrosshair.SetActive(false);
                handInteractionUI.SetActive(true);

                if (Input.GetMouseButtonDown(0))
                {
                    hit.transform.gameObject.SendMessage("Interaction");
                }
                
            }
            else
            {
                //change icon
                normalCrosshair.SetActive(true);
                handInteractionUI.SetActive(false);
            }
        }

       
    }
}
