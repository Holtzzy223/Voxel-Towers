using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Base : MonoBehaviour
{
    [SerializeField] int maxHP = 200;
    [SerializeField] int currentHP;
    
    int CurrentHP { get { return currentHP; } }
    public ParticleSystem healthParticles;
    ParticleSystem.MainModule main;
    // Start is called before the first frame update
    void Awake()
    {
 

        currentHP = maxHP;
        healthParticles = GetComponentInChildren<ParticleSystem>();
        main = healthParticles.main;
        main.startColor = Color.green;
        if (healthParticles == null)
        {
            Debug.Log("healthparticles is null");
        }

    }

    private void Update()
    {

    }

    public void RemoveHP(int amount)
    {
        currentHP -= Mathf.Abs(amount);
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
    }

}