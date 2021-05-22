using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TesterFunctions : MonoBehaviour
{
    public Slider timeSlider;

    public TextMeshProUGUI timeScaleValue;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
     
        UpdateDebugValues();
    }

    void UpdateDebugValues()
    {
        Time.timeScale = timeSlider.value;
        timeScaleValue.text = timeSlider.value.ToString();
    }
  
}
