using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public int cost = 50;
    // Start is called before the first frame update
    void Start()
    {
        Trap trap = this;       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool CreateTrap(Trap trap, Vector3 position)
    {
        PlayerBank bank = FindObjectOfType<PlayerBank>();
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        GridManager gridManager = FindObjectOfType<GridManager>();
        var offset = new Vector3(0, 1, 0);
        if (bank == null)
        {
            return false;
        }
        if (bank.CurrentBalance >= cost)
        {
            Instantiate(trap, position+offset, Quaternion.Euler(-90,0,0));
            bank.Withdrawl(cost);


            return true;
        }
        else
        {
            return false;
        }

    }
}
