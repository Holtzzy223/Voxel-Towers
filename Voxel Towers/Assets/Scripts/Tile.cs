using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    [SerializeField] Tower[] towers;
    [SerializeField] Trap[] traps;
    [SerializeField] Building[] buildings;
    GridManager gridManager;
    Pathfinder pathfinder;
    public Vector2Int coords = new Vector2Int();
    public bool isPlaceable = true;
    public GameObject grassMesh;
    public GameObject pathMesh;
    public GameObject path90Mesh;
    public GameObject playerBase;
    public WavePool wavePool;
    public TowerUI towerUI;

    public Mesh mesh;
    public Material yesMaterial;
    public Material noMaterial;


    public bool IsPlaceable { get { return isPlaceable; } }
    public bool basePlaced = false;
    public bool poolPlaced = false;
    private void Awake()
    {

        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    private void Start()
    {

        towerUI = FindObjectOfType<TowerUI>();
        coords = gridManager.GetCoordsFromPos(transform.position);
        //if (pathfinder.IsInPath(coords))
        //{
        //    grassMesh.gameObject.SetActive(false);
        //    pathMesh.gameObject.SetActive(true);
        //
        //}

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
    private void FixedUpdate()
    {
      //  if (pathfinder.WillBlockPath(this.coords))
      //  {
      //      isPlaceable = false;
      //  }
        if (coords == pathfinder.DestinationCoords)
        {
            if (!basePlaced)
            {
                Instantiate(playerBase, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                basePlaced = true;
            }
        }
        
        
        


    }

    private void OnMouseOver()
    {
        var menu = GameObject.FindGameObjectWithTag("Upgrade");
        DrawHighLight();
        if (towerUI.TowerChoice != -1 && !pathfinder.WillBlockPath(coords))
        {
            DrawTowerMesh();
            DrawTowerRange();
            if ((menu == null || menu.activeInHierarchy == false))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    PlaceTower();

                }
            }
        }


        if (towerUI.TrapChoice != -1)
        {
            if ((menu == null || menu.activeInHierarchy == false))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    PlaceTrap();

                }
            }

        }

        if (towerUI.BuildingChoice != -1)
        {
            if ((menu == null || menu.activeInHierarchy == false))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    PlaceBuilding();

                }
            }

        }
    }
        void PlaceBuilding()
        {
            if (isPlaceable)
            {
                if (gridManager.GetNode(coords).isTraversable && towerUI.BuildingChoice != -1)
                {
                    if (coords != pathfinder.StartCoords && coords != pathfinder.DestinationCoords)
                    {
                        bool isPlaced = buildings[towerUI.BuildingChoice].CreateBuilding(buildings[towerUI.BuildingChoice], transform.position);

                        isPlaceable = !isPlaced;
                        if (isPlaced && !buildings[towerUI.BuildingChoice].isRefinery)
                        {
                            gridManager.BlockNode(coords);
                            if (pathfinder.IsInPath(coords))
                            {
                                grassMesh.gameObject.SetActive(false);
                                pathMesh.gameObject.SetActive(true);

                            }
                            pathfinder.NotifyRecievers();

                        }
                        towerUI.BuildingChoice = -1;
                    }
                }
            }
            Cursor.visible = true;
        }

        void PlaceTrap()
        {
            if (isPlaceable)
            {
            if (gridManager.GetNode(coords).isTraversable && towerUI.TrapChoice != -1)
            {
                if (traps[towerUI.TrapChoice].isWall && !pathfinder.WillBlockPath(coords))
                {
                    if (coords != pathfinder.StartCoords && coords != pathfinder.DestinationCoords)
                    {
                        bool isPlaced = traps[towerUI.TrapChoice].CreateTrap(traps[towerUI.TrapChoice], transform.position);

                        isPlaceable = !isPlaced;
                        if (isPlaced && traps[towerUI.TrapChoice].isWall)
                        {
                            gridManager.BlockNode(coords);
                            if (pathfinder.IsInPath(coords))
                            {
                                grassMesh.gameObject.SetActive(false);
                                pathMesh.gameObject.SetActive(true);

                            }
                            pathfinder.NotifyRecievers();

                        }
                        towerUI.TrapChoice = -1;
                    }
                }
                if(!traps[towerUI.TrapChoice].isWall)
                {
                    if (coords != pathfinder.StartCoords && coords != pathfinder.DestinationCoords)
                    {
                        bool isPlaced = traps[towerUI.TrapChoice].CreateTrap(traps[towerUI.TrapChoice], transform.position);

                        isPlaceable = !isPlaced;
                        if (isPlaced && traps[towerUI.TrapChoice].isWall)
                        {
                            gridManager.BlockNode(coords);
                            if (pathfinder.IsInPath(coords))
                            {
                                grassMesh.gameObject.SetActive(false);
                                pathMesh.gameObject.SetActive(true);

                            }
                            pathfinder.NotifyRecievers();

                        }
                        towerUI.TrapChoice = -1;
                    }
                }
            }
            }
            Cursor.visible = true;
        }

        void PlaceTower()
        {
            if (gridManager.GetNode(coords).isTraversable && towerUI.TowerChoice != -1)
            {
                if (!pathfinder.WillBlockPath(coords)&&isPlaceable)
                {
                    bool isPlaced = towers[towerUI.TowerChoice].CreateTower(towers[towerUI.TowerChoice], transform.position);

                    isPlaceable = !isPlaced;
                    if (isPlaced)
                    {
                        gridManager.BlockNode(coords);
                        if (pathfinder.IsInPath(coords))
                        {
                            grassMesh.gameObject.SetActive(false);
                            pathMesh.gameObject.SetActive(true);

                        }
                        pathfinder.NotifyRecievers();
                    }
                    towerUI.TowerChoice = -1;
                }
            }
            Cursor.visible = true;
        }
        void DrawHighLight()
        {
            Vector3 scale = new Vector3(1f, 1.1f, 1f);
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
            var towerChoice = towers[towerUI.TowerChoice];
            Matrix4x4 trsMatrix = Matrix4x4.TRS(transform.position, Quaternion.identity, scale);

            Graphics.DrawMesh(towerChoice.hilightMesh, trsMatrix, towerChoice.material, 1);
        }
        void DrawTowerRange()
        {
            var towerChoice = towers[towerUI.TowerChoice];
            float zDrawOffset = 0.75f;
            Vector3 rangeIndicatorVector = new Vector3(towerChoice.Range * towerChoice.RangeIndicatorMod, towerChoice.Range * (towerChoice.RangeIndicatorMod * zDrawOffset), towerChoice.Range * towerChoice.RangeIndicatorMod);
            Matrix4x4 trsMatrix = Matrix4x4.TRS(transform.position, Quaternion.identity, rangeIndicatorVector);

            Graphics.DrawMesh(towerChoice.mesh, trsMatrix, towerChoice.material, 1);

        }

}


