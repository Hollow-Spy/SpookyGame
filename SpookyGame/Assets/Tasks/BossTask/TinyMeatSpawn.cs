using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinyMeatSpawn : MonoBehaviour
{
    [SerializeField] GameObject TinyMeat;
   


    public void Interaction()
    {
        Instantiate(TinyMeat, Input.mousePosition, Quaternion.identity);
    }
}
