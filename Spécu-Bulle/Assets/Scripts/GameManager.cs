using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float hanger;
    [SerializeField] private float sleep;
    [SerializeField] private float money;
    [SerializeField] private float timeEnd;
    public GameStateCanva gameState;
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject gameCanvas;
    [SerializeField] private List<GameObject> listOfCanvas;
    [SerializeField] private TextMeshProUGUI timeText;
    float elapsedTime;
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

    public void StartGame()
    {
        gameState = GameStateCanva.Game;
        elapsedTime = 0;
    }

    public void EndGame()
    {
        Application.Quit();
    }
    private void Start()
    {
        gameState = GameStateCanva.Menu;
    }

    private bool CheckIfTimeEnd()
    {
        if (elapsedTime - timeEnd > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ReloadCurrentScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
    private void Update()
    {
        if (CheckIfTimeEnd())
        {
            gameState = GameStateCanva.GameOver;
        }
        
        elapsedTime += Time.deltaTime;
        timeText.text = "Timer: " + elapsedTime.ToString("0.00");
        
        switch (gameState)
        {
            case GameStateCanva.Menu : SetCanvas(menuCanvas);
                PlayerCOntroller.instance.enabled = false;
                break;
            case GameStateCanva.GameOver : SetCanvas(gameOverCanvas);
                PlayerCOntroller.instance.enabled = false;
                break;
            case GameStateCanva.Game : SetCanvas(gameCanvas);
                PlayerCOntroller.instance.enabled = true;
                break;
        }
        
    }
}
