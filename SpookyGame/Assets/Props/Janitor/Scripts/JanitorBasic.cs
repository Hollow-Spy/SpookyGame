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
    float OGplayerspeed;
    [SerializeField] float BasicSpeed;
    [SerializeField] Transform freezerpos;
    [SerializeField] Transform punchposition;

    public GameObject punchtrigger;

    bool SecondCatch;
    public Camera grabcamera;
     Camera maincamera;
    public GameObject BlackOutScreen;
    
    void Awake()
    {
        maincamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<PlayerController>();

        OGplayerspeed = player.speed;
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
                for(int i = 0;i<20;i++)
                {

                    Vector3 dir = playerpos.position - transform.position;
                    dir.y = 0;//This allows the object to only rotate on its y axis
                    Quaternion rot = Quaternion.LookRotation(dir);
                    transform.rotation = Quaternion.Lerp(transform.rotation, rot, 10 * Time.deltaTime);
                    if(transform.rotation == rot)
                    {
                        i = 21;
                    }
                    
                }
                animator.SetTrigger("punch");
                detection += detectionTime;

            }
          
        }
    }



   public void Punch()
    {
        Instantiate(punchtrigger, punchposition.position, Quaternion.identity);
    }


    private void OnTriggerExit(Collider other)
    {
        animator.ResetTrigger("punch");
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

    public void JanitorGrabed()
    {
        maincamera.tag = "Untagged";
        grabcamera.gameObject.SetActive(true);
        grabcamera.tag = "MainCamera";
        player.gameObject.SetActive(false);
    }

    public void JanitorFreezeThrow()
    {
        grabcamera.GetComponent<Animator>().SetTrigger("throw");
        Instantiate(BlackOutScreen, transform.position, Quaternion.identity);
    }

    public void JanitorGrabFreezer()
    {
        if(SecondCatch)
        {

        }
        else
        {
           
            agent.speed = BasicSpeed * 2;
            agent.SetDestination(freezerpos.position);
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
                if(inRange)
                {
                    agent.speed = 0;

                }
                else
                {
                    agent.speed = BasicSpeed;
                }


            }
          else
            {
                agent.SetDestination(hidingplace);
              
                if(agent.remainingDistance < .1f)
                {

                    detection = 0;
                    if(knowshider)
                    {
                        Debug.Log("not foncfs");
                        player.speed = 0;

                        Vector3 dir = playerpos.position - transform.position;
                        Quaternion rot = Quaternion.LookRotation(dir);
                        while (transform.rotation != rot)
                        {
                            yield return null;
                             dir = playerpos.position - transform.position;
                            dir.y = 0;//This allows the object to only rotate on its y axis
                             rot = Quaternion.LookRotation(dir);
                            transform.rotation = Quaternion.Lerp(transform.rotation, rot, 10 * Time.deltaTime);
                            Debug.Log("help");
                        }
                        

                        animator.SetBool("grabunder",true);
                        yield return new WaitForSeconds(3.1f);
                        if (!SecondCatch)
                        {

                            while (agent.remainingDistance > .1f)
                            {
                                yield return null;
                            }
                            agent.speed = BasicSpeed;
                            SecondCatch = true;

                            animator.SetBool("grabunder", false);

                            yield return new WaitForSeconds(5);
                            transform.position = PatrolPoints[Random.Range(2, 4)].position;
                            agent.SetDestination(PatrolPoints[Random.Range(0, PatrolPoints.Length)].position);
                            yield return new WaitForSeconds(1);
                            player.gameObject.SetActive(true);

                            player.is_crouched = false;
                            player.transform.localScale = new Vector3(1, 1, 1);
                            player.is_hidden = false;

                           playerpos.position = freezerpos.position;
                            grabcamera.tag = "Untagged";
                            grabcamera.gameObject.SetActive(false);
                            maincamera.tag = "MainCamera";
                            player.speed = OGplayerspeed;


                        }
                        else
                        {
                            Debug.Log("die");

                        }

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
