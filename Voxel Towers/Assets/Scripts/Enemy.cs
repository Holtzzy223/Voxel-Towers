using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   public int killReward = 25;
   public int damageToBase = 10;

    PlayerBank bank;
    Base playerBase;
    
    // Start is called before the first frame update
    void Awake()
    {
      
        bank = FindObjectOfType<PlayerBank>();
        playerBase = FindObjectOfType<Base>();
       
    }


    public void RewardCurrency() 
    {
        if (bank == null){ return; }
        bank.Deposit(killReward);
    }

    public void DamageBase() 
    {
        if (playerBase == null)
        {
            Debug.LogAssertion("BASE IS NULL");
            return;
        }
        else
        {
            playerBase.RemoveHP(damageToBase);
        }
    }


}
