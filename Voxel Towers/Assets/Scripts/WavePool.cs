using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class WavePool : MonoBehaviour
{
    [SerializeField] int enemiesPerWave;
    private int waveCount = 0;
    public int maxWaves;
    public int WaveCount { get { return waveCount; } }
    public int EnemiesPerWave { get { return enemiesPerWave; } }
    [SerializeField] float waveTimer;
    public float WaveTimer { get { return waveTimer; } }
    [SerializeField] float spawnTimer;
    public float SpawnTimer { get { return spawnTimer; } }

    public GameObject[] waves;
    public GameObject[] pools;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI waveTimerText;
    public bool triggered = false;
    private void Awake()
    {
        SetWaves();
    }
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
    public IEnumerator SpawnWave()
    {

        triggered = true;
        for (int i = 0; i < waves.Length; i++)
        {
            if (waves[i].activeInHierarchy == false)
            {
                //waves[i].SetActive(true);
                waveCount++;
                waveText.text = "Wave: " + waveCount.ToString() + " / " + maxWaves.ToString();

                Instantiate(waves[i], transform);

                yield return new WaitForSeconds(waveTimer * spawnTimer);
            }

        }
        if (waveCount == waves.Length)
        {
            ExpandGrid(5, 0);
            Invoke("ResetPool", 10f);
            // var sceneCurrent = SceneManager.GetActiveScene();
            // switch (sceneCurrent.name)
            // {
            //     case "Scene1":
            //         // SceneManager.LoadScene("Scene2");
            //         ExpandGrid(5,0);
            //         Invoke("ResetPool", 3f);
            //         break;
            //     case "Scene2":
            //         ExpandGrid(5, 0);
            //         
            //         break;
            //     case "Scene3":
            //         ExpandGrid(5, 0);
            //         break;
            //     case "Scene4":
            //         ExpandGrid(5, 0);
            //         break;
            //         // case "Scene5":
            //         //     SceneManager.LoadScene("GameWinScene");
            //         //     break;
            // }
        }  //


    }
    void SetCoords()
    {
        var playerBase = FindObjectOfType<Base>();
        var pathfinder = FindObjectOfType<Pathfinder>();
        var gridManager = FindObjectOfType<GridManager>();
        playerBase.gameObject.SetActive(false);
        pathfinder.startCoords = new Vector2Int(Random.Range(0, gridManager.gridSize.x - 1), Random.Range(0, gridManager.gridSize.y - 1));
        pathfinder.destinationCoords = new Vector2Int(Random.Range(1, gridManager.gridSize.x - 2), Random.Range(1, gridManager.gridSize.y - 2));//new Vector2Int(14, 8);
        if (!pathfinder.WillBlockPath(pathfinder.startCoords) && !pathfinder.WillBlockPath(pathfinder.destinationCoords))
        {
            pathfinder.GetNewPath();
        }
        else
        {
            SetCoords();
        }

    }
    void ExpandGrid(int sizeX, int sizeY)
    {
        var gridManager = FindObjectOfType<GridManager>();
        gridManager.gridSize += new Vector2Int(sizeX, sizeY);
        gridManager.StartCoroutine(gridManager.CreateGrid(gridManager.gridSize.x - sizeX, 0, false));
        Invoke("SetCoords", 1f);

    }
    void ResetPool()
    {
        waveCount = 0;
        SetWaves();
        FindObjectOfType<Timer>().timerIsRunning = true;
        StartCoroutine(SpawnWave());
    }
    void SetWaves()
    {
        maxWaves = Random.Range(5, 25);
        waves = new GameObject[maxWaves];
        for (int i = 0; i < maxWaves; i++)
        {

            waves[i] = pools[Random.Range(0, 2)];
        }
        waveText.text = "Wave: " + waveCount.ToString() + " / " + maxWaves.ToString();
    }

}
