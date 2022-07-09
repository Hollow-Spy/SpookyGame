using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuActivator : MonoBehaviour
{

    [SerializeField] GameObject PauseMenu,OptionsMenu;
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu.SetActive(true);
        }
    }

    private void Start()
    {
        Application.targetFrameRate = 60;

        PauseMenu.SetActive(true);
        OptionsMenu.SetActive(true);
        OptionsMenu.SetActive(false);
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        if (FindObjectOfType<PlayerController>())
        {
            FindObjectOfType<PlayerController>().enabled = true;
        }

    }
}
