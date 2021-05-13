using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUI : MonoBehaviour
{
    public int ButtonChoice { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        ButtonChoice = -1;
        PauseGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CursorChange()
    {
        Cursor.visible = false;

    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void UnPauseGame()
    {
        Time.timeScale = 1;
    }


}
