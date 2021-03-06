using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;

[ExecuteAlways]
public class CoordLabeler : MonoBehaviour
{
    TextMeshPro label;
    Vector2Int coords = new Vector2Int();
    Tile waypoint;
    Node node;
    GridManager gridManager;
    Color defColor = Color.white;
    Color pathColor = Color.green;
    Color exploredColor = Color.blue;
    Color blockedColor = Color.red;
    // Start is called before the first frame update
    void Awake()
    {
       label = GetComponent<TextMeshPro>();
       label.enabled = false;
       waypoint = GetComponentInParent<Tile>();
      
       DisplayCoords();
       
    }
    private void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
       
    }
    // Update is called once per frame
    void Update()
    {
       if (!Application.isPlaying)
       {
           DisplayCoords();
           UpdateName();
       }
        DebugDisplay();
    } 

   void DisplayCoords() 
   {
       //Comment these out before build
     coords.x = Mathf.RoundToInt(transform.parent.position.x /FindObjectOfType<GridManager>().sceneGridSize);
     coords.y = Mathf.RoundToInt(transform.parent.position.z /FindObjectOfType<GridManager>().sceneGridSize);
     label.text = coords.x + "," + coords.y;
     //label.enabled = !label.IsActive();
    }
   void UpdateName()
   {
       transform.parent.name = coords.ToString();
   }
   public void DebugDisplay() 
   {
        node = gridManager.GetNode(coords);
        
        if (node != null)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
          // if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.G))
          // {
                
           // }
            if (node.isTraversable)
            {
                label.color = defColor;
            }
            if (node.isPath)
            {

                label.color = pathColor;
                waypoint.grassMesh.SetActive(false);
                waypoint.pathMesh.SetActive(true);
            }
            if (!node.isTraversable)
            {
                label.color = blockedColor;
            }
            if (!node.isPath)
            {
                waypoint.pathMesh.SetActive(false);
                waypoint.grassMesh.SetActive(true);
            }
        }
    //  if (node.isExplored)
    //  {
    //      // label.color = exploredColor;
    //  }
      
 
   }
}
