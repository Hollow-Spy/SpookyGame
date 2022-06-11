using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OptionsMenu : MonoBehaviour
{

        int CurrentOption = 0;
        [SerializeField] Text[] menuTexts;
        [SerializeField] bool[] ButtonActive;
        [SerializeField] GameObject[] menuSelectArrow;
        [SerializeField] GameObject PauseMenu,optionsMenu;
        [SerializeField] GameObject AppearSound, DissapearSound,ClickSound;
    [SerializeField] Text[] ZeroOneTexts;
       

    [SerializeField] RectTransform[] BlackBars;
    [SerializeField] AudioSource audioplayer;
    

    float currentAudio;
    float currentBrightness;
    int currentDistortion;
    int currentHighlights;
    

    private void OnEnable()
    {
        CurrentTextSelect(false);
        CurrentOption = 0;
        CurrentTextSelect(true);

        currentBrightness = PlayerPrefs.GetFloat("Brightness");
        currentAudio = PlayerPrefs.GetFloat("Volume");
        currentDistortion = PlayerPrefs.GetInt("Distortion");
        currentHighlights = PlayerPrefs.GetInt("Highlight");



        BlackBars[0].localScale = new Vector3(1 - currentAudio, 1, 1);
        BlackBars[1].localScale = new Vector3(1 - currentBrightness, 1, 1);
        ZeroOneTexts[0].text = currentDistortion.ToString();
    }

    // Update is called once per frame
    void Update()
        {



            if (Input.GetKeyDown(KeyCode.W))
            {
                CurrentTextSelect(false);

                CurrentOption--;
                if (CurrentOption < 0)
                {
                    CurrentOption = menuTexts.Length - 1;

                }
                CurrentTextSelect(true);


            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                CurrentTextSelect(false);
                CurrentOption++;
                if (CurrentOption > menuTexts.Length - 1)
                {
                    CurrentOption = 0;

                }
                CurrentTextSelect(true);

            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ClickButton();
            }


            if(Input.GetKey(KeyCode.A) )
             {
               SliderMove(-1);


             }
            else
           {
             if (Input.GetKey(KeyCode.D))
              {
                SliderMove(1);

               }
              else
               {
                audioplayer.pitch = 1;

                audioplayer.Stop();

                }
           }
          

      


    }


    void SliderMove(float dir)
    {
      
        if (CurrentOption < 2)
        {
            if(!audioplayer.isPlaying)
            {
                audioplayer.Play();
            }
           

            if(currentAudio == 1 || currentAudio == 0)
            {
                audioplayer.pitch = 1;

            }
            else
            {
                audioplayer.pitch += .0001f;
            }
        }


        switch (CurrentOption)
        {
            case 0:
         
                currentAudio += dir / 500;
                

                currentAudio = Mathf.Clamp(currentAudio, 0, 1);
               
                BlackBars[0].localScale = new Vector3(1 - currentAudio, 1, 1);
               

                AudioListener.volume = currentAudio;

               
                break;
            case 1:
                currentBrightness += dir / 500;
                currentBrightness = Mathf.Clamp(currentBrightness, 0, 1);
                BlackBars[1].localScale = new Vector3(1 - currentBrightness, 1, 1);
                RenderSettings.ambientLight = new Color(currentBrightness, currentBrightness, currentBrightness, 1.0f);

                break;


        }
    }

        void ClickButton()
        {
        Instantiate(ClickSound, transform.position, Quaternion.identity);

        switch (CurrentOption)
            {
             
                case 2:
                if (currentDistortion == 0)
                {
                    currentDistortion = 1;
                    RenderSettings.ambientLight = new Color(RenderSettings.ambientLight.r, RenderSettings.ambientLight.g, RenderSettings.ambientLight.b, 1f);
                }
                else
                {
                    currentDistortion = 0;
                    RenderSettings.ambientLight = new Color(RenderSettings.ambientLight.r, RenderSettings.ambientLight.g, RenderSettings.ambientLight.b, .0f);
                }

                ZeroOneTexts[0].text = currentDistortion.ToString();
                break;

                case 3:
            
                break;
            case 4:


                PlayerPrefs.SetFloat("Brightness", currentBrightness);
                PlayerPrefs.SetFloat("Volume", currentAudio);
                PlayerPrefs.SetInt("Distortion", currentDistortion);
                PlayerPrefs.SetInt("Highlight", currentHighlights);

                PauseMenu.SetActive(true);
                optionsMenu.SetActive(false);

                break;

            }
        }

        void CurrentTextSelect(bool state)
        {
            if (state)
            {
                menuTexts[CurrentOption].color = new Color(1, 1, 1, 1);
                menuSelectArrow[CurrentOption].SetActive(true);
            }
            else
            {
                menuTexts[CurrentOption].color = new Color(0.19607843137f, 0.19607843137f, 0.19607843137f, 1);
                menuSelectArrow[CurrentOption].SetActive(false);

            }

        }


    }


