using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    
    [SerializeField] Tower tower;
    public bool isPlaceable = true;
    
    public bool IsPlaceable { get   { return isPlaceable;}}

    private void OnMouseOver()
    {

        if (isPlaceable && Input.GetMouseButtonDown(0))
        {
            bool isPlaced = tower.CreateTower(tower,transform.position);
            isPlaceable = !isPlaced;
        }
    }
}
