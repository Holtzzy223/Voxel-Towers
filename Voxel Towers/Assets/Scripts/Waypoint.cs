using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    
    [SerializeField] Tower[] towers;
    public bool isPlaceable = true;
    public GameObject grassMesh;
    public GameObject pathMesh;
    public GameObject path90Mesh;
    
    public bool IsPlaceable { get   { return isPlaceable;}}
    private void Start()
    {
     ///  GameObject parentObject = GetComponentInParent<GameObject>();
     ///  if (parentObject != null)
     ///  {
     ///      if (parentObject.name.ToLower() == "path")
     ///      { 
     ///          
     ///      }
     ///  }
    }
    private void OnMouseOver()
    {

        if (isPlaceable && Input.GetMouseButtonDown(0))
        {
            bool isPlaced = towers[1].CreateTower(towers[1],transform.position);
            isPlaceable = !isPlaced;
        }
    }
}
