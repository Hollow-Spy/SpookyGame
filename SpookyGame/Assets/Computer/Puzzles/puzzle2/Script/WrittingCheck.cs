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

        if(codetarget.text[input.caretPosition-1] != input.text[input.caretPosition-1] )
        {
            Debug.Log("diff");

            // Replace text with color value for character.
            textwrite.color = Color.red;
        }
        else
        {
            textwrite.color = Color.white;

        }


    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
