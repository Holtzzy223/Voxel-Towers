using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TesterFunctions : MonoBehaviour
{
    public Slider timeSlider;
    public Slider healthSlider;
    public Slider speedSlider;
    public TextMeshProUGUI timeScaleValue;
    public TextMeshProUGUI healthValue;
    public TextMeshProUGUI speedValue;
    bool updatedUI = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        InitDebugUI();
        UpdateDebugValues();
    }
     public  void InitDebugUI()
    {
        var enemyStats = FindObjectOfType<EnemyStats>();
        
        if (enemyStats != null && updatedUI == false)
        {
   
            healthSlider.maxValue = enemyStats.maxHP * 4;
            speedSlider.maxValue = 5;
            updatedUI = true;
            
        }
    }
    void UpdateDebugValues()
    {
        var enemyStats = FindObjectOfType<EnemyStats>();

        var towerUI = FindObjectOfType<Tower>().onHoverUI;
        if (towerUI.activeInHierarchy == false)
        {
            Time.timeScale = timeSlider.value;
        }
        enemyStats.maxHP = healthSlider.value;
        enemyStats.maxSpeed = speedSlider.value;
        enemyStats.currentHP = healthSlider.value;
        enemyStats.currentSpeed = speedSlider.value;
        timeScaleValue.text = timeSlider.value.ToString();
        healthValue.text = enemyStats.maxHP.ToString();
        speedValue.text = enemyStats.maxSpeed.ToString();
            
        

    }
  
}
