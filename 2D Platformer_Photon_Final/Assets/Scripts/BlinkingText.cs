using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingText : MonoBehaviour
{
    Text text;
    //public GameObject startText;
       
    void Start()
    {
        text = GetComponent<Text>();
        StartBlinking();
    }
           
    IEnumerator Blink()
    {
        while (true)
        {
            switch (text.color.a.ToString())
            {
                case "0":
                    text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
                    yield return new WaitForSeconds(0.5f);
                    break;

                case "1":
                    text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
                    yield return new WaitForSeconds(0.5f);
                    break;
            }
        }
    }

    void StartBlinking()
    {
        StopCoroutine("Blink");
        StartCoroutine("Blink");

        //StopCoroutine("EnableAndDisable");
        //StartCoroutine("EnableAndDisable");
    }

    void StopBlinking()
    {
        StopCoroutine("Blink");
        //StopCoroutine("EnableAndDisable");
    }
    /*
    IEnumerator EnableAndDisable()
    {
        startText.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        startText.SetActive(false);
        Debug.Log("EnableAndDisable");
    }*/

}
