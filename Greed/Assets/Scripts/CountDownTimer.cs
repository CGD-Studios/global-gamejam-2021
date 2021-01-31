using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDownTimer : MonoBehaviour
{
    [SerializeField] float startTime;
    [SerializeField] TextMeshProUGUI timerText;

    float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Timer());
    }

    public void StartTimer() {
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        timer = startTime;

        do
        {
            timer -= Time.deltaTime;
            if (timer <= 10) SoundManager.PlayClockFast();

            FormatText();

            yield return null;
        } while (timer > 0);
    }

    private void FormatText()
    {
        int minutes = (int)(timer / 60) % 60;
        float seconds = (timer % 60);
        string secondString = seconds.ToString("F2");

        timerText.text = "";

        if (minutes > 0)
        {
            timerText.text += minutes + " : ";
        }
        if (seconds > 0f)
        {
            timerText.text += secondString;
        }
        if (seconds < 0f)
        {
            timerText.text += "Times Up!";
            GameController.TimerEnd();
        }
    }
}
