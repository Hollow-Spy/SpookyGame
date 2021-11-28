using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class points : MonoBehaviour
{
    //script used to store information on endPoint's position in the scene.
    public static Transform point;
    public Transform endPoint;
    // Start is called before the first frame update
    void Awake()
    {
        point = endPoint;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
