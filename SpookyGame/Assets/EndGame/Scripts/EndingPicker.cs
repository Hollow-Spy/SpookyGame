using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingPicker : MonoBehaviour
{
    [SerializeField]  int goodscore, badscore;
    [SerializeField] GameObject goodEnd, badEnd, okEnd;
    [SerializeField] Text scoreText;
    void Start()
    {
        scoreText.text = PlayerPrefs.GetInt("Score").ToString();

        if (PlayerPrefs.GetInt("Score") >= goodscore)
        {
            goodEnd.SetActive(true);
        }
        else
        {
            if(PlayerPrefs.GetInt("Score") <= badscore)
            {
                badEnd.SetActive(true);

            }
            else
            {
                okEnd.SetActive(true);

            }
        }


    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            FindObjectOfType<SceneLoader>().LoadScene("Menu");
        }
    }

}
