using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    bool pickup;
    AudioSource ring;
    [SerializeField] GameObject PickUpSFX;
    [SerializeField] GameObject Janitor;
    [SerializeField] Transform CheckPos;

    [SerializeField] GameObject CrouchText;
    [SerializeField] GameObject[] voices;
    GameObject currentvoice;
    AudioSource voiceplayer;
    [SerializeField] GameObject Zor;
    [SerializeField] SceneLoader sceneloader;
    void Start()
    {
        ring = GetComponent<AudioSource>();
        StartCoroutine(Phone());
    }
    IEnumerator Phone()
    {
        ring.Play();
        yield return new WaitForSeconds(1);
        while(!pickup)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Tiredness>().tiredLevel = 0;
            yield return null;
        }

        currentvoice = Instantiate(voices[0], transform.position, Quaternion.identity);
        voiceplayer = currentvoice.GetComponent<AudioSource>();
        while(voiceplayer.isPlaying)
        {
            yield return null;
        }
        Instantiate(PickUpSFX, transform.position, Quaternion.identity);


        //wait
        GameObject.Find("UICanvas").GetComponentInChildren<TaskOrganizer>().AddTask();
        yield return new WaitForSeconds(1);
        while(TaskOrganizer.Score <= 0)
        {
            yield return null;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Tiredness>().tiredLevel = 0;

            if (GameObject.Find("UICanvas").GetComponentInChildren<TaskOrganizer>().ActiveTasks.Count <= 0)
            {
                GameObject.Find("UICanvas").GetComponentInChildren<TaskOrganizer>().AddTask();
                yield return new WaitForSeconds(1);

            }
        }
        pickup = false;
        gameObject.layer = 8;
        ring.Play();

        while(!pickup)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Tiredness>().tiredLevel = 0;

            yield return null;
        }

        currentvoice = Instantiate(voices[1], transform.position, Quaternion.identity);
        voiceplayer = currentvoice.GetComponent<AudioSource>();
        while (voiceplayer.isPlaying)
        {
            yield return null;
        }
        Instantiate(PickUpSFX, transform.position, Quaternion.identity);

        Zor.layer = 8;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Tiredness>().tiredLevel = 50;

        while (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Tiredness>().isTired)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Tiredness>().tiredLevel += 5 * Time.deltaTime;
            yield return null;
        }

        pickup = false;
        gameObject.layer = 8;
        ring.Play();
        while(!pickup)
        {
            yield return null;
        }

        currentvoice = Instantiate(voices[2], transform.position, Quaternion.identity);
        voiceplayer = currentvoice.GetComponent<AudioSource>();
        while (voiceplayer.isPlaying)
        {
            yield return null;
        }
        Instantiate(PickUpSFX, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(1);
        Janitor.SetActive(true);
        yield return new WaitForSeconds(.1f);
        CrouchText.SetActive(true);
        Janitor.GetComponent<JanitorBasic>().Investigate(CheckPos.position);
        while(!Janitor.GetComponent<JanitorBasic>().Wandering || !GameObject.FindGameObjectWithTag("Player").GetComponentInParent<PlayerController>().is_hidden)
        {
            yield return null;
        }
        CrouchText.SetActive(false);
        pickup = false;
        gameObject.layer = 8;
        ring.Play();

        while (!pickup)
        {
            yield return null;
        }


        currentvoice = Instantiate(voices[3], transform.position, Quaternion.identity);
        voiceplayer = currentvoice.GetComponent<AudioSource>();
        while (voiceplayer.isPlaying)
        {
            yield return null;
        }
        Instantiate(PickUpSFX, transform.position, Quaternion.identity);
        sceneloader.LoadScene(2);

    }

  public void Interaction()
    {
        if(!pickup)
        {
            gameObject.layer = 0;

            Instantiate(PickUpSFX, transform.position, Quaternion.identity);
            pickup = true;
            ring.Stop();
        }
    }
}
