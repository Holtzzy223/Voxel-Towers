using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int poolSize = 5;
    [SerializeField] float spawnTimer = 1f;
    GameObject[] pool;
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
        pool = new GameObject[poolSize];
        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemyPrefab, transform);
            pool[i].SetActive(false);
        }
    }

    // Update is called once per frame
    IEnumerator SpawnEnemy() 
    {
        for (int i = 0; i < pool.Length; i++)
        {
           
            pool[i].SetActive(true);
            yield return new WaitForSeconds(spawnTimer);
        }
       //while (true)
       //{
       //    Instantiate(enemyPrefab, transform);
       //    yield return new WaitForSeconds(spawnTimer);
       //}
    }
}
