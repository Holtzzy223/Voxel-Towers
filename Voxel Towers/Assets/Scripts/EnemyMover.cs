using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Tile> path = new List<Tile>();
    public float speed = 1f;
    float oldSpeed;
    [SerializeField] bool  pathRestartOnEnd = false;
    [SerializeField] bool  pathFinished;
    bool PathFinshed {get { return pathFinished; } }
    Enemy enemy;
    Base playerBase;
    private void Awake()
    {
        enemy = GetComponentInChildren<Enemy>();
        Debug.LogError(enemy.ToString());
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        speed = enemy.GetComponent<EnemyStats>().currentSpeed;
        oldSpeed = speed;
        pathFinished = false;
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    // Update is called once per frame
    void FindPath()
    {
        path.Clear();
        GameObject parent = GameObject.FindGameObjectWithTag("Path");
        foreach (Transform child in parent.transform)
        {
            path.Add(child.GetComponent<Tile>());
            child.GetComponentInParent<Tile>().isPlaceable = false;
        }
        
    }
    void ReturnToStart() 
    {
        transform.position = path[0].transform.position;
    
    }
    IEnumerator FollowPath()
    {
        
        foreach (Tile waypoint in path)
        {
            var offset = new Vector3(0, 1, 0);
            Vector3 startPos = transform.position;
            Vector3 endPos = waypoint.transform.position+offset;
            float travelPercent = 0f;
            transform.LookAt(endPos);
            Debug.Log(waypoint.name);
            while (travelPercent < 1)
            {
                travelPercent += Mathf.Abs(speed)*Time.deltaTime;
                transform.position = Vector3.Lerp(startPos, endPos+offset, travelPercent);
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

            speed = Mathf.Clamp(speed,0f, speed-damage); ;
        }
        else
        {
            Debug.LogError("Collider is Null!");
            speed = oldSpeed;
        }
        

    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.LogError("Hit by bullet " + other.name);
        ProcessHit(other);
    }
    
}
