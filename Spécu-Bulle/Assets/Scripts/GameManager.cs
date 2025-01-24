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
    public GameStateCanva gameState;
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject gameCanvas;
    [SerializeField] private List<GameObject> listOfCanvas;
    public static GameManager instance;
    public enum GameStateCanva
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

    public void ChangeGameState(GameStateCanva state)
    {
        gameState = state;
    }
     
    private void Start()
    {
        gameState = GameStateCanva.Menu;
    }

    private void Update()
    {
        switch (gameState)
        {
            case GameStateCanva.Menu : SetCanvas(menuCanvas);
                break;
            case GameStateCanva.GameOver : SetCanvas(gameOverCanvas);
                break;
            case GameStateCanva.Game : SetCanvas(gameCanvas);
                break;
        }
        
    }
}
