using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class StockManager : MonoBehaviour
{
    // Variables
    public int actionValues;
    public int fluctuationRate;
    public int fluctuationAmount;
    public float timer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        if (timer <= 0)
        {
            timer = 1.0f;
            UpdateStock();
            Debug.Log(actionValues);
        }
        else
        {
            timer -= Time.deltaTime;
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
    }
}
