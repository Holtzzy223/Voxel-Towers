using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    public float oldTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        PauseGame();
    }
    private void OnDisable()
    {
        UnPauseGame();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void PauseGame()
    {
        oldTime = Time.timeScale;
        Time.timeScale = 0;
    }
    public void UnPauseGame()
    {
        if (oldTime > 0.25f)
        {
            Time.timeScale = oldTime;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
