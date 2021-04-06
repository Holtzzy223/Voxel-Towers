using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBank : MonoBehaviour
{
    [SerializeField] int startBalance = 150;
    [SerializeField] int currentBalance;        //change once UI in place
    public int CurrentBalance {get  { return currentBalance;} }

    private void Awake()
    {
        currentBalance = startBalance;
    }

   public void Deposit(int amount) 
    {
        currentBalance += Mathf.Abs(amount);
    }

    public void Withdrawl(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
    }
}
