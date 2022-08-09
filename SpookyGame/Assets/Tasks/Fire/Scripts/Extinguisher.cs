using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extinguisher : MonoBehaviour
{

    bool holding;
    PlayerInteract interact;
    
    [SerializeField] Transform ExtinnguishPos;
    [SerializeField] Transform SpawnPos;
    [SerializeField] ParticleSystem Particles;
    [SerializeField] GameObject Fire,FireOutParticle;
    AudioSource soundplayer;
    Vector3 ogPos;
    bool active;
    [SerializeField] LayerMask FireLayer;
    private void Awake()
    {
        soundplayer = GetComponent<AudioSource>();
        Debug.Log("Firs");
        ogPos = ExtinnguishPos.localPosition;
    }

    private void OnEnable()
    {
        active = true;
        Particles.gameObject.SetActive(true);
        Particles.Stop();

       

        ExtinnguishPos.localPosition = ogPos;
        transform.position = SpawnPos.position;
        Fire.SetActive(true);
        Fire.GetComponent<ParticleSystem>().Play();

        transform.SetParent(null);
        GetComponent<CapsuleCollider>().enabled = true;

        Physics.IgnoreCollision(GetComponent<CapsuleCollider>(), GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider>(), true);
        interact = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerInteract>();

        holding = false;
    }




    public void FireGone()
    {
        Instantiate(FireOutParticle, Fire.transform.position, Fire.transform.rotation );
        active = false;
        Fire.GetComponent<ParticleSystem>().Stop();
      

        StartCoroutine(TaskDone(false));
    }

    IEnumerator TaskDone(bool failed)
    {
        for (int i = 0; i < 150; i++)
        {
            yield return null;
            ExtinnguishPos.localPosition = Vector3.Lerp(ExtinnguishPos.localPosition, new Vector3(ExtinnguishPos.localPosition.x, ExtinnguishPos.localPosition.y - .1f, ExtinnguishPos.localPosition.z), 35 * Time.deltaTime);
        }

        while (GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<TaskOrganizer>().busy)
        {
            yield return null;
        }
        GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<TaskOrganizer>().RemoveTask(gameObject, failed);

    }


    private void OnDisable()
    {
        if (holding)
        {

            holding = false;
            
            Fire.SetActive(false);

            interact.active = true;


        }


    }

    public void Update()
    {
        if (holding)
        {
            transform.position = ExtinnguishPos.position;
           


            if (Input.GetMouseButton(0) && active)
            {

                if (!soundplayer.isPlaying)
                {
                    soundplayer.Play();
                }

                if(!Particles.isPlaying)
                {
                    Particles.Play();
                }
              


                RaycastHit hit;

                if (Physics.SphereCast(GameObject.FindGameObjectWithTag("MainCamera").transform.position, 1, GameObject.FindGameObjectWithTag("MainCamera").transform.forward, out hit, 4,FireLayer))
                {
                    if(hit.transform.gameObject == Fire)
                    {
                        Fire.GetComponent<Fire>().LoseHealth();
                    }
                }

            }
            else
            {

                soundplayer.Stop();
                
                Particles.Stop();
            }
          

        }


    }

    public void Interaction()
    {
        if (!holding)
        {
          
            
            transform.SetParent(ExtinnguishPos);
            GetComponent<CapsuleCollider>().enabled = false;
            transform.eulerAngles = new Vector3(0, 200, 0f);
            /*
          transform.position = new Vector3(0.145f, -0.192f, 0.337f);
          transform.eulerAngles = new Vector3(0f, -23.473f, 75.793f);
            */
            interact.active = false;

            holding = true;
            

        }

    }
}


