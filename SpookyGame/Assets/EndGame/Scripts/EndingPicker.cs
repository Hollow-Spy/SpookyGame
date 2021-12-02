using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingPicker : MonoBehaviour
{
    [SerializeField]  int goodscore, badscore;
    [SerializeField] GameObject goodEnd, badEnd, okEnd;
    void Start()
    {
       

        if(PlayerPrefs.GetInt("Score") >= goodscore)
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

}
