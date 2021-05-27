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


    }




    private void UpdateStats(WavePool wavePool)
    {
        if (wavePool != null)
        {
            
            if (wavePool.WaveCount >3)
            {
                maxHP = (baseHP + 15) * (wavePool.WaveCount);
                maxSpeed = baseSpeed +(wavePool.WaveCount * 0.08f);
                enemy.killReward = Mathf.RoundToInt(enemy.killReward * 1.15f);
            }
            if (wavePool.WaveCount >10)
            {
                maxHP = (baseHP*(1+(wavePool.RoundCount*0.25f))) * (wavePool.WaveCount*0.25f);
                maxSpeed = baseSpeed + (wavePool.WaveCount * 0.08f);
                enemy.killReward = Mathf.RoundToInt(enemy.killReward * 1.5f);
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
