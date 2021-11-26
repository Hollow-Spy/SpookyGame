using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WrittingCheck : MonoBehaviour
{
    public UnityEngine.UI.Text myText;
    // Get index of character.
    InputField input;
    public Text codetarget;
    public Text textwrite;
    bool diff;

    // Start is called before the first frame update
    void Start()
    {
       
        input = GetComponent<InputField>();
    }

    public void CheckGramma()
    {
        if(input.caretPosition==0)
        {
            return;
        }

        if(codetarget.text.Substring(0,input.text.Length) != input.text.Substring(0,input.text.Length) )
        {
            Debug.Log("diff");
            

            // Replace text with color value for character.
            textwrite.color = Color.red;
        }
        else
        {
            textwrite.color = Color.white;

            if(codetarget.text.Length == input.text.Length)
            {
                
            }

        }


    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
