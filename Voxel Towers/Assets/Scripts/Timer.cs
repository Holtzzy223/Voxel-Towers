using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class Timer : MonoBehaviour
{
    public float timeRemaining;
    public bool timerIsRunning = false;
    public bool displayTime;
    public TextMeshProUGUI timeText;
    private float waveTimer;
    private float spawnTimer;
    public bool isWaveTimer = true;
    public bool isAlarmTimer = false;
    


    private void Awake()
    {
        waveTimer = FindObjectOfType<WavePool>().WaveTimer;
        spawnTimer = FindObjectOfType<WavePool>().SpawnTimer;
        timeRemaining = waveTimer * spawnTimer;
        // Starts the timer automatically
        
    }

    void Update()
    {
        RunTimer();
    }
    public void RunTimer()
    {
         if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                if (displayTime)
                {
                    DisplayTime(timeRemaining);
                }
            }
            else
            {
                Debug.Log("Time has run out!");

                if (isWaveTimer)
                {
                    
                    timeRemaining = 0;
                   if (FindObjectOfType<Base>().CurrentHP== FindObjectOfType<Base>().MaxHP)
                   {
                       FindObjectOfType<PlayerBank>().Deposit(50);
                   }
                    WavePool wavePool = FindObjectOfType<WavePool>();
                    if (!wavePool.triggered)
                    {
                        wavePool.StartCoroutine(wavePool.SpawnWave());
                    }
                    timeRemaining = waveTimer * spawnTimer;
                }
                if (isAlarmTimer)
                {
                   
                }
      
                //timerIsRunning = false;
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
}