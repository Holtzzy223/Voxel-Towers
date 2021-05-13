using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    public float speed = 1f;
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
            path.Add(child.GetComponent<Waypoint>());
            child.GetComponentInParent<Waypoint>().isPlaceable = false;
        }
        
    }
    void ReturnToStart() 
    {
        transform.position = path[0].transform.position;
    
    }
    IEnumerator FollowPath()
    {
        
        foreach (Waypoint waypoint in path)
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

}
