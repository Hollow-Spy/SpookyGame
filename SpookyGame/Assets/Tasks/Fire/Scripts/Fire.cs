using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] float firehealth;
    float health;
    private void OnEnable()
    {
        health = firehealth;
    }

    public void LoseHealth()
    {
        health -= Time.deltaTime;
        if(health <= 0)
        {

        }
    }
}
