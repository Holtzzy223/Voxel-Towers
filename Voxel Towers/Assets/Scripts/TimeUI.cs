using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeUI : MonoBehaviour
{

    public float oldTime;
    public float currentTime;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
     
        
    }

    public void PauseButton()
    {
        oldTime = Time.timeScale;
        Time.timeScale = 0;

    }
    public void PlayButton()
    {
        Time.timeScale = oldTime; 
    }
    public void NormalTimeButton()
    {
        Time.timeScale = 1;
    }
    public void halfTimeButton()
    {
        Time.timeScale = 0.5f;    
    }

    public void DoubleTimeButton() 
    {
        Time.timeScale = 2;
    }

  
}
