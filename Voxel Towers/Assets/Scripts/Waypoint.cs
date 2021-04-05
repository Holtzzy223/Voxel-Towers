using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    
    [SerializeField] GameObject Tower;
    public bool isPlaceable = true;
    
    public bool IsPlaceable { get   { return isPlaceable;}}

    private void OnMouseOver()
    {

        if (isPlaceable && Input.GetMouseButtonDown(0))
        {
            Instantiate(Tower,transform.position,Quaternion.identity);
            isPlaceable = false;
        }
    }
}
