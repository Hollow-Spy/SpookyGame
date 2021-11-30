using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CheckText : MonoBehaviour
{
    InputField userInput;   
    public Text write;
    public GameObject key;
    
    public Text PromptMessage;
    public GameObject Send;
   
    


    // Start is called before the first frame update
    void Start()
    {
       
        userInput = GetComponent<InputField>();
        userInput.text = write.text;
        Send.GetComponent<Button>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CheckEmail()
    {
        GameObject Keyboard = Instantiate(key, transform.position, Quaternion.identity);
        Keyboard.GetComponent<AudioSource>().volume = Random.Range(.5f, .8f);
        Keyboard.GetComponent<AudioSource>().pitch = Random.Range(.7f, .95f);
       
        if(userInput.text.Length < 20 && userInput.text.Length > 200)//this prevents the player from entering anything over 200 characters and under 20 characters
        {                                        
            write.color = Color.blue;
            Debug.Log("Error: email exceeds character limit.");

        }
        else
        {
            if(userInput.text.Length >= 20 && userInput.text.Length <= 200)
            {
                
                Send.GetComponent<Button>().enabled = true;//u
                Debug.Log("You can send Email now");

            }
            else
            {

            }
           



        }

        
    }
}
