using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootUp : MonoBehaviour
{
    public GameObject loadingCircle;
    public GameObject desktop;

   
    int tries=0;
    public void Loading()
    {
        loadingCircle.SetActive(true);

    }

 
    public void loadchance()
    {

        int rand = Random.Range(0, 2);
        if(rand==0)
        {
            loaded();

        }
        else
        {
            tries++;
            if (tries > 1)
            {
                loaded();
            }

        }
       
    }

    public void loaded()
    {
        loadingCircle.SetActive(false);
        desktop.SetActive(true);
    }
   
}
