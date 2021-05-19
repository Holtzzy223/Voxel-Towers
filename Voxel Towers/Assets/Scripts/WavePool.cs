using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class WavePool : MonoBehaviour
{
    [SerializeField] int enemiesPerWave;
    private int waveCount = 0;
    public int WaveCount { get { return waveCount; } }
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
        if (waveCount == waves.Length)
        {
            var sceneCurrent = SceneManager.GetActiveScene();
            switch (sceneCurrent.name)
            {
                case "Scene1":
                    SceneManager.LoadScene("Scene2");
                    break;
                case "Scene2":
                    SceneManager.LoadScene("Scene3");
                    break;
                case "Scene3":
                    SceneManager.LoadScene("Scene4");
                    break;
                case "Scene4":
                    SceneManager.LoadScene("Scene5");
                    break;
               // case "Scene5":
               //     SceneManager.LoadScene("GameWinScene");
               //     break;
            }
        }


    }



}
