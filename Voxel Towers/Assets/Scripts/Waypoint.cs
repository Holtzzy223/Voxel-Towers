using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    
    [SerializeField] Tower[] towers;
    public bool isPlaceable = true;
    
    public bool IsPlaceable { get   { return isPlaceable;}}

    private void OnMouseOver()
    {

        if (isPlaceable && Input.GetMouseButtonDown(0))
        {
            bool isPlaced = towers[1].CreateTower(towers[1],transform.position);
            isPlaceable = !isPlaced;
        }
    }
}
