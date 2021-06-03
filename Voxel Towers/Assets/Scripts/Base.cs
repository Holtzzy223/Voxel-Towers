using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Base : MonoBehaviour
{
    [SerializeField] int maxHP = 200;
    public int MaxHP {get {return maxHP;}}
    
    [SerializeField] int currentHP;
    public int CurrentHP { get { return currentHP; } }
    public Slider healthBar;
    
    
    public ParticleSystem healthParticles;
    ParticleSystem.MainModule main;
    
    // Start is called before the first frame update
    void Awake()
    {
        currentHP = maxHP;
        healthBar.maxValue = maxHP;
        healthBar.value = healthBar.maxValue;


     
    }

    private void Update()
    {
       // UpdateHeathBar();



    }

    public void RemoveHP(int amount)
    {
        Debug.LogError("Remove Health");
        currentHP -= Mathf.Abs(amount);
        UpdateHeathBar();
        if (currentHP <= 0)
        {
            
            /* Handle Base Death
             * Play Death Anims/Particles/VFX
             * Pop Level Stats UI
             * Pop Option to restart Level Quit to main?
             */
            Destroy(gameObject);
            //Reload Scene Temp
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            
        }
    }
    public void AddHP(int amount)
    {
        currentHP += Mathf.Abs(amount);
        UpdateHeathBar();
    }
    private void UpdateHeathBar()
    {
        

        Debug.Log("Should change health bar");
        healthBar.value = currentHP;
       
        


    }
}
