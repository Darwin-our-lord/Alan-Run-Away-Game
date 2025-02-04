using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public TMP_Text timerTxt;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;

        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSecondsRealtime(1);
        timerTxt.text = "3";
        yield return new WaitForSecondsRealtime(1);
        timerTxt.text = "2";
        yield return new WaitForSecondsRealtime(1);
        timerTxt.text = "1";
        yield return new WaitForSecondsRealtime(1);
        timerTxt.text = "";
        Time.timeScale = 1f;
    }

}
