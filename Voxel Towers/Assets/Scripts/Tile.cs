using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    
    [SerializeField] Tower[] towers;
    GridManager gridManager;
    Pathfinder pathfinder;
    public Vector2Int coords = new Vector2Int();
    public bool isPlaceable = true;
    public GameObject grassMesh;
    public GameObject pathMesh;
    public GameObject path90Mesh;
    public TowerUI towerUI;
    
    public Mesh mesh;
    public Material yesMaterial;
    public Material noMaterial;


    public bool IsPlaceable { get   { return isPlaceable;}}

    private void Awake()
    {

        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    private void Start()
    {
        towerUI = FindObjectOfType<TowerUI>();
        coords = gridManager.GetCoordsFromPos(transform.position);
       // if (coords == pathfinder.StartCoords)
       // {
       //     isPlaceable = false;
       //
       // }
        
     ///  GameObject parentObject = GetComponentInParent<GameObject>();
     ///  if (parentObject != null)
     ///  {
     ///      if (parentObject.name.ToLower() == "path")
     ///      { 
     ///          
     ///      }
     ///  }
    }
    private void Update()
    {
        if (pathfinder.WillBlockPath(coords))
        {
            isPlaceable = false;
        }
        if (pathfinder.IsInPath(coords))
        {
            grassMesh.gameObject.SetActive(false);
            pathMesh.gameObject.SetActive(true);
        }


    }

    private void OnMouseOver()
    {
        var menu = GameObject.FindGameObjectWithTag("Upgrade");
        DrawHighLight();
        if (towerUI.ButtonChoice != -1 && !pathfinder.WillBlockPath(coords))
        {
            DrawTowerMesh();
            DrawTowerRange();
            if ((menu == null || menu.activeInHierarchy == false) )
            {
                if (Input.GetMouseButtonDown(0))
                {
                    PlaceTower();
                }
            }
        }
       

        
     
        
    }
    public void PlaceTower()
    {
        if (gridManager.GetNode(coords).isTraversable && towerUI.ButtonChoice != -1)
        {
            if (!pathfinder.WillBlockPath(coords))
            {
                bool isPlaced = towers[towerUI.ButtonChoice].CreateTower(towers[towerUI.ButtonChoice], transform.position);
          
                isPlaceable = !isPlaced;
                if (isPlaced)
                {
                    gridManager.BlockNode(coords);
                    pathfinder.NotifyRecievers();
                }
                towerUI.ButtonChoice = -1;
            } 
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
        var scale = new Vector3(1, 1, 1);
        var towerChoice = towers[towerUI.ButtonChoice];
        Matrix4x4 trsMatrix = Matrix4x4.TRS(transform.position, Quaternion.identity, scale);

        Graphics.DrawMesh(towerChoice.hilightMesh, trsMatrix, towerChoice.material, 1);
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


