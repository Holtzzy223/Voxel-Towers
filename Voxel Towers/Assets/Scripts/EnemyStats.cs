using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyStats : MonoBehaviour
{
    public float baseHP = 20f;
    public float currentHP = 0f;
    public float maxHP;
    public bool isBreaker;
    public float baseSpeed = 0.5f;
    public float currentSpeed = 0f;
    public float maxSpeed;
    public Slider healthBar;
    public Enemy enemy;
    public GameObject childEnemy;
    bool updatedDebug = false;
    Slider healthSlider;
    Slider speedSlider;
    // Start is called before the first frame update
    void OnEnable()
    {
        healthBar.maxValue = maxHP;
        healthBar.value = healthBar.maxValue;
        //int health per wave at base * wave count 
        WavePool wavePool = FindObjectOfType<WavePool>();
        UpdateStats(wavePool);
        healthSlider  = FindObjectOfType<TesterFunctions>().healthSlider;
        speedSlider = FindObjectOfType<TesterFunctions>().speedSlider;
        healthSlider.onValueChanged.AddListener(delegate { UpdateDebugStats(); });
        speedSlider.onValueChanged.AddListener(delegate { UpdateDebugStats(); });

    }




    private void UpdateStats(WavePool wavePool)
    {
        if (wavePool != null)
        {
            if (wavePool.WaveCount >1)
            {
                maxHP = (baseHP + 15) * (wavePool.WaveCount);
                maxSpeed = baseSpeed +(wavePool.WaveCount * 0.08f);
                enemy.killReward = Mathf.RoundToInt(enemy.killReward * 1.15f);
            }
            if (wavePool.WaveCount >3)
            {
                maxHP = (baseHP*1.15f) * (wavePool.WaveCount);
                maxSpeed = baseSpeed + (wavePool.WaveCount * 0.08f);
                enemy.killReward = Mathf.RoundToInt(enemy.killReward * 1.25f);
            }
            else
            {
                maxHP = baseHP;
                maxSpeed = baseSpeed;
            }
            currentHP = maxHP;
            currentSpeed = maxSpeed;
        }
    }
    // Update is called once per frame
    void UpdateDebugStats()
    {
        maxHP = healthSlider.value;
        maxSpeed = speedSlider.value;
        currentHP = maxHP;
        currentSpeed = maxSpeed;
        FindObjectOfType<EnemyMover>().speed = currentSpeed;
    }
    private void UpdateHeathBar()
    {


        Debug.Log("Should change health bar");
        healthBar.value = currentHP;

    }
    void ProcessHit(Collider other) 
    {
        var damage = other.GetComponentInParent<VoxelArsenal.VoxelProjectileScript>().BulletDamage;
        if (other != null)
        {
            currentHP -= damage;
        }
        else
        {
            Debug.LogError("Collider is Null!");
        }
        UpdateHeathBar();
        if (currentHP <= 0)
        {
            if (isBreaker == true)
            {
                for (int i = 0; i < 3; i++)
                {
                    Vector3 spawnPosition = new Vector3(transform.position.x+i, transform.position.y, transform.position.z+i);

                    Instantiate(childEnemy,spawnPosition,Quaternion.identity);

                }
                gameObject.SetActive(false);
                enemy.RewardCurrency();
            }
            else
            {
                gameObject.SetActive(false);
                enemy.RewardCurrency();
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.LogError("Hit by bullet " + other.name);
        ProcessHit(other);
    }
}
