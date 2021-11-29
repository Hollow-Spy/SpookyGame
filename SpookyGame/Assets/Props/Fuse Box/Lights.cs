using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
         public GameObject[] Lightz;

         void Start()
         {
             Lightz = GameObject.FindGameObjectsWithTag("Lights");
         }



}
