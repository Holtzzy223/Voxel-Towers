using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] float speed = 1f;
    [SerializeField] bool  pathRestartOnEnd = false;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {

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
            Vector3 startPos = transform.position;
            Vector3 endPos = waypoint.transform.position;
            float travelPercent = 0f;
            transform.LookAt(endPos);
            Debug.Log(waypoint.name);
            while (travelPercent < 1)
            {
                travelPercent += Mathf.Abs(speed)*Time.deltaTime;
                transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }

        if (pathRestartOnEnd)
        {
            ReturnToStart();
            StartCoroutine(FollowPath());
        }
        else 
        {
            gameObject.SetActive(false);
        }
    }
}
