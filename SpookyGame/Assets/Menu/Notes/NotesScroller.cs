using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesScroller : MonoBehaviour
{

    int index;
    [SerializeField] GameObject[] Pages;
    [SerializeField] GameObject PageSound;
    public void LeftNavigate()
    {
        Pages[index].SetActive(false);
        if(index-1 < 0)
        {
            index = Pages.Length-1;
        }
        else
        {
            index--;
        }
        Pages[index].SetActive(true);
        Instantiate(PageSound, transform.position, Quaternion.identity);
    }

    public void RightNavigate()
    {
        Pages[index].SetActive(false);
        if (index + 1 > Pages.Length-1)
        {
            index = 0;
        }
        else
        {
            index++;
        }
        Pages[index].SetActive(true);
        Instantiate(PageSound, transform.position, Quaternion.identity);
    }
}
