using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class popupMenu : MonoBehaviour
{
    public Canvas emailWindow;
    public bool MenuOpen = false;
    // Start is called before the first frame update
   
    public void menu()
    {
        if (MenuOpen == false)
        {
            MenuOpen = true;
            emailWindow.enabled = true;
        }
        else if(MenuOpen == true)
        {
            MenuOpen = false;
            emailWindow.enabled = false;
        }

        
    }
}
