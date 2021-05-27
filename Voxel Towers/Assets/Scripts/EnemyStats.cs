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
       
        UpdateStats();


    }




    private void UpdateStats()
    {
        WavePool wavePool = FindObjectOfType<WavePool>();
        if (wavePool != null)
        {
            Debug.LogError("FOUND THE POOL");
            if (wavePool.WaveCount <=3)
            {
                maxHP = (baseHP + 15) * (wavePool.WaveCount + (wavePool.RoundCount * 0.05f));
                maxSpeed = baseSpeed; //+(wavePool.WaveCount * 0.08f);
                enemy.killReward = Mathf.RoundToInt(enemy.killReward * 1.10f);
            }
            if (wavePool.WaveCount>3&&wavePool.WaveCount <=10)
            {
                maxHP = (baseHP*1.35f) * (wavePool.WaveCount+(wavePool.RoundCount*0.10f));
                maxSpeed = baseSpeed + (wavePool.WaveCount * 0.02f);
                enemy.killReward = Mathf.RoundToInt(enemy.killReward * 1.15f);
            }
            if ( wavePool.WaveCount > 10)
            {
                maxHP = (baseHP * 1.5f) * (wavePool.WaveCount + (wavePool.RoundCount * 0.15f));
                maxSpeed = baseSpeed + (wavePool.WaveCount * 0.04f);
                enemy.killReward = Mathf.RoundToInt(enemy.killReward * 1.25f);
            }
           
            currentHP = maxHP;
            currentSpeed = maxSpeed;
            Debug.LogError("Enemy Health: " + currentHP);
            Debug.LogError("Enemy Speed: " + currentSpeed);
        }

    }
    // Update is called once per frame

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
            UpdateHeathBar();
            Debug.LogError("Hit by bullet " + other.name + "  damage: " + damage);
            if (other.gameObject.CompareTag("Trap"))
            {
                damage = 1;
            }
        }
        else
        {
            Debug.LogError("Collider is Null!");
        }
        
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
       
        ProcessHit(other);
    }
    private void OnTriggerStay(Collider other)
    {
        ProcessHit(other);
    }
}
