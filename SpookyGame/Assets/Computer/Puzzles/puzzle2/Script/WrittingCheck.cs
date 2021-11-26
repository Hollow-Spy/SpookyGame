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
    [SerializeField] GameObject win;
    [SerializeField] GameObject keysound;
    [SerializeField] string[] prompts;

    // Start is called before the first frame update
    void Start()
    {
        codetarget.text = prompts[Random.Range(0, prompts.Length)];
        input = GetComponent<InputField>();
    }

    public void CheckGramma()
    {
      GameObject sound =   Instantiate(keysound, transform.position, Quaternion.identity);
        sound.GetComponent<AudioSource>().volume = Random.Range(.7f, .9f);
        sound.GetComponent<AudioSource>().pitch = Random.Range(.75f, .9f);
        if(input.text.Length <= codetarget.text.Length && codetarget.text.Substring(0,input.text.Length) != input.text.Substring(0,input.text.Length))
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
                win.SetActive(true);
            }

        }


    }


}
