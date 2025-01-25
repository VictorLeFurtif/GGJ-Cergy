using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class StockManager : MonoBehaviour
{
    // Variables
    public int actionValue;
    
    public float timer;
    
    public GameObject GraphPoint;
    private List<GameObject> GraphPointsList = new List<GameObject>();
    
    public int playerMoney;
    public int playerActions = 0;

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

        // Supprime les anciens points du graphique
        if (GraphPointsList.Count != 0)
        {
            foreach (var VARIABLE in GraphPointsList)
            {
                Destroy(VARIABLE);
            }
            GraphPointsList.Clear();
        }
    
        // Crée les nouveaux points et les lignes
        for (int i = 0; i < actionList.Count; i++)
        {
            // Instancie un nouveau point
            var newPoint = Instantiate(GraphPoint, new Vector2(i * 5 + 10, actionList[i] / 10 + 10), Quaternion.identity);
            GraphPointsList.Add(newPoint);

            // Relie ce point au précédent s'il existe
            if (i > 0)
            {
                LineRenderer lineRenderer = newPoint.GetComponent<LineRenderer>();
                if (lineRenderer != null)
                {
                    lineRenderer.positionCount = 2;
                    lineRenderer.SetPosition(0, GraphPointsList[i - 1].transform.position); // Point précédent
                    lineRenderer.SetPosition(1, newPoint.transform.position);             // Point actuel
                }
            }
        }
    }


    private void UpdateStock()
    {
        actionValue = Random.Range(0, 500);
        
        lastActionsValues.Enqueue(actionValue);
        if (lastActionsValues.Count > 10) lastActionsValues.Dequeue();
    }

    private void BuyStock()
    {
        if (playerMoney >= actionValue)
        {
            playerMoney -= actionValue;
            playerActions++;
        }
    }

    private void SellStock()
    {
        if (playerActions >= 1)
        {
            playerMoney += actionValue;
            playerActions--;
        }
    }
}
