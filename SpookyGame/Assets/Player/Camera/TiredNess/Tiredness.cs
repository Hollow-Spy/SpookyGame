using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Tiredness : MonoBehaviour
{
    [SerializeField] public float tiredLevel = 0;
    public static float visuals;
    public float grabDistance = 5f;
    RaycastHit hit;
    public bool isTired = true;





    void Update()
    {

        VignettePulse.m_Vignette.intensity.value = tiredLevel / 200;

        if (isTired && tiredLevel < 125)
        {
            tiredLevel += Time.deltaTime;
        }
        else if (!isTired && tiredLevel > 0)
        {
            tiredLevel -= Time.deltaTime + 0.5f;
        }
        else
        {
            isTired = true;
        }


        /*
        var fwd = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, fwd, out hit, grabDistance) && hit.transform.tag == "Fuel")
        {
            if (Input.GetMouseButtonDown(0))
            {
                BoxCollider bc = hit.collider as BoxCollider;
                if (bc != null)
                {
                 
                   
                    //MARKUS HERE 
               
                }
            }
        }
        */
    }







}