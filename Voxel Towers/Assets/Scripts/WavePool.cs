using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class WavePool : MonoBehaviour
{
    private int enemiesPerWave;
    private int waveCount = -1;
    private int roundCount = -1;
    public int maxWaves;
    public int WaveCount { get { return waveCount; } }
    public int RoundCount { get { return roundCount; } }
    public int EnemiesPerWave { get { return enemiesPerWave; } }
    [SerializeField] float waveTimer;
    public float WaveTimer { get { return waveTimer; } }
    [SerializeField] float spawnTimer;
    public float SpawnTimer { get { return spawnTimer; } }

    public GameObject[] waves;
    public GameObject[] pools;
    public GameObject towerChoice;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI roundText;
    public TextMeshProUGUI waveTimerText;
    public bool triggered = false;
    private void Awake()
    {
        SetWaves();
        SetStartCoords();
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        var gridManager = FindObjectOfType<GridManager>();
        pathfinder.startCoords = new Vector2Int(Random.Range(0, gridManager.gridSize.x), Random.Range(Random.Range(pathfinder.DestinationCoords.x - 6, pathfinder.DestinationCoords.y + 6), Random.Range(gridManager.gridSize.x, gridManager.gridSize.y)));
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
              if(waveCount > 0)
                {
                    if (waveCount == 1)
                    {
                        var gridManager = FindObjectOfType<GridManager>();
                        var pathfinder = FindObjectOfType<Pathfinder>();
                        //pathfinder.startCoords = new Vector2Int(Random.Range(0, 0), Random.Range(Random.Range(pathfinder.DestinationCoords.x - 6, pathfinder.DestinationCoords.y + 6), Random.Range(gridManager.gridSize.x, gridManager.gridSize.y)));
                        // SetCoords();
                    }
                    waveText.text = "Wave: " + waveCount.ToString() + " / " + maxWaves.ToString();
                    
                    Instantiate(waves[i], transform);
                }
                yield return new WaitForSeconds(waveTimer * spawnTimer);
            }

        }
        if (waveCount == waves.Length)
        {
            //ExpandGrid(5, 0);
            
            SetStartCoords();
            
        }  


    }
    public void SetStartCoords()
    {
        var playerBase = FindObjectOfType<Base>();
        var pathfinder = FindObjectOfType<Pathfinder>();
        var gridManager = FindObjectOfType<GridManager>();
       // playerBase.gameObject.SetActive(false);
        pathfinder.startCoords = new Vector2Int(Random.Range(0,gridManager.gridSize.x), Random.Range(Random.Range(pathfinder.DestinationCoords.x - 6,pathfinder.DestinationCoords.y+6),Random.Range(gridManager.gridSize.x, gridManager.gridSize.y)));
        //pathfinder.destinationCoords = new Vector2Int(Random.Range(1, gridManager.gridSize.x - 2), Random.Range(1, gridManager.gridSize.y - 2));//new Vector2Int(14, 8);
        if (pathfinder.StartCoords!=pathfinder.DestinationCoords)//&& !pathfinder.WillBlockPath(pathfinder.destinationCoords))
        {
            ResetPool();
            pathfinder.GetNewPath();
            pathfinder.NotifyRecievers();

        }
        else
        {
            SetStartCoords();
            
        }

    }
    void ExpandGrid(int sizeX, int sizeY)
    {
        var gridManager = FindObjectOfType<GridManager>();
        gridManager.gridSize += new Vector2Int(sizeX, sizeY);
        gridManager.StartCoroutine(gridManager.CreateGrid(gridManager.gridSize.x - sizeX, 0, false));
     

    }
    void ResetPool()
    {
        waveCount = 0;
        triggered = false;
        SetWaves();
        FindObjectOfType<Timer>().timerIsRunning = true;
      
    }
    void SetWaves()
    {
        
        roundCount++;
        switch (roundCount)
        {
            case 2:
                ActivateTowerChoice("Laser Upgrade");

                break;
            case 4:
                ActivateTowerChoice("Cannon Upgrade");
                break;
            case 6:
                ActivateTowerChoice("Sloth Upgrade");
                break;
            case 8:
                break;
        }

        enemiesPerWave = Random.Range(10,15);
        maxWaves = Random.Range(9,18);
        waves = new GameObject[maxWaves];
       
        for (int i = 0; i < maxWaves; i++)
        {
            if (i < 8)
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

    private void ActivateTowerChoice(string choiceTag)
    {
        var towerButtons = towerChoice.GetComponentsInChildren<Transform>(true);
        for (int i = 0; i < towerButtons.Length; i++)
        {
            if (towerButtons[i].CompareTag(choiceTag))
            {
                towerButtons[i].gameObject.SetActive(true);
            }
        }

    }
}
