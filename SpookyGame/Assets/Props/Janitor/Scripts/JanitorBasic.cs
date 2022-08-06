using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

[System.Serializable]
public class Voice
{
    [SerializeField] public int priority;
  [SerializeField]  public AudioClip[] Lines;
}


[System.Serializable]
public class Voices
 {
    [SerializeField] public Voice Idle;
    [SerializeField] public Voice Chase;
    [SerializeField] public Voice Security;
    [SerializeField] public Voice Death;
    [SerializeField] public Voice Choco;
    [SerializeField] public Voice Throw;
    [SerializeField] public Voice Punch;
    [SerializeField] public Voice Confused;
    [SerializeField] public Voice Found;
    [SerializeField] public Voice Toilet;

}

public class JanitorBasic : MonoBehaviour
{
    // Start is called before the first frame update

   [SerializeField] public Voices voices;
    NavMeshAgent agent;
    public Transform playerpos;
    public bool Wandering;
    public bool Chasing;
    public bool Investigating;

    public bool isEating;

    public float detection=0;
    JanitorFOV fov;
    public float detectionTime;
    public float ExtraChaseTime;
    Vector3 hidingplace;
    bool inRange;
    public bool blind;
    PlayerController player;
    bool knowshider;
    private IEnumerator Chasecoroutine,Wandercoroutine,ChaseFOVCoroutine;
    [SerializeField] Transform[] PatrolPoints;
    Animator animator;
    float OGplayerspeed;
    [SerializeField] float BasicSpeed;
    [SerializeField] Transform freezerpos;
    [SerializeField] Transform punchposition;
    [SerializeField] float maxDetection;
    public GameObject punchtrigger;
    float cooldown = 1;
    public float HurtTimeMultiplier;
   public bool SecondCatch;
    public Camera grabcamera;
     Camera maincamera;
    public GameObject BlackOutScreen,punchblackout;
    public GameObject LoseScreen;
    
    public PostProcessVolume hurteffect;
    IEnumerator grabcoroutine;

    float contactTime;

    [SerializeField] bool RandomizeSpawn;
   public  bool inCutscene;
    public AudioSource detectionSound, chasesong, walkSound;
    [SerializeField] VHSPostProcessEffect GlitchEffect;
    [SerializeField] GlitchSound glitchsound;
    [SerializeField] GameObject swingSFX,PunchSFX;
    [SerializeField] GameObject ChocoParticles;
    [SerializeField] AudioSource CurrentVoice;
    [SerializeField] GameObject StompSFX;
    [SerializeField] GameObject SmallBlackout;
    int LastVoicePriority;
    void Awake()
    {
        maincamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<PlayerController>();

        OGplayerspeed = player.speed;
      playerpos = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        fov = GetComponent<JanitorFOV>();

        if(RandomizeSpawn)
        {
            agent.Warp(PatrolPoints[Random.Range(0, PatrolPoints.Length)].position); 
        }
    }
    
    public void SayVoice(string spot)
    {
      switch(spot)
        {

            case "Idle":
                if (!CurrentVoice.isPlaying || voices.Idle.priority < LastVoicePriority)
                {
                    CurrentVoice.clip = voices.Idle.Lines[Random.Range(0, voices.Idle.Lines.Length)];
                    CurrentVoice.Play();
                    LastVoicePriority = voices.Idle.priority;
                }
                break;
            case "Chase":
                if (!CurrentVoice.isPlaying || voices.Chase.priority < LastVoicePriority)
               {
                    CurrentVoice.clip = voices.Chase.Lines[Random.Range(0, voices.Chase.Lines.Length)];
                    CurrentVoice.Play();
                    LastVoicePriority = voices.Chase.priority;
               }
                break;
            case "Security":
                if (!CurrentVoice.isPlaying || voices.Security.priority < LastVoicePriority)
                {
                    CurrentVoice.clip = voices.Security.Lines[Random.Range(0, voices.Security.Lines.Length)];
                    CurrentVoice.Play();
                    LastVoicePriority = voices.Security.priority;
                }
                break;
            case "Death":
                if (!CurrentVoice.isPlaying || voices.Death.priority < LastVoicePriority)
                {
                    CurrentVoice.clip = voices.Death.Lines[Random.Range(0, voices.Death.Lines.Length)];
                    CurrentVoice.Play();
                    LastVoicePriority = voices.Death.priority;
                }
                break;
            case "Choco":
                if (!CurrentVoice.isPlaying || voices.Choco.priority < LastVoicePriority)
                {
                    CurrentVoice.clip = voices.Choco.Lines[Random.Range(0, voices.Choco.Lines.Length)] ;
                    CurrentVoice.Play();
                    LastVoicePriority = voices.Choco.priority;
                }
                break;
            case "Throw":
                if (!CurrentVoice.isPlaying || voices.Throw.priority < LastVoicePriority)
                {
                    CurrentVoice.clip = voices.Throw.Lines[Random.Range(0, voices.Throw.Lines.Length)];
                    CurrentVoice.Play();
                    LastVoicePriority = voices.Throw.priority;
                }
                break;
            case "Punch":
                if (!CurrentVoice.isPlaying || voices.Punch.priority < LastVoicePriority)
                {
                    CurrentVoice.clip = voices.Punch.Lines[Random.Range(0, voices.Punch.Lines.Length)];
                    CurrentVoice.Play();
                    LastVoicePriority = voices.Punch.priority;
                }
                break;
            case "Confused":
                if (!CurrentVoice.isPlaying || voices.Confused.priority < LastVoicePriority)
                {
                    CurrentVoice.clip = voices.Confused.Lines[Random.Range(0, voices.Confused.Lines.Length)];
                    CurrentVoice.Play();
                    LastVoicePriority = voices.Confused.priority;
                }
                break;
            case "Found":
                if (!CurrentVoice.isPlaying || voices.Found.priority < LastVoicePriority)
                {
                    CurrentVoice.clip = voices.Found.Lines[Random.Range(0, voices.Found.Lines.Length)];
                    CurrentVoice.Play();
                    LastVoicePriority = voices.Found.priority;
                }
                break;
            case "Toilet":
                if (!CurrentVoice.isPlaying || voices.Toilet.priority < LastVoicePriority)
                {
                    CurrentVoice.clip = voices.Toilet.Lines[Random.Range(0, voices.Toilet.Lines.Length)];
                    CurrentVoice.Play();
                    LastVoicePriority = voices.Toilet.priority;
                }
                break;

    }
     

    }

    public void PlayerHurt()
    {

        Instantiate(PunchSFX, transform.position, Quaternion.identity);
        maincamera.GetComponent<CameraShake>().ShakeScreen(0.24f, 0.021f, 0.35f);
        if(hurteffect.weight == 0)
        {
            
            cooldown = 3f;
            Physics.IgnoreCollision(GetComponent<BoxCollider>(), GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider>(), true);
            player.enabled = false;
            playerpos.GetComponent<Rigidbody>().AddForce(transform.forward * 10,ForceMode.Impulse);
            hurteffect.weight = 1;
              maincamera.GetComponent<Tiredness>().isTired = false;
            glitchsound.StartSound();
            GlitchEffect.enabled = true;
            IEnumerator reducehurtcoroutine;
            reducehurtcoroutine = ReduceHurtNumerator();
            StartCoroutine(reducehurtcoroutine);
        }
        else
        {
            maincamera.GetComponent<PlayerInteract>().active = false;
            agent.speed = BasicSpeed * 4;
            Instantiate(punchblackout, transform.position, Quaternion.identity);
            grabcoroutine = GrabCutscene();
            StartCoroutine(grabcoroutine);
        }

    }
    IEnumerator ReduceHurtNumerator()
    {
        yield return new WaitForSeconds(1);
        player.enabled = true;
        SayVoice("Punch");

        while (hurteffect.weight > 0)
        {
            hurteffect.weight -= Time.deltaTime * .25f * HurtTimeMultiplier;
            yield return new WaitForSeconds(.01f);
        }
        if(maincamera.gameObject.activeInHierarchy)
        {
            glitchsound.StopSound();
            GlitchEffect.enabled = false;
            hurteffect.weight = 0;
        }
      
        yield return null;

    }

    public void ChaseSlip()
    {
        if(Chasing)
        {
            StartCoroutine(ChaseSlipNumerator());
            

        }
    }
    
    IEnumerator ChaseSlipNumerator()
    {
        agent.isStopped = true;
        animator.SetBool("sad", true);

        CurrentVoice.clip = voices.Idle.Lines[8];
        CurrentVoice.Play();
        LastVoicePriority = voices.Idle.priority;

        yield return new WaitForSeconds(1.5f);
        animator.SetBool("sad", false);
        agent.isStopped = false;


    }

    IEnumerator GrabCutscene()
    {

        player.speed = 0;
        inCutscene = true;
        StopCoroutine(Chasecoroutine);
        detection = 0;
        agent.speed = BasicSpeed * 2;

        Vector3 dir = playerpos.position - transform.position;
        Quaternion rot = Quaternion.LookRotation(dir);
        for (int i = 0; i < 30; i++)
        {
            yield return null;
            dir = playerpos.position - transform.position;
            dir.y = 0;//This allows the object to only rotate on its y axis
            rot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, 10 * Time.deltaTime);
           
        }
        while (chasesong.volume > 0)
        {
            yield return null;
            chasesong.volume -= Time.deltaTime;
        }

        if(!player.hiddendesk)
        {
            if(player.currentlocker)
            {
                player.currentlocker.Interaction();

            }
            animator.SetBool("grablock", true);

        }
        else
        {
            animator.SetBool("grabunder", true);

        }

        yield return new WaitForSeconds(3.1f);
        if (!SecondCatch)
        {
            if (!walkSound.isPlaying)
            {
                walkSound.Play();
            }
            while (agent.remainingDistance > .1f)
            {
                
                yield return null;
            }
            agent.speed = BasicSpeed;
            SecondCatch = true;

            if (walkSound.isPlaying)
            {
                walkSound.Stop();
            }

            if (!player.hiddendesk)
            {
                animator.SetBool("grablock", false);

            }
            else
            {
                animator.SetBool("grabunder", false);

            }

            yield return new WaitForSeconds(5);
            agent.Warp(PatrolPoints[Random.Range(2, 4)].position);

            agent.SetDestination(PatrolPoints[Random.Range(0, PatrolPoints.Length)].position);
            
            Chasing = false;
            inRange = false;
            inCutscene = false;

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
            maincamera.fieldOfView = 60;
            maincamera.GetComponent<PlayerInteract>().active = true;
            glitchsound.StopSound();
            GlitchEffect.enabled = false;
            hurteffect.weight = 0;
            
            blind = true;
            yield return new WaitForSeconds(2);
            blind = false;


        }
  

    }





    private void OnTriggerStay(Collider other)
    {
        if(!isEating && !inCutscene)
        {
            if (other.CompareTag("Player") )
            {
                inRange = true;
                if (!player.is_hidden && !inCutscene && cooldown <= 0)
                {
                    cooldown = 1;
                    for (int i = 0; i < 20; i++)
                    {

                        Vector3 dir = playerpos.position - transform.position;
                        dir.y = 0;//This allows the object to only rotate on its y axis
                        Quaternion rot = Quaternion.LookRotation(dir);
                        transform.rotation = Quaternion.Lerp(transform.rotation, rot, 30 * Time.deltaTime);
                        if (transform.rotation == rot)
                        {
                            i = 21;
                        }

                    }
                    animator.SetTrigger("punch");
                    Instantiate(swingSFX, transform.position, Quaternion.identity);
                    detection += detectionTime + 2;

                }

            }

            if (other.gameObject.name == "Zork" )
            {
                isEating = true;
                if(Wandering)
                {
                    Wandering = false;

                }
                if(Chasing)
                {
                    detection = 0;
                }
                if(Investigating)
                {
                    Investigating = false;
                }
                if(walkSound.isPlaying)
                {
                    walkSound.Stop();
                }


                agent.isStopped = true;

                other.SendMessage("Ate");
                ChocoParticles.SetActive(true);
                animator.SetTrigger("eat");
                SayVoice("Choco");

                IEnumerator EatCoroutine = EatingNumerator();
                StartCoroutine(EatCoroutine);
            }
        }
   
    }
    IEnumerator EatingNumerator()
    {
       

        yield return new WaitForSeconds(7);
        
        agent.isStopped = false;
        isEating = false;
        yield return new WaitForSeconds(2);
        ChocoParticles.SetActive(false);

    }



    public void Punch()
    {
       
        Instantiate(punchtrigger, punchposition.position, Quaternion.identity);
    }


    private void OnTriggerExit(Collider other)
    {
       // animator.ResetTrigger("punch");
        if (other.CompareTag("Player"))
        {
            inRange = false;

        }
    }

    public void PlayerHide(Vector3 pos)
    {
        hidingplace = pos;
      
       
        if (fov.canSeePlayer || inRange || contactTime >= 0)
        {
            knowshider = true;

        }
        else
        {

            knowshider = false;
        }

        if (Chasing)
        {
            detection += 7;
        }
    }

    IEnumerator WanderingNumerator()
    {
        animator.SetFloat("walkspeed", 1);
        agent.speed = BasicSpeed;

        while (chasesong.volume > 0)
        {
            yield return null;
            chasesong.volume -= Time.deltaTime;
        }

        while (Wandering)
        {
            Vector3 randompatrol = PatrolPoints[Random.Range(0, PatrolPoints.Length)].position;
            agent.SetDestination(randompatrol);

            float randomvoice = Random.Range(0, 100);
            if(randomvoice < 50)
            {
                SayVoice("Idle");
            }

            yield return new WaitForSeconds(.3f);
            if (agent.remainingDistance < 2f)
            {
                 randompatrol = PatrolPoints[Random.Range(0, PatrolPoints.Length)].position;
                agent.SetDestination(randompatrol);
            }
            yield return new WaitForSeconds(.3f);

            while (agent.remainingDistance > .1f)
            {
                if(!walkSound.isPlaying)
                {
                    walkSound.pitch = Random.Range(.8f, 1.1f);
                    walkSound.Play();
                }


                yield return null;
            }
           
                walkSound.Stop();
                
                
              
                animator.SetBool("sad", true);
                int rand = Random.Range(0, 6);
                yield return new WaitForSeconds(3);
             
           
                SayVoice("Idle");
            
            yield return new WaitForSeconds(rand);
            animator.SetBool("sad", false);
            
           
           
        }

    }

    public void JanitorGrabed()
    {
        SayVoice("Found");
        maincamera.tag = "Untagged";
        grabcamera.gameObject.SetActive(true);
        grabcamera.tag = "MainCamera";
        player.gameObject.SetActive(false);
        if(!player.hiddendesk)
        {
            grabcamera.GetComponent<Animator>().SetTrigger("locker");
        }
      

    }

    public void JanitorFreezeThrow()
    {
    
            grabcamera.GetComponent<Animator>().SetTrigger("throw");
        SayVoice("Throw");

        Instantiate(BlackOutScreen, transform.position, Quaternion.identity);
    }


   

    IEnumerator KillingPlayer()
    {
        
        yield return null;
        int randomkill = Random.Range(0, 2);

        switch(randomkill)
        {
            case 0:
                SayVoice("Death");
                animator.Play("StompKill");
                grabcamera.GetComponent<Animator>().Play("StompCam");  
                break;
            case 1:
                yield return new WaitForSeconds(2);
                Instantiate(SmallBlackout, transform.position, Quaternion.identity);
                animator.Play("PunchKill");
                grabcamera.GetComponent<Animator>().Play("PunchKillCam");
                break;

        }

    }
    ///                          ///
    /// ANIMATION HELPERS &      ///
    ///     SOUNDS              ///
    public void PunchHitSFXFunc()
    {
        Instantiate(PunchSFX, transform.position, Quaternion.identity);
    }
    public void SwingSFXFunc()
    {
        Instantiate(swingSFX, transform.position, Quaternion.identity);
    }
    public void StompSound()
    {
        Instantiate(StompSFX, transform.position, Quaternion.identity);
    }

    public void LoseScreenFunc(string message)
    {
        GameObject screen = Instantiate(LoseScreen, transform.position, Quaternion.identity);
        screen.GetComponentInChildren<Text>().text = message;
    }


    public void JanitorGrabFreezer()
    {
        if(SecondCatch)
        {

            StartCoroutine(KillingPlayer());

        }
        else
        {
           
            agent.speed = BasicSpeed * 2;
            agent.SetDestination(freezerpos.position);
        }
      
    }

    public void Investigate(Vector3 pos)
    {
        if((Wandering && !inCutscene && !isEating) || Investigating)
        {
            Wandering = false;
            StopAllCoroutines();

            Investigating = true;
            animator.SetBool("sad", false);
            animator.SetFloat("walkspeed", 1.2f);


            agent.SetDestination(pos);

            IEnumerator InvestigationCoroutine = InvestigationNumerator();
            StartCoroutine(InvestigationCoroutine);
            

        }
    }


    IEnumerator InvestigationNumerator()
    {
        yield return new WaitForSeconds(.1f);
        animator.SetFloat("walkspeed", 1);

        while (agent.remainingDistance > .1f && Investigating)
        {
            yield return null;
        }
        if(Investigating)
        {
            animator.SetTrigger("confused");

            yield return new WaitForSeconds(4);

            SayVoice("Confused");

            Investigating = false;
        }
    }

    IEnumerator ChaseFOVNumerator()
    {
        yield return new WaitForSeconds(.2f);
        while(maincamera.fieldOfView > 40 && Chasing)
        {
            yield return null;
            maincamera.fieldOfView -= 12 * Time.deltaTime;
        }
        maincamera.fieldOfView = 40;
    }

    IEnumerator ChaseNumerator()
    {
      /*  if(ChaseFOVCoroutine == null)
        {
            ChaseFOVCoroutine = ChaseFOVNumerator();    
        }*/
        StartCoroutine(ChaseFOVNumerator());
       

        SayVoice("Chase");
        detectionSound.Stop();
        chasesong.Play();

      
        chasesong.volume = 1;
        detectionSound.volume = 0;

        Investigating = false;

        walkSound.Stop();

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
                    agent.speed = 1f;

                }
                else
                {
                    animator.SetFloat("walkspeed", 2);
                    agent.speed = BasicSpeed * 2;
                }


            }
          else
            {
                agent.SetDestination(hidingplace);
              
                if(agent.remainingDistance < .1f)
                {
                    animator.SetFloat("walkspeed", 1);

                    detection = 0;
                    if(knowshider)
                    {
                        grabcoroutine = GrabCutscene();
                        StartCoroutine(grabcoroutine);


                        yield return new WaitForSeconds(.1f);
                      while(inCutscene)
                        {
                            yield return null;
                        }

                    }
                    else
                    {
                        animator.SetTrigger("confused");
                    }

                    yield return new WaitForSeconds(4);
                    SayVoice("Confused");
                }
                   
            }

          


        }
        

        Chasing = false;
        while(chasesong.volume > 0)
        {
            yield return null;
            chasesong.volume -= Time.deltaTime;
        }
        while(maincamera.fieldOfView < 60)
        {
            yield return null;
            maincamera.fieldOfView += 8 * Time.deltaTime;
        }
        maincamera.fieldOfView = 60;
      
    }



    // Update is called once per frame
    void Update()
    {
   

     

        if(cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else
        {
            if (GameObject.FindGameObjectWithTag("Player") )
            {
                Physics.IgnoreCollision(GetComponent<BoxCollider>(), GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider>(), false);

            }

            cooldown = 0;
        }

    

        if(detection > maxDetection)
        {
            detection = maxDetection;
        }



      if(fov.canSeePlayer && !isEating && !blind)
       {
            detection += Time.deltaTime ;
            detection += .05f - Mathf.Clamp(Vector3.Distance(playerpos.position, transform.position),0,4) * .01f;

            contactTime += Time.deltaTime;
            if(contactTime > 2)
            {
                contactTime = 2;
            }
            
            

            if(!Chasing)
            {
                if(!detectionSound.isPlaying)
                {
                    detectionSound.Play();

                }
                detectionSound.volume += Time.deltaTime * .5f; 
            }

            if(detection >= detectionTime && !Chasing && !inCutscene)
            {
                Chasecoroutine = ChaseNumerator();
                StartCoroutine(Chasecoroutine);
                Chasing = true;
            }
        }
        else
        {
            if (contactTime > 0)
            {
                contactTime -= Time.deltaTime;
            }
          
           

            if (detection > 0 )
            {
                detectionSound.volume -= Time.deltaTime * .5f;
                detection -= Time.deltaTime;
            }

        }
        
        if(detection <= 0 && !Wandering && !Chasing && !inCutscene && !Investigating && !isEating)
        {
           
            Wandering = true;
            Wandercoroutine = WanderingNumerator();
            StartCoroutine(Wandercoroutine);

        }

        
    }
}
