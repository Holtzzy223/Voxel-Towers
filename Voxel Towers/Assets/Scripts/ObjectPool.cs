using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;

    [SerializeField] int waveSize   = 5;
    [SerializeField] int waveNumber = 5;
    [SerializeField] int poolSize;
    [SerializeField] float spawnTimer = 1f;
    [SerializeField] float waveTimer = 15f;
    GameObject[] pool;
    int waveCount = 1;
    int spawnCount = 1;
    private void Awake()
    {
        PopulatePool();
        
    }

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    void PopulatePool() 
    {
        poolSize = waveSize * waveNumber;
        pool = new GameObject[poolSize];
        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemyPrefab, transform);
            pool[i].SetActive(false);
        }
    }

    
    IEnumerator SpawnEnemy()
    {
        
        
            for (int i = 0; i < pool.Length; i++)
            {
                if (pool[i].activeInHierarchy == false)
                {
                    pool[i].SetActive(true);
                    
                    yield return new WaitForSeconds(spawnTimer);
                }
           }



    }

}
