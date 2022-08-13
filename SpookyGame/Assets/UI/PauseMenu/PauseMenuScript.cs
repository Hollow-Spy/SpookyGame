using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    int CurrentOption=0;
    [SerializeField] Text[] menuTexts;
    [SerializeField] bool[] ButtonActive;
    [SerializeField] GameObject[] menuSelectArrow;
    [SerializeField] GameObject WholeMenu,OptionsMenu,SmallPauseMenu;
    [SerializeField] GameObject AppearSound, DissapearSound,ClickSound;


  [SerializeField]  PauseMenuActivator activator;
    private void OnEnable()
    {
       
     
       
        Instantiate(AppearSound, transform.position, Quaternion.identity);
        Time.timeScale = 0;
    }
    // Update is called once per frame
    void Update()
    {
        


        if(Input.GetKeyDown(KeyCode.W))
        {
            CurrentTextSelect(false);

            CurrentOption--;
            if(CurrentOption < 0)
            {
                CurrentOption = menuTexts.Length-1;

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

        if(Input.GetKeyDown(KeyCode.Space))
        {
            ClickButton();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            CurrentOption = 0;
            ClickButton();
        }

    }

    void ClickButton()
    {
        Instantiate(ClickSound, transform.position, Quaternion.identity);

        switch (CurrentOption)
        {
            case 0:
                Time.timeScale = 1;
                WholeMenu.SetActive(false);
                Instantiate(DissapearSound, transform.position, Quaternion.identity);
                if (FindObjectOfType<PlayerController>() && !activator.playerWasDisabled)
                {
                    FindObjectOfType<PlayerController>().enabled = true;
                }

                break;

            case 1:
                OptionsMenu.SetActive(true);
                SmallPauseMenu.SetActive(false);

                break;

            case 2:
                if(ButtonActive[CurrentOption])
                {
                    Time.timeScale = 1;
                    FindObjectOfType<SceneLoader>().LoadScene(2);
                }
                break;

            case 3:
                Time.timeScale = 1;
                FindObjectOfType<SceneLoader>().LoadScene(0);
                break;

        }
    }

    void CurrentTextSelect(bool state )
    {
        if(state)
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
