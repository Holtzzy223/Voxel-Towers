using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyStats : MonoBehaviour
{
    [SerializeField] float baseHP = 10f;
    [SerializeField] float currentHP = 0f;
    [SerializeField] float maxHP;
    public Slider healthBar;
    Enemy enemy;

    // Start is called before the first frame update
    void OnEnable()
    {
        healthBar.maxValue = maxHP;
        healthBar.value = healthBar.maxValue;
        //int health per wave at base * wave count 
        WavePool wavePool = FindObjectOfType<WavePool>();
        if (wavePool != null)
        {
            if (wavePool.WaveCount > 1)
            {
                maxHP = baseHP * (wavePool.WaveCount );
            }
            else 
            {
                maxHP = baseHP;
            }
            currentHP = maxHP;
        }
    }
    private void Start()
    {
        enemy = GetComponent<Enemy>();
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

        currentHP -= other.GetComponentInParent<VoxelArsenal.VoxelProjectileScript>().BulletDamage;
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
