using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class StockManager : MonoBehaviour
{
    // Variables
    public int actionValues;
    public int fluctuationRate;
    public int fluctuationAmount;
    public float timer;
    public GameObject GraphPoint;
    public int playerMoney;
    public int playerActions = 0;
    
    private List<GameObject> GraphPointsList = new List<GameObject>(5);

    private Queue<int> lastActionsValues = new Queue<int>(5);
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();

        if (Input.GetKeyDown(KeyCode.B))
        {
            BuyStock();
            Debug.Log(playerMoney);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SellStock();
            Debug.Log(playerMoney);
        }
    }

    private void UpdateTimer()
    {
        if (timer <= 0)
        {
            timer = 1.0f;
            UpdateStock();
            UpdateGraph();
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    private void UpdateGraph()
    {
        List<int> actionList = new List<int>(5);
        foreach (var VARIABLE in lastActionsValues)
        {
            actionList.Add(VARIABLE);
        }

        if (GraphPointsList.Count != 0)
        {
            foreach (var VARIABLE in GraphPointsList)
            {
                Destroy(VARIABLE);
            }
            GraphPointsList.Clear();
        }
        
        
        for (int i = 0; i < actionList.Count; i++)
        {
            Debug.Log(actionList[i]);
            GraphPointsList.Add(Instantiate(GraphPoint, new Vector2(i*10, actionList[i]/10), Quaternion.identity));
        }
    }

    private void UpdateStock()
    {
    
        if (Random.Range(0, 100) <= fluctuationRate)
        {
            actionValues += fluctuationAmount;
            fluctuationRate -= Random.Range(1,5);
        }
        else
        {
            actionValues -= fluctuationAmount;
            fluctuationRate += Random.Range(2,8);
        }
        
        lastActionsValues.Enqueue(actionValues);
        if (lastActionsValues.Count > 5) lastActionsValues.Dequeue();
    }

    private void BuyStock()
    {
        if (playerMoney >= actionValues)
        {
            playerMoney -= actionValues;
            playerActions++;
        }
    }

    private void SellStock()
    {
        playerMoney += actionValues;
        playerActions--;
    }
}
