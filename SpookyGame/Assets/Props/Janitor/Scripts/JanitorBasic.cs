using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JanitorBasic : MonoBehaviour
{
    // Start is called before the first frame update
    NavMeshAgent agent;
    public Transform playerpos;
    public bool Wandering;
    public bool Chasing;
    public float detection=0;
    JanitorFOV fov;
    public float detectionTime;
    public float ExtraChaseTime;
    Vector3 hidingplace;
    bool inRange;
    PlayerController player;
    bool knowshider;
    private IEnumerator Chasecoroutine,Wandercoroutine;
    [SerializeField] Transform[] PatrolPoints;
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<PlayerController>();
        playerpos = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        fov = GetComponent<JanitorFOV>();
    }
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
          if(!player.is_hidden)
            {

            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;

        }
    }

    public void PlayerHide(Vector3 pos)
    {
        hidingplace = pos;
        detection += 7;
        if (fov.canSeePlayer || inRange)
        {
            knowshider = true;

        }
        else
        {

            knowshider = false;
        }

    }

    IEnumerator WanderingNumerator()
    {
        while(Wandering)
        {
            Vector3 randompatrol = PatrolPoints[Random.Range(0, PatrolPoints.Length)].position;
            agent.SetDestination(randompatrol);
            yield return new WaitForSeconds(.3f);
            if (agent.remainingDistance < 2f)
            {
                 randompatrol = PatrolPoints[Random.Range(0, PatrolPoints.Length)].position;
                agent.SetDestination(randompatrol);
            }
            yield return new WaitForSeconds(.3f);

            while (agent.remainingDistance > .1f)
            {
                
                yield return null;
            }
           
            animator.SetBool("sad", true);
            int rand = Random.Range(0, 6);
            yield return new WaitForSeconds(3 + rand);
            animator.SetBool("sad", false);
           
        }

    }


    IEnumerator ChaseNumerator()
    {
        detection += ExtraChaseTime;
        animator.SetBool("sad", false);
        if(Wandercoroutine != null)
        {
            StopCoroutine(Wandercoroutine);
        }


        knowshider = false;
        Wandering = false;

        while (detection > 0)
        {
            yield return null;
          if(!player.is_hidden)
            {
                agent.SetDestination(playerpos.position);

            }
          else
            {
                agent.SetDestination(hidingplace);
              
                if(agent.remainingDistance < .1f)
                {

                    detection = 0;
                    if(knowshider)
                    {
                        Debug.Log("not confused");
                    }
                    else
                    {
                        animator.SetTrigger("confused");
                    }

                    yield return new WaitForSeconds(4);

                }
                   
            }

          


        }
        Chasing = false;
      
    }



    // Update is called once per frame
    void Update()
    {
   
      if(fov.canSeePlayer)
       {
            detection += Time.deltaTime;
            if(detection >= detectionTime && !Chasing)
            {
                Chasecoroutine = ChaseNumerator();
                StartCoroutine(Chasecoroutine);
                Chasing = true;
            }
        }
        else
        {
            if (detection > 0 )
            { 
            detection -= Time.deltaTime;
            }

        }
        
        if(detection <= 0 && !Wandering && !Chasing)
        {
            Wandering = true;
            Wandercoroutine = WanderingNumerator();
            StartCoroutine(Wandercoroutine);

        }

        
    }
}
