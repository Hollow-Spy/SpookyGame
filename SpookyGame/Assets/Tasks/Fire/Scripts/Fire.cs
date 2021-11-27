using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] float firehealth;
    float health;
    [SerializeField] GameObject Extinguish;

    private void OnEnable()
    {
        health = firehealth;
    }



    public void LoseHealth()
    {
        health -= Time.deltaTime;
        if(health <= 0)
        {
            health = firehealth;
            Extinguish.GetComponent<Extinguisher>().FireGone();
        }
    }
}
