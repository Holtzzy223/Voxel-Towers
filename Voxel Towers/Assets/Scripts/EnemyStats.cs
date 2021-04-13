using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] float baseHP = 5f;
    [SerializeField] float currentHP = 0f;
    Enemy enemy;

    // Start is called before the first frame update
    void OnEnable()
    {
        //int health per wave at base * wave count 
        WavePool wavePool = FindObjectOfType<WavePool>();
        if (wavePool != null)
        {
            float maxHP = baseHP * wavePool.WaveCount;
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
    private void OnParticleCollision(GameObject other)
    {
        Debug.LogError("Hit by Particle "+other.name);   
        ProcessHit(other);
    }
    void ProcessHit(GameObject other) 
    {

        currentHP -= other.GetComponentInParent<Tower>().Damage;
        if (currentHP <= 0)
        {
            gameObject.SetActive(false);
            enemy.RewardCurrency();
        }
    }
}
