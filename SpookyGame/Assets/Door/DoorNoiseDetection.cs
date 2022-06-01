using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorNoiseDetection : MonoBehaviour
{
   [SerializeField] float doorSensitivity;
    [SerializeField] float maxDetectionRange;
    [SerializeField] Transform CheckPos;
    JanitorBasic janitor;
    private void Start()
    {
        janitor = GameObject.FindGameObjectWithTag("Janitor").GetComponent<JanitorBasic>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > doorSensitivity && collision.gameObject.CompareTag("Player") && Vector3.Distance(janitor.transform.position,transform.position) < maxDetectionRange )
        {
            // Instantiate(doorstepsfx, transform.position, Quaternion.identity);
            janitor.Investigate(CheckPos.transform.position);
            
        }
    }
}
