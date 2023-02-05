using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    public TextMeshProUGUI text;
    float n = 4f;
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    public TextMeshProUGUI timeText;
    public GameObject options, final;

    private void Start()
    {
        options.SetActive(false);
        // Starts the timer automatically
    }
    void Update()
    {
        n -= Time.deltaTime;

        int num = (int)n;
        text.text = num.ToString();

        if (n <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Time.timeScale == 1) { Time.timeScale = 0; options.SetActive(true); timeText.enabled = false; }
                else { Time.timeScale = 1; options.SetActive(false); timeText.enabled = true; }
            }

            timerIsRunning = true;
            gameObject.GetComponent<TextMeshProUGUI>().enabled = false;
        }
        if (timerIsRunning)
        {
            if (timeRemaining >= 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                final.SetActive(true);
                timeText.enabled = false;
            }
        }






    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void Options()
    {
        options.SetActive(false);
        timeText.enabled=true;
        Time.timeScale= 1;
    }












}
