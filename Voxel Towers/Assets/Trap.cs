using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public int cost = 50;
    public int health = 10;
    public bool isWall = false;
    PlayerBank bank;        
    Pathfinder pathfinder;  
    GridManager gridManager;
    // Start is called before the first frame update
    void Start()
    {
      
        pathfinder = FindObjectOfType<Pathfinder>();
        gridManager = FindObjectOfType<GridManager>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool CreateTrap(Trap trap, Vector3 position)
    {
        bank = FindObjectOfType<PlayerBank>();
        var offset = new Vector3(0, 1, 0);
        if (bank == null)
        {
            return false;
        }
        if (bank.CurrentBalance >= cost)
        {
            if (!isWall)
            {
                Instantiate(trap, position + offset, Quaternion.Euler(-90, 0, 0));

            }
            if (isWall)
            {
                offset = new Vector3(0, 3, 0);
                Instantiate(trap, position + offset, Quaternion.identity);
            }
            bank.Withdrawl(cost);
           

            return true;
        }
        else
        {
            return false;
        }

    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            KillSelf();
           

        }
    }
    void KillSelf()
    {
        float targetDistance = 0f;

        Tile[] waypoints = FindObjectsOfType<Tile>();
        Transform closestPoint = null;
        float maxDistance = Mathf.Infinity;

        foreach (Tile waypoint in waypoints)
        {
            targetDistance = Vector3.Distance(transform.position, waypoint.transform.position);
            if (targetDistance < maxDistance)
            {
                closestPoint = waypoint.transform;
                maxDistance = targetDistance;
            }
        }
        var targetPoint = closestPoint;
        closestPoint.gameObject.GetComponent<Tile>().isPlaceable = true;
        if (isWall)
        {
            gridManager.ClearNode(gridManager.GetCoordsFromPos(closestPoint.position));
            FindObjectOfType<Pathfinder>().NotifyRecievers();
        }
        Destroy(this.gameObject);
    }
    public void DamageTrap()
    {
        health--;
        Debug.LogError("Trap loss health -1, total health = " + health);
        if (health <= 0)
        {
            KillSelf();
        }
    }
    private void OnDestroy()
    {
        float targetDistance = 0f;

        Tile[] waypoints = FindObjectsOfType<Tile>();
        Transform closestPoint = null;
        float maxDistance = Mathf.Infinity;

        foreach (Tile waypoint in waypoints)
        {
            targetDistance = Vector3.Distance(transform.position, waypoint.transform.position);
            if (targetDistance < maxDistance)
            {
                closestPoint = waypoint.transform;
                maxDistance = targetDistance;
            }
        }
        var targetPoint = closestPoint;
        closestPoint.gameObject.GetComponent<Tile>().isPlaceable = true;
        if (isWall)
        {
            gridManager.ClearNode(gridManager.GetCoordsFromPos(closestPoint.position));
            FindObjectOfType<Pathfinder>().NotifyRecievers();
        }
    }

} 
