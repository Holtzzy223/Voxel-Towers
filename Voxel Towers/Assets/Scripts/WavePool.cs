using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavePool : MonoBehaviour
{
    [SerializeField] int enemiesPerWave;
    private float waveCount = 0;
    public float WaveCount { get { return waveCount; } }
    public int EnemiesPerWave { get { return enemiesPerWave; } }
    [SerializeField] float waveTimer;

    public GameObject[] waves;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnWave()
    {


        for (int i = 0; i < waves.Length; i++)
        {
            if (waves[i].activeInHierarchy == false)
            {
                //waves[i].SetActive(true);
                waveCount++;
                Instantiate(waves[i], transform);
                
                yield return new WaitForSeconds(waveTimer);
            }
        }



    }

}
