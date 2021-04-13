using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;


    [SerializeField] int poolSize;
    [SerializeField] float spawnTimer;
    public float SpawnTimer {get { return spawnTimer; } }


    public GameObject[] pool;
    
    int spawnCount = 1;
    private void Awake()
    {

        WavePool manager = FindObjectOfType<WavePool>();
        spawnTimer = manager.SpawnTimer;
        if (manager != null)
        {
            poolSize = manager.EnemiesPerWave;
            PopulatePool();
        }
        else
        {
            Debug.Log("Wave Manager is fucked");
        }
    }

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    void PopulatePool() 
    {
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
