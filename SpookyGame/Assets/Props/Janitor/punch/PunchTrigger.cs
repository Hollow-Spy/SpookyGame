using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<HeadBop>().Shake(.23f, .013f, 0.05f);
            GameObject.FindGameObjectWithTag("Janitor").GetComponent<JanitorBasic>().PlayerHurt();
        }
    }
}