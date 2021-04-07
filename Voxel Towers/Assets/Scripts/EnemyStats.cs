using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] int maxHP = 5;
    [SerializeField] int currentHP = 0;
    Enemy enemy;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        currentHP = maxHP;
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
        ProcessHit();
    }
    void ProcessHit() 
    {
        currentHP -= 2;
        if (currentHP <= 0)
        {
            gameObject.SetActive(false);
            enemy.RewardCurrency();
        }
    }
}
