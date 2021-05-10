using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyStats : MonoBehaviour
{
    [SerializeField] float baseHP = 20f;
    [SerializeField] float currentHP = 0f;
    [SerializeField] float maxHP;  
    public float baseSpeed = 0.5f;
    public float currentSpeed = 0f;
    public float maxSpeed;
    public Slider healthBar;
    Enemy enemy;

    // Start is called before the first frame update
    void OnEnable()
    {
        healthBar.maxValue = maxHP;
        healthBar.value = healthBar.maxValue;
        //int health per wave at base * wave count 
        WavePool wavePool = FindObjectOfType<WavePool>();
        UpdateStats(wavePool);
    }



    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }
    private void UpdateStats(WavePool wavePool)
    {
        if (wavePool != null)
        {
            if (wavePool.WaveCount > 1)
            {
                maxHP = baseHP * (wavePool.WaveCount);
                maxSpeed = baseSpeed +(wavePool.WaveCount * 0.08f);
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
    void Update()
    {
        
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
            gameObject.SetActive(false);
            enemy.RewardCurrency();
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.LogError("Hit by bullet " + other.name);
        ProcessHit(other);
    }
}
