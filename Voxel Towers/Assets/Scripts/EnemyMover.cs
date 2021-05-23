using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    List<Node> path = new List<Node>();
    public float speed = 1f;
    float oldSpeed;
    [SerializeField] bool  pathRestartOnEnd = false;
    [SerializeField] bool  pathFinished;
    bool PathFinshed {get { return pathFinished; } }
    Enemy enemy;
    Base playerBase;
    GridManager gridManager;
    Pathfinder pathfinder;
    private void Awake()
    {
        enemy = GetComponentInChildren<Enemy>();
       
        Debug.LogError(enemy.ToString());
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        pathfinder = FindObjectOfType<Pathfinder>();
        gridManager = FindObjectOfType<GridManager>();
        speed = enemy.GetComponent<EnemyStats>().currentSpeed;
        oldSpeed = speed;
        pathFinished = false;
        ReturnToStart();
        FindPath(true);
    }

    // Update is called once per frame
    void FindPath(bool resetPath)
    {
        Vector2Int coords = new Vector2Int();
        if (resetPath)
        {
            coords = pathfinder.StartCoords;
        }
        else
        {
            coords = gridManager.GetCoordsFromPos(transform.position);
        }
        StopAllCoroutines();
        path.Clear();
        path = pathfinder.GetNewPath(coords);
        StartCoroutine(FollowPath());
    }
    void ReturnToStart() 
    {
        transform.position = gridManager.GetPosFromCoords(pathfinder.StartCoords);
    
    }
    IEnumerator FollowPath()
    {
        
        for (int i = 1; i < path.Count; i++)
        {
            var offset = new Vector3(0, 1, 0);
            Vector3 startPos = transform.position;
            Vector3 endPos = gridManager.GetPosFromCoords(path[i].coords);
            float travelPercent = 0f;
            transform.LookAt(endPos);
            Debug.Log(path.Count);
            while (travelPercent < 1)
            {
                travelPercent += Mathf.Abs(speed)*Time.deltaTime;
                transform.position = Vector3.Lerp(startPos+offset, endPos+offset, travelPercent);
                yield return new WaitForFixedUpdate();
            }
        }

        if (pathRestartOnEnd)
        {
            pathFinished = true;
            enemy.DamageBase();
            ReturnToStart();
            StartCoroutine(FollowPath());
        }
        else 
        {

            pathFinished = true;
            enemy.DamageBase();
            gameObject.SetActive(false);
        }
    }
    void ProcessHit(Collider other)
    {
        var damage = other.GetComponentInParent<VoxelArsenal.VoxelProjectileScript>().SpeedDamage;
        
        if (other != null)
        {

            speed = Mathf.Clamp(speed,0f, speed-damage); 
        }
        else
        {
            Debug.LogError("Collider is Null!");
            speed = oldSpeed;
        }
        

    }
    private void OnTriggerEnter(Collider other)
    {
       
        ProcessHit(other);
    }
    
}
