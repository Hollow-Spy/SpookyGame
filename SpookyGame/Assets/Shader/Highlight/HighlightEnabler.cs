using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightEnabler : MonoBehaviour
{
    [SerializeField] GameObject HighlightObject;
    Renderer render;
   [SerializeField] Transform playerpos;
    [SerializeField] LayerMask masks;
    [SerializeField] Collider TaskCollider;
    bool HighlightsEnabled;
  [SerializeField]  float distanceHighlight=7;

    private void OnEnable()
    {
        if(PlayerPrefs.GetInt("Highlight") == 1)
        {
            HighlightsEnabled = true;
            HighlightObject.SetActive(true);
        }
        else
        {
            HighlightsEnabled = false;

            HighlightObject.SetActive(false);
        }
    }
    private void OnDisable()
    {
        HighlightObject.SetActive(false);

    }

    private void Start()
    {
      
        render = HighlightObject.GetComponent<Renderer>();
       
    }

    private void Update()
    {
     if(HighlightsEnabled)
        {
            RaycastHit hit;
            if (Vector3.Distance(playerpos.position, TaskCollider.transform.position) < distanceHighlight || Physics.Raycast(playerpos.position, Vector3.Normalize(TaskCollider.gameObject.transform.position - playerpos.position), out hit, 10, masks) && hit.collider.gameObject == TaskCollider.gameObject)
            {
                HighlightObject.SetActive(false);
            }
        }
      
      
 
    }

}
