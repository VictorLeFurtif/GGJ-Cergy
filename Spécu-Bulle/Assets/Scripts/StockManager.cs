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
    public int fluctuationValue;
    public int fluctuationAmount;
    public int errorMargin;
    public int explodingAmount;
    public int explodingRate;
    
    public float timer;
    
    public GameObject GraphPoint;
    public int playerMoney;
    public int playerActions = 0;
    
    private List<GameObject> GraphPointsList = new List<GameObject>();

    private Queue<int> lastActionsValues = new Queue<int>();


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
            fluctuationAmount = Random.Range(2*fluctuationValue, 7*fluctuationValue);
            explodingAmount = Random.Range(8*fluctuationValue, 15*fluctuationValue);
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
            GraphPointsList.Add(Instantiate(GraphPoint, new Vector2(i*5, actionList[i]/10), Quaternion.identity));
        }
    }

    private void UpdateStock()
    {
    
        if ((Random.Range(0, 100) <= fluctuationRate  && Random.Range(0, 100) >= errorMargin) || (Random.Range(0, 100) <= fluctuationRate && Random.Range(0, 100) <= errorMargin))
        {
            if (Random.Range(0, 100) <= explodingRate)
            {
                actionValues += fluctuationAmount+explodingAmount;
                fluctuationRate -= Random.Range(1,5);
            }
            actionValues += fluctuationAmount;
            fluctuationRate -= Random.Range(1,5);
        }
        else
        {
            if (Random.Range(0, 100) <= explodingRate)
            {
                actionValues -= fluctuationAmount+explodingAmount;
                fluctuationRate += Random.Range(5,15);
            }
            actionValues -= fluctuationAmount;
            fluctuationRate += Random.Range(8,20);
        }
        
        lastActionsValues.Enqueue(actionValues);
        if (lastActionsValues.Count > 10) lastActionsValues.Dequeue();
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
