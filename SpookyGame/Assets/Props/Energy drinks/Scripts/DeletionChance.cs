using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletionChance : MonoBehaviour
{
    [SerializeField] float Chance;

    // Start is called before the first frame update
    void Start()
    {
        float rand = Random.Range(0, 100);
        if (rand < Chance)
        {
            Destroy(gameObject);
        }
    }
}
