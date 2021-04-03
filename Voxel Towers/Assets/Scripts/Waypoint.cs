using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] bool isPlaceable;

    // Start is called before the first frame update

    private void OnMouseOver()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(transform.name);
        }
    }
}
