using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("UI").SetActive(false);
        Cursor.lockState = CursorLockMode.None;
    }
    public void ExitGame()
    {

    }

public void Restart()
    {
        SceneManager.LoadScene("OfficeProp");


    }

}
