using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] int maxHP = 5;
    [SerializeField] int currentHP = 0;
    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;

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
        currentHP--;
        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
