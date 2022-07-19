using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JanitorHallucination : MonoBehaviour
{
  [SerializeField]  LayerMask masks;
  [SerializeField]  Transform cameratransform;
    [SerializeField] float radius;
    
 
    private void Update()
    {
       
        RaycastHit hit;
        if(Physics.SphereCast(cameratransform.position,radius,cameratransform.forward,out hit, 10,masks) && hit.collider.gameObject == gameObject)
        {
            Debug.Log("Hit");
        }
    }
}
