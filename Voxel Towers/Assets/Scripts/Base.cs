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
    TesterFunctions debugUI;
    // Start is called before the first frame update
    void Awake()
    {
        healthBar.maxValue = maxHP;
        healthBar.value = healthBar.maxValue;

        currentHP = maxHP;
      // healthParticles = GetComponentInChildren<ParticleSystem>();
      // main = healthParticles.main;
      // main.startColor = Color.green;
        if (healthParticles == null)
        {
            Debug.Log("healthparticles is null");
        }
        debugUI = FindObjectOfType<TesterFunctions>();
        debugUI.gameObject.SetActive(false);
    }

    private void Update()
    {
        UpdateHeathBar();
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
       if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.D))
       
          if (debugUI.gameObject.activeInHierarchy == true)
          {
              debugUI.gameObject.SetActive(false);
          }
          else
          {
              debugUI.gameObject.SetActive(true);
          }
       
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
