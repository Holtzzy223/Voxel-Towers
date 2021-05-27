using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Building : MonoBehaviour
{
    public bool isRefinery = false;
    public int cost;
    public float chargeTime;
    public float refineTime;
    public float refineAmount;
    public GameObject UI;
    public Button[] buttons;
    // Start is called before the first frame update
    void Start()
    {
        if (isRefinery)
        {
            InvokeRepeating("Refinery",chargeTime,refineTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool CreateBuilding(Building building, Vector3 position)
    {
        PlayerBank bank = FindObjectOfType<PlayerBank>();
        var offset = new Vector3(0, 1, 0);
        if (bank == null)
        {
            return false;
        }
        if (bank.CurrentBalance >= cost)
        {
   
            offset = new Vector3(0, 3, 0);
            Instantiate(building, position + offset, Quaternion.identity);
          
            bank.Withdrawl(cost);


            return true;
        }
        else
        {
            return false;
        }

    }
    public void Refinery()
    {
        FindObjectOfType<Base>().dataOreAmt += refineAmount;
    }

    public void ScienceUI()
    {

    }
    public void ResearchTower(int choice)
    {
        var researchCost = 100;
        var dataOreAmt = FindObjectOfType<Base>().dataOreAmt;
        switch (choice)
        {
            case 0:
               
                if (dataOreAmt >= researchCost)
                {
                    buttons[choice].gameObject.SetActive(true);
                    FindObjectOfType<Base>().dataOreAmt -= researchCost;
                }
                break;
            case 1:
                researchCost =Mathf.FloorToInt(researchCost* 2.5f);
                if (dataOreAmt >= researchCost)
                {
                    buttons[choice].gameObject.SetActive(true);
                    FindObjectOfType<Base>().dataOreAmt -= researchCost;
                }
                break;
            case 2:
                researchCost = Mathf.FloorToInt(researchCost * 3.5f);
                if (dataOreAmt >= researchCost)
                {
                    buttons[choice].gameObject.SetActive(true);
                    FindObjectOfType<Base>().dataOreAmt -= researchCost;
                }
                break;
            case 3:
                researchCost = Mathf.FloorToInt(researchCost * 5.5f);
                if (dataOreAmt >= researchCost)
                {
                    buttons[choice].gameObject.SetActive(true);
                    FindObjectOfType<Base>().dataOreAmt -= researchCost;
                }
                break;
            case 4:
                researchCost = Mathf.FloorToInt(researchCost * 7.5f);
                if (dataOreAmt >= researchCost)
                {
                    buttons[choice].gameObject.SetActive(true);
                    FindObjectOfType<Base>().dataOreAmt -= researchCost;
                }
                break;
            case 5:
                researchCost = Mathf.FloorToInt(researchCost * 10f);
                if (dataOreAmt >= researchCost)
                {
                    buttons[choice].gameObject.SetActive(true);
                    FindObjectOfType<Base>().dataOreAmt -= researchCost;
                }
                break;
        }
        
    }
    public void ResearchPerk(int choice)
    {
        switch (choice)
        {
            case 0:

                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
        }
    }

    private void OnDestroy()
    {
        CancelInvoke();
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UI.SetActive(true);
        }
        if (Input.GetMouseButtonDown(1))
        {
            UI.SetActive(false);
        }
    }
}
