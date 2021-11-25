using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeeTask : MonoBehaviour
{
    [SerializeField] Transform PeePos;
    [SerializeField] GameObject PeeParticles;
    [SerializeField] LayerMask playerlayer;
    Transform playerbody;

    [SerializeField] float fillTime;
    float filled;

    [SerializeField] float shakedelay;
    float shaking;
    bool active;
  [SerializeField]  GameObject unzipSFX;
    AudioSource SoundPlayer;
    [SerializeField] AudioClip[] peesounds;


    private void OnEnable()
    {
        SoundPlayer = PeePos.GetComponent<AudioSource>();
        filled = 0;
        gameObject.layer = 8;
        PeePos.SetParent(GameObject.FindGameObjectWithTag("MainCamera").transform);
        PeePos.transform.localPosition = new Vector3(-0.02f, -0.441f, 2.397f);

     
        PeeParticles.transform.SetParent(GameObject.FindGameObjectWithTag("MainCamera").transform);
        PeeParticles.transform.localPosition = new Vector3(0, -0.63f, 0);
        

        playerbody = GameObject.FindGameObjectWithTag("Player").transform;


    }
    

    private void OnDisable()
    {
       
        if (PeeParticles)
        {
            playerbody.eulerAngles = new Vector3(0, playerbody.eulerAngles.y, 0);
            PeeParticles.GetComponent<ParticleSystem>().enableEmission = false;

            active = false;
            
        }
      

    }

    public void Interaction()
    {
        Instantiate(unzipSFX, transform.position, Quaternion.identity);

        PeeParticles.GetComponent<ParticleSystem>().enableEmission = true;

        PeeParticles.transform.eulerAngles = new Vector3(0, 0, 0);

        gameObject.layer = 0;
        PeeParticles.SetActive(true);
        active = true;

    }

    IEnumerator TaskDone(bool failed)
    {
        while (GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<TaskOrganizer>().busy)
        {
            yield return null;
        }
        GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<TaskOrganizer>().RemoveTask(gameObject, failed);

    }


    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            if (shaking <= 0)
            {
                shaking = shakedelay;
                playerbody.eulerAngles = new Vector3(0 + Random.Range(-.2f, .2f), playerbody.eulerAngles.y + Random.Range(-1.5f, 2f), 0);
            }
            else
            {
                shaking -= Time.deltaTime;
            }


            if (Physics.Raycast(PeePos.position, Vector3.down, 1.5f, playerlayer) && PeePos.position.y > 3.5f)
            {
                Debug.Log(filled);

                filled += Time.deltaTime;

                if(SoundPlayer.clip != peesounds[1])
                {
                    SoundPlayer.clip = peesounds[1];
                }
                if(!SoundPlayer.isPlaying)
                {
                    SoundPlayer.pitch = Random.Range(.8f, 1.1f);
                    SoundPlayer.Play();
                }


                if (filled >= fillTime)
                {
                    PeeParticles.GetComponent<ParticleSystem>().enableEmission = false;
                    Instantiate(unzipSFX, transform.position, Quaternion.identity);
                    SoundPlayer.Stop();
                    active = false;
                    StartCoroutine(TaskDone(false));

                }

            }
            else
            {
                if (SoundPlayer.clip != peesounds[0])
                {
                    SoundPlayer.clip = peesounds[0];
                }
                if (!SoundPlayer.isPlaying)
                {
                    SoundPlayer.pitch = Random.Range(.8f, 1.1f);
                    SoundPlayer.Play();
                }

            }

        }
       
    }
}
