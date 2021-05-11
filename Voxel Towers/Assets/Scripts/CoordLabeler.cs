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
    Waypoint waypoint;
    Color defColor = Color.white;
    Color blockedColor = Color.red;
    // Start is called before the first frame update
    void Awake()
    {
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        waypoint = GetComponentInParent<Waypoint>();
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
      // coords.x = Mathf.RoundToInt(transform.parent.position.x / EditorSnapSettings.move.x);
      // coords.y = Mathf.RoundToInt(transform.parent.position.z / EditorSnapSettings.move.z);
      // label.text = coords.x + "," + coords.y;
    }
    void UpdateName()
    {
        transform.parent.name = coords.ToString();
    }
    void DebugDisplay() 
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKey(KeyCode.LeftControl)&& Input.GetKeyDown(KeyCode.D))
        {
            label.enabled = !label.IsActive();
        }
        if (waypoint.IsPlaceable)
        {
            label.color = defColor;
        }
        else 
        {
            label.color = blockedColor;
        }

    }
}
