using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleEffect : MonoBehaviour
{
   [SerializeField] GameObject fadetext;
    Text titletext;

    private void Start()
    {
        titletext = GetComponent<Text>();
        StartCoroutine(textnumerator());
    }
    IEnumerator textnumerator()
    {
       
        int a = 0;
        while(a < 30)
        {
            a++;
            yield return new WaitForSeconds(.05f);
            GameObject textinst = Instantiate(fadetext, transform.position, Quaternion.identity);
            textinst.transform.parent = gameObject.transform.parent;
            textinst.transform.localScale = transform.localScale;
            textinst.GetComponent<Text>().text = titletext.text;
        }
    }        

        }
