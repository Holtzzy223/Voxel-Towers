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
       gridManager = FindObjectOfType<GridManager>();
       DisplayCoords();
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
   }
   void UpdateName()
   {
       transform.parent.name = coords.ToString();
   }
   void DebugDisplay() 
   {
        node = gridManager.GetNode(coords);
       if (Input.GetKeyDown(KeyCode.R))
       {
           SceneManager.LoadScene(SceneManager.GetActiveScene().name);
       }
       if (Input.GetKey(KeyCode.LeftControl)&& Input.GetKeyDown(KeyCode.D))
       {
           label.enabled = !label.IsActive();
       }
       if (node.isTraversable)
       {
           label.color = defColor;
       }
       if (node.isPath)
       {
            
            label.color = pathColor; 
       }
    //  if (node.isExplored)
    //  {
    //      // label.color = exploredColor;
    //  }
       if (!node.isTraversable)
       {
            label.color = blockedColor;
       }
 
   }
}
