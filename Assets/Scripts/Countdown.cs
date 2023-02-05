using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Countdown : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float n = 4f;
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    public TextMeshProUGUI timeText;
    public GameObject options, final;
    bool a;
    float v;

    private void Start()
    {
        //options.SetActive(false);
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
                a = true;

            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                final.SetActive(true);
                //timeText.enabled = false;
            }
        }



        if (a)
        {
            v += Time.deltaTime;
            if (v >= 3)
            {
                bool anyVictory = VirtualRAM.playerVictories == 3 || VirtualRAM.bot1Victories == 3 || VirtualRAM.bot2Victories == 3 || VirtualRAM.bot3Victories == 3;
                if (anyVictory)
                {
                    VirtualRAM.playerVictories = 0;
                    VirtualRAM.bot1Victories = 0;
                    VirtualRAM.bot2Victories = 0;
                    VirtualRAM.bot3Victories = 0;
                }
                SceneManager.LoadScene(anyVictory ? "Option Scene" : "PruebaMovimiento");
            }
        }


    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        //timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void Options()
    {
        options.SetActive(false);
        timeText.enabled=true;
        Time.timeScale= 1;
    }












}
