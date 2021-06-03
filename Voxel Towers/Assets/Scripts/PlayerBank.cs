using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerBank : MonoBehaviour
{
    [SerializeField] int startBalance = 150;
    [SerializeField] int currentBalance;        //change once UI in place
    public int CurrentBalance {get  { return currentBalance;} }
  
    public TextMeshProUGUI currencyText;
    public TextMeshProUGUI oreText;
    private void Awake()
    {
        currentBalance = startBalance;
        currencyText.text = "Currency: " + currentBalance;
    }


    public void Deposit(int amount) 
    {
        currentBalance += Mathf.Abs(amount);
        currencyText.text = "Currency: " + currentBalance;
    }

    public void Withdrawl(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        currencyText.text = "Currency: " + currentBalance;
    }
}
