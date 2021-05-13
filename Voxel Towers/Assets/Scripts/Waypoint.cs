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
    public Material yesMaterial;
    public Material noMaterial;


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
        DrawTowerRange();
        var menu = GameObject.FindGameObjectWithTag("Upgrade");
        if(menu==null || menu.activeInHierarchy == false)
        {
            PlaceTower();
        }
        
     
        
    }
    public void PlaceTower()
    {
        if (isPlaceable && Input.GetMouseButtonDown(0) && towerUI.ButtonChoice != -1 )
        {
            bool isPlaced = towers[towerUI.ButtonChoice].CreateTower(towers[towerUI.ButtonChoice],transform.position);
            isPlaceable = !isPlaced;
            towerUI.ButtonChoice = -1;
        }
        Cursor.visible = true;
    }
    void DrawHighLight()
    {
        Vector3 scale =new Vector3( 1f,1.1f,1f);
        //Vector3 rangeIndicatorVector = new Vector3(range * rangeIndicatorMod, range * (rangeIndicatorMod * zDrawOffset), range * rangeIndicatorMod);
        Matrix4x4 trsMatrix = Matrix4x4.TRS(transform.position, Quaternion.identity, scale);
        if (isPlaceable)
        {
            Graphics.DrawMesh(mesh, trsMatrix, yesMaterial, 1);

        }
        else
        {
            Graphics.DrawMesh(mesh, trsMatrix, noMaterial, 1);
        }
    }
    void DrawTowerMesh()
    {
    
    
    }
    void DrawTowerRange()
    {
        var towerChoice = towers[towerUI.ButtonChoice];
        float zDrawOffset = 0.75f;
        Vector3 rangeIndicatorVector = new Vector3(towerChoice.Range * towerChoice.RangeIndicatorMod, towerChoice.Range * (towerChoice.RangeIndicatorMod * zDrawOffset), towerChoice.Range * towerChoice.RangeIndicatorMod);
        Matrix4x4 trsMatrix = Matrix4x4.TRS(transform.position, Quaternion.identity, rangeIndicatorVector);

        Graphics.DrawMesh(towerChoice.mesh, trsMatrix, towerChoice.material, 1);

    }

}


