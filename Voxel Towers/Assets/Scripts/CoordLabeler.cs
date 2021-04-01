using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class CoordLabeler : MonoBehaviour
{
    TextMeshPro label;
    Vector2Int coords = new Vector2Int();

    // Start is called before the first frame update
    void Awake()
    {
        label = GetComponent<TextMeshPro>();
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
    }

    void DisplayCoords() 
    {
        coords.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coords.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);
        label.text = coords.x + "," + coords.y;
    }
    void UpdateName()
    {
        transform.parent.name = coords.ToString();
    }
}
