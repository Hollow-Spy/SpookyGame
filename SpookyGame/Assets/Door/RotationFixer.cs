using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationFixer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = Vector3.zero;
        GetComponent<Rigidbody>().inertiaTensorRotation = Quaternion.identity;

    }


}
