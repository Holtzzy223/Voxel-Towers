using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] int maxHP = 200;
    [SerializeField] int currentHP;
    int CurrentHP { get { return currentHP; } }
   

    // Start is called before the first frame update
    void Awake()
    {
        
        currentHP = maxHP;
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
        }
    }
    public void AddHP(int amount)
    {
        currentHP += Mathf.Abs(amount);
    }

}
