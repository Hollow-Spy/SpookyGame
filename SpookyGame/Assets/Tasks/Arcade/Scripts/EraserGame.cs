using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class EraserGame : MonoBehaviour
{
    int meterLapsed = 0;
    int Increment;
    [SerializeField] GameObject StartSequence;
    [SerializeField] Animator RoadAnimator;
    [SerializeField] EraserPlayer Player;
    [SerializeField] Text meterCounter;
    bool counting,spawning;
    [SerializeField] AudioSource music;

    [SerializeField] float maxtimeOG;
    [SerializeField] float mintime;
    float maxtime;
    [SerializeField] float difficultyIncrement;
    [SerializeField] Transform[] pos;
    [SerializeField] GameObject CarPrefab,LoseScreen;
    [SerializeField] GameObject[] Warnings;
    [SerializeField] Canvas thecanvas;
 
    private void OnEnable()
    {
        spawning = true;
        maxtime = maxtimeOG;
        Increment = 0;
        music.Stop();
        music.pitch = 1;
        meterCounter.text = "0";
        meterLapsed = 0;
        StartSequence.SetActive(true);
        counting = true;
        StartCoroutine(CountingMeters());
        StartCoroutine(Obstacles());
        StartCoroutine(SpawnStop());

    }

    public void StopGame()
    {
        if(counting)
        {
            music.Stop();
            RoadAnimator.enabled = false;
            LoseScreen.SetActive(true);
            counting = false;
            StopAllCoroutines();
            StartCoroutine(waiting());
        }
   
    }
    IEnumerator waiting()
    {
        yield return new WaitForSeconds(2);

    }

    IEnumerator SpawnStop()
    {
        yield return new WaitForSeconds(34);
        spawning = false;
    }

    IEnumerator Obstacles()
    {
        yield return new WaitForSeconds(2.5f);
        while(spawning)
        {
            yield return new WaitForSeconds(Random.Range( mintime, maxtime));
            int random = Random.Range(0, 3);
            Warnings[random].SetActive(true);
            yield return new WaitForSeconds(1f);
            Warnings[random].SetActive(false);
            GameObject fab= Instantiate(CarPrefab, pos[random].position, Quaternion.identity);
            fab.transform.parent = thecanvas.transform;
            float scaler = meterLapsed;
            scaler = scaler / 1000;
            fab.GetComponent<Rigidbody2D>().gravityScale *= scaler;
            maxtime -= difficultyIncrement;
        }
    }

    IEnumerator CountingMeters()
    {
        yield return new WaitForSeconds(2.5f);
        Player.enabled = true;
        RoadAnimator.enabled = true;
        RoadAnimator.SetFloat("Speed", 1);
        StartSequence.SetActive(false);
        music.Play();

        while (counting)
        {
            yield return null;
            meterLapsed+=2 + Increment;
            Increment = meterLapsed / 500;
            meterCounter.text = meterLapsed.ToString();

            float pitche = meterLapsed;
            pitche = pitche / 2000;
            RoadAnimator.SetFloat("Speed", .5f+ pitche);

            music.pitch = 1 + pitche;

           
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
