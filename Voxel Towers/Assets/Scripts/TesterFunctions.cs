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
       EnemyStats[] enemyStats = FindObjectsOfType<EnemyStats>();
       EnemyMover[] enemyMover = FindObjectsOfType<EnemyMover>();

        var towerUI = FindObjectOfType<Tower>().onHoverUI;
        if (towerUI != null)
        {
            if (towerUI.activeInHierarchy == false)
            {
                Time.timeScale = timeSlider.value;
            }
        }
        for(int i = 0;i<enemyStats.Length;i++)
        {
            enemyStats[i].maxHP = healthSlider.value;
            enemyMover[i].speed = speedSlider.value;
            enemyStats[i].currentHP = healthSlider.value;
            
           
        }
        timeScaleValue.text = timeSlider.value.ToString();
        healthValue.text = FindObjectOfType<EnemyStats>().maxHP.ToString();
        speedValue.text = FindObjectOfType<EnemyMover>().speed.ToString();


    }
  
}
