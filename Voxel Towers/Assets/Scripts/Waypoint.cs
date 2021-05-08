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
    public TowerUI towerUI;
    public Mesh mesh;
    public Material material;


    public bool IsPlaceable { get   { return isPlaceable;}}
    private void Start()
    {
     towerUI = FindObjectOfType<TowerUI>(); 
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
        DrawHighLight();
        PlaceTower();
        
        
    }
    public void PlaceTower()
    {
        if (isPlaceable && Input.GetMouseButtonDown(0) && towerUI.ButtonChoice != -1)
        {
            bool isPlaced = towers[towerUI.ButtonChoice].CreateTower(towers[towerUI.ButtonChoice],transform.position);
            isPlaceable = !isPlaced;
        }
        Cursor.visible = true;
    }
    void DrawHighLight()
    {
        Vector3 scale =new Vector3( 1f,1.1f,1f);
        //Vector3 rangeIndicatorVector = new Vector3(range * rangeIndicatorMod, range * (rangeIndicatorMod * zDrawOffset), range * rangeIndicatorMod);
        Matrix4x4 trsMatrix = Matrix4x4.TRS(transform.position, Quaternion.identity, scale);
        Graphics.DrawMesh(mesh, trsMatrix, material, 1);
    }
}
