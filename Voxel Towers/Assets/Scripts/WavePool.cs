using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class WavePool : MonoBehaviour
{
    private int enemiesPerWave;
    private int waveCount = 0;
    private int roundCount = 0;
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
    public TextMeshProUGUI roundText;
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

        }  


    }
    void SetCoords()
    {
        var playerBase = FindObjectOfType<Base>();
        var pathfinder = FindObjectOfType<Pathfinder>();
        var gridManager = FindObjectOfType<GridManager>();
       // playerBase.gameObject.SetActive(false);
        pathfinder.startCoords = new Vector2Int(Random.Range(0, gridManager.gridSize.x - 1), Random.Range(0, gridManager.gridSize.y - 1));
        //pathfinder.destinationCoords = new Vector2Int(Random.Range(1, gridManager.gridSize.x - 2), Random.Range(1, gridManager.gridSize.y - 2));//new Vector2Int(14, 8);
        if (pathfinder.StartCoords!=pathfinder.DestinationCoords)//&& !pathfinder.WillBlockPath(pathfinder.destinationCoords))
        {
            pathfinder.GetNewPath();
            ResetPool();
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
        enemiesPerWave = Random.Range(7,12);
        maxWaves = Random.Range(5, 25);
        waves = new GameObject[maxWaves];
        roundCount++;
        for (int i = 0; i < maxWaves; i++)
        {
            if (i < 10)
            {
                waves[i] = pools[Random.Range(0, 2)];
            }
            else
            {
                waves[i] = pools[Random.Range(0, 3)];
            }
        }
        roundText.text = "Round: " + roundCount.ToString();
        waveText.text = "Wave: " + waveCount.ToString() + " / " + maxWaves.ToString();
    }

}
