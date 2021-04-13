using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class WavePool : MonoBehaviour
{
    [SerializeField] int enemiesPerWave;
    private float waveCount = 0;
    public float WaveCount { get { return waveCount; } }
    public int EnemiesPerWave { get { return enemiesPerWave; } }
    [SerializeField] float waveTimer;
    public float WaveTimer { get { return waveTimer; } }
    [SerializeField] float spawnTimer;
    public float SpawnTimer { get { return spawnTimer; } }

    public GameObject[] waves;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI waveTimerText;
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(SpawnWave());
        
    }

    // Update is called once per frame
    void FixedUpdate()
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
                waveText.text = "Wave: "+ waveCount.ToString();

                Instantiate(waves[i], transform);
                
                yield return new WaitForSeconds(waveTimer*spawnTimer);
            }
        }



    }



}
