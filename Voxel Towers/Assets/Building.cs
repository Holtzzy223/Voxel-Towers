using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Building : MonoBehaviour
{
    public bool isRefinery = false;
    public int cost;
    public float chargeTime;
    public float refineTime;
    public float refineAmount;
    public GameObject UI;
    public GameObject[] buttons;
    public int health;
    public GridManager gridManager;
    // Start is called before the first frame update
    void Start()
    {
  
        buttons = FindObjectsOfType<GameObject>(true);
        Debug.LogAssertion(buttons.Length);
        gridManager = FindObjectOfType<GridManager>();
    }

    // Update is called once per frame
    void Update()
    {
   
    }
    public bool CreateBuilding(Building building, Vector3 position)
    {
        PlayerBank bank = FindObjectOfType<PlayerBank>();
        var offset = new Vector3(0, 1, 0);
        if (bank == null)
        {
            return false;
        }
        if (bank.CurrentBalance >= cost)
        {
   
            offset = new Vector3(0, 3, 0);
            Instantiate(building, position + offset, Quaternion.identity);
          
            bank.Withdrawl(cost);


            return true;
        }
        else
        {
            return false;
        }

    }
   

    private void OnDestroy()
    {
        CancelInvoke();
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UI.SetActive(true);
        }
        if (Input.GetMouseButtonDown(1))
        {
            UI.SetActive(false);
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
        //if (isWall)
        //{
        //    gridManager.ClearNode(gridManager.GetCoordsFromPos(closestPoint.position));
        //    FindObjectOfType<Pathfinder>().NotifyRecievers();
        //}
        Destroy(this.gameObject);
    }
    public void DamageBuilding()
    {
        health--;
        Debug.LogError("Trap loss health -1, total health = " + health);
        if (health <= 0)
        {
            KillSelf();
        }
    }

}
