using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    private Text text;
    public int time = 5;

    private void Start()
    {
        text = GetComponent<Text>();
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        for(int i = time; i>0 ; i--)
        {
            text.text = i.ToString();
            yield return new WaitForSeconds(1);
        }

        this.gameObject.SetActive( false);
    }
}
