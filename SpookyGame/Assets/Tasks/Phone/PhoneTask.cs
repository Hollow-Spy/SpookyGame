using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneTask : MonoBehaviour
{
    [SerializeField] AudioSource phonering;
    [SerializeField] GameObject PhoneCanvas;
    bool PickedUp;
    [SerializeField] GameObject PickUpSound;
    [SerializeField] Text TimeText,PhoneNumberText,DialogueText;
    [TextArea(3, 10)]
    [SerializeField] string[] StartPhoneQuotes,EndPhoneQuotes,GoodEndPhoneQuotes;
    
    [SerializeField] AudioSource[] DialogueAudioStart;
    [SerializeField] AudioSource[] DialogueAudioFinish;

    [SerializeField] AudioSource[] DialogueGoodAudioFinish;
    [SerializeField] GameObject OptionsUI,ARARSound,DialogueUI;
    int currentIndex;
    private void OnEnable()
    {
        PickedUp = false;
        phonering.Play();
    }

    private void OnDisable()
    {
        phonering.Stop();
        if (PickedUp)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponentInParent<PlayerController>().enabled = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().isKinematic = false;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<HeadBop>().enabled = true;
            GameObject.FindGameObjectWithTag("Janitor").GetComponent<JanitorBasic>().blind = false;

            PhoneCanvas.SetActive(false);
            DialogueUI.SetActive(false);
            OptionsUI.SetActive(false);

            DialogueAudioStart[currentIndex].Stop();
        }
    }

    public void Interaction()
    {
        if(!PickedUp && !GameObject.FindGameObjectWithTag("Janitor").GetComponent<JanitorBasic>().Chasing)
        {
            PickedUp = true;
            Instantiate(PickUpSound, transform.position, Quaternion.identity);

            GameObject.FindGameObjectWithTag("Player").GetComponentInParent<PlayerController>().enabled = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().isKinematic = true;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<HeadBop>().enabled = false;
            GameObject.FindGameObjectWithTag("Janitor").GetComponent<JanitorBasic>().blind = true;
            PhoneCanvas.SetActive(true);

            phonering.Stop();

            StartCoroutine(Dialogue(Random.Range(0,3)));
            //StartCoroutine(Dialogue(2));
            StartCoroutine(TimeCount());

           // 
        }
       
    }

    IEnumerator Dialogue(int index)
    {
        DialogueUI.SetActive(true);
      
        currentIndex = index;
        int textFilled=0;
        switch(index)
        {
            case 0:
                //prankster
                PhoneNumberText.text = "555-777";
                textFilled = 6;
                break;
            case 1:
                //pizza
                PhoneNumberText.text = "341-123";
                textFilled = 6;
                break;
            case 2:
                //fan
                PhoneNumberText.text = "444-919";
                textFilled = 5;
                break;
        }
        DialogueAudioStart[index].Play();
        DialogueText.text = DialogueText.text = StartPhoneQuotes[index].Substring(0, textFilled);
        yield return new WaitForSeconds(1.2f);

        while (textFilled < StartPhoneQuotes[index].Length)
        {
          
            DialogueText.text = StartPhoneQuotes[index].Substring(0, textFilled);
            yield return new WaitForSeconds(Random.Range(.1f, .21f));
                textFilled += 4;
        }
        DialogueText.text = StartPhoneQuotes[index];
        DialogueAudioStart[index].Stop();

        OptionsUI.SetActive(true);
        int choice = -1;
        while (choice == -1)
        {
            yield return null;
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                choice = 0;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                choice = 1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                choice = 2;
            }

        }

        OptionsUI.SetActive(false);
        switch (index)
        {
            case 0:
               switch(choice)
                {
                    case 0:
                        DialogueAudioFinish[0].Play();
                        textFilled = 4;
                        while (textFilled < EndPhoneQuotes[index].Length)
                        {

                            DialogueText.text = EndPhoneQuotes[index].Substring(0, textFilled);
                            yield return new WaitForSeconds(Random.Range(.2f, .33f));
                            textFilled += 4;
                        }
                        DialogueText.text = EndPhoneQuotes[index];
                        DialogueAudioFinish[index].Stop();
                        yield return new WaitForSeconds(1f);
                        Instantiate(ARARSound, transform.position, Quaternion.identity);
                        PhoneCanvas.SetActive(false);
                        StartCoroutine(TaskDone(true));
                        break;
                    case 1:
                        DialogueAudioFinish[0].Play();
                        textFilled = 4;
                        while (textFilled < EndPhoneQuotes[index].Length)
                        {

                            DialogueText.text = EndPhoneQuotes[index].Substring(0, textFilled);
                            yield return new WaitForSeconds(Random.Range(.2f, .33f));
                            textFilled += 4;
                        }
                        DialogueText.text = EndPhoneQuotes[index];
                        DialogueAudioFinish[index].Stop();
                        yield return new WaitForSeconds(1f);
                   
                        PhoneCanvas.SetActive(false);
                      
                        Instantiate(ARARSound, transform.position, Quaternion.identity);

                        StartCoroutine(TaskDone(true));
                        break;
                    case 2:
                        //right choice
                        PhoneCanvas.SetActive(false);
                        Instantiate(PickUpSound, transform.position, Quaternion.identity);
                        StartCoroutine(TaskDone(false));
                        break;
                }
                break;
            case 1:
                switch (choice)
                {
                    case 0:
                        DialogueAudioFinish[1].Play();
                        textFilled = 2;
                        while (textFilled < EndPhoneQuotes[index].Length)
                        {

                            DialogueText.text = EndPhoneQuotes[index].Substring(0, textFilled);
                            yield return new WaitForSeconds(Random.Range(.2f, .33f));
                            textFilled += 7;
                        }
                        DialogueText.text = EndPhoneQuotes[index];
                        DialogueAudioFinish[index].Stop();
                        yield return new WaitForSeconds(1f);
                       
                        PhoneCanvas.SetActive(false);
                        StartCoroutine(TaskDone(true));
                        break;
                    case 1:
                        //right choice
                        DialogueGoodAudioFinish[1].Play();
                        textFilled = 3;
                        while (textFilled < GoodEndPhoneQuotes[index].Length)
                        {

                            DialogueText.text = GoodEndPhoneQuotes[index].Substring(0, textFilled);
                            yield return new WaitForSeconds(Random.Range(.2f, .33f));
                            textFilled += 4;
                        }
                        DialogueText.text = GoodEndPhoneQuotes[index];
                        DialogueGoodAudioFinish[index].Stop();
                        yield return new WaitForSeconds(1f);

                        PhoneCanvas.SetActive(false);
                        StartCoroutine(TaskDone(false));
                        break;
                    case 2:
                        
                        PhoneCanvas.SetActive(false);
                        Instantiate(PickUpSound, transform.position, Quaternion.identity);
                        StartCoroutine(TaskDone(true));
                        break;
                }
                break;
            case 2:
                switch (choice)
                {
                    case 0:
                        //right choice
                        DialogueGoodAudioFinish[2].Play();
                        textFilled = 4;
                        while (textFilled < GoodEndPhoneQuotes[index].Length)
                        {

                            DialogueText.text = GoodEndPhoneQuotes[index].Substring(0, textFilled);
                            yield return new WaitForSeconds(Random.Range(.2f, .33f));
                            textFilled += 7;
                        }
                        DialogueText.text = GoodEndPhoneQuotes[index];
                        DialogueGoodAudioFinish[index].Stop();
                        yield return new WaitForSeconds(1f);

                        PhoneCanvas.SetActive(false);
                        StartCoroutine(TaskDone(false));
                        break;
                    case 1:
                       
                        DialogueAudioFinish[2].Play();
                        textFilled = 3;
                        while (textFilled < EndPhoneQuotes[index].Length)
                        {

                            DialogueText.text = EndPhoneQuotes[index].Substring(0, textFilled);
                            yield return new WaitForSeconds(Random.Range(.2f, .33f));
                            textFilled += 4;
                        }
                        DialogueText.text = EndPhoneQuotes[index];
                        DialogueAudioFinish[index].Stop();
                        yield return new WaitForSeconds(1f);

                        PhoneCanvas.SetActive(false);
                        StartCoroutine(TaskDone(true));
                        break;
                    case 2:

                        PhoneCanvas.SetActive(false);
                        Instantiate(PickUpSound, transform.position, Quaternion.identity);
                        StartCoroutine(TaskDone(true));
                        break;
                }
                break;
        }


    }




    IEnumerator TimeCount()
    {
        int time = 0;
        TimeText.text = "00:00";

        while(true)
        {
            time++;
            yield return new WaitForSeconds(1);
            if(time < 10)
            {
                TimeText.text = "00:0" + time;
            }
            else
            {
                TimeText.text = "00:" + time;
            }
        }

    }

    IEnumerator TaskDone(bool failed)
    {
        while (GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<TaskOrganizer>().busy)
        {
            yield return null;
        }
        GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<TaskOrganizer>().RemoveTask(gameObject, failed);

    }
}
