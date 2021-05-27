using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUI : MonoBehaviour
{
    public int TowerChoice { get; set; }
    public int TrapChoice { get; set; }
    public int BuildingChoice { get; set; }
    public float TimeScale { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        TowerChoice = -1;
        TrapChoice = -1;
        BuildingChoice = -1;
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
        TimeScale = Time.timeScale;
        Time.timeScale = 0;
    }
    public void UnPauseGame()
    {
        Time.timeScale = TimeScale;
    }


}
