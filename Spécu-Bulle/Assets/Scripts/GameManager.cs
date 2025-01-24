using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float hanger;
    [SerializeField] private float sleep;
    [SerializeField] private float money;
    [SerializeField] private float timeStart;
    [SerializeField] private float timeEnd;
    [SerializeField] GameState gameState;
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject gameCanvas;
    [SerializeField] private List<GameObject> listOfCanvas;
    private GameManager instance;
    enum GameState
    {
        Menu,
        Game,
        GameOver
    }

    private void SetCanvas(GameObject canvas)
    {
        foreach (GameObject obj in listOfCanvas)
        {
            obj.SetActive(false);
        }
        canvas.SetActive(true);
    }

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
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        gameState = GameState.Menu;
    }

    private void Update()
    {
        switch (gameState)
        {
            case GameState.Menu : SetCanvas(menuCanvas);
                break;
            case GameState.GameOver : SetCanvas(gameOverCanvas);
                break;
            case GameState.Game : SetCanvas(gameCanvas);
                break;
        }
        
    }
}
