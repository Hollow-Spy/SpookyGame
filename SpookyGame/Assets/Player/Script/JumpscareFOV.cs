using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpscareFOV : MonoBehaviour
{
    // NOT MY SCRIPT, I WISH I WAS THIS GOOD WITH  MATH, SECOND SEMESTER HERE I COME
    //https://www.youtube.com/watch?v=j1-OyLo77ss

    // I WILL SIMPLY DO ADAPTATIONS



    public float radius;
    [Range(0, 360)]
    public float angle;

    public List<GameObject> Refs;
    //public GameObject[] Refs;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;
    

    private void Start()
    {

        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }


    private void ListObjectCheck()
    {

    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y , transform.position.z), radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - new Vector3(transform.position.x, transform.position.y , transform.position.z)).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(new Vector3(transform.position.x, transform.position.y, transform.position.z), target.position);

                if (!Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z), directionToTarget, distanceToTarget, obstructionMask))
                {
                   
                    canSeePlayer = true;
                   
                }
                else
                {
                    canSeePlayer = false;
                }
                  
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }
}
