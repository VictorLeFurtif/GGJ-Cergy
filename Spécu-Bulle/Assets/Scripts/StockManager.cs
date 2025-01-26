using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class StockManager : MonoBehaviour
{
    // Variables
    public int actionValue;
    
    public float timer;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI actionText;
    [SerializeField] private TextMeshPro actionValueText;
    public GameObject GraphPoint;
    private List<GameObject> GraphPointsList = new List<GameObject>();
    public static StockManager instance;
    [SerializeField] private TextMeshProUGUI textobjectif;
    [SerializeField] private TextMeshProUGUI textday;

    public int level;
    [SerializeField] private List<int> levelContract;
    
    public int playerMoney;
    public int playerActions = 0;

    private Queue<int> lastActionsValues = new Queue<int>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        textobjectif.text = "Objectif : " + levelContract[level].ToString();
        textday.text = "Jour : " + level.ToString();
        moneyText.text = "MONEY : "+ playerMoney.ToString();
        actionText.text = "ACTION : " + playerActions.ToString();
        actionValueText.text = "Stock Value : " + actionValue.ToString() + " $";
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
            timer = 2.0f;
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

    public bool CheckContract()
    {
        if (playerMoney >= levelContract[level])
        {
            Debug.Log("jour suivant");
            playerMoney -= levelContract[level];
            level++;
            GameManager.instance.elapsedTime = 0;
            GameManager.instance.timeEnd = 120;
            GameManager.instance.hanger = 100;
            GameManager.instance.sleep = 100;
            return false;
        }
        else { return true; }
    }


    private void UpdateStock()
    {
        actionValue = Random.Range(0, 500);
        
        lastActionsValues.Enqueue(actionValue);
        if (lastActionsValues.Count > 10) lastActionsValues.Dequeue();
    }

    public void BuyStock()
    {
        if (playerMoney >= actionValue)
        {
            playerMoney -= actionValue;
            playerActions++;
        }
    }

    public void SellStock()
    {
        if (playerActions >= 1)
        {
            playerMoney += actionValue;
            playerActions--;
        }
    }
}
