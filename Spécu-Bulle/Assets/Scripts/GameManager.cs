using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Parameters")]
    public float hanger;
    public float sleep;
    public float hangerStock;
    public float sleepStock;
    public float timeEnd;
    public GameStateCanva gameState;
    [Header("SLider")] [SerializeField] private Slider sliderHunger;
    [SerializeField] private Slider sliderSleep;
    [Header("Canvas")]
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject gameCanvas;
    [SerializeField] private List<GameObject> listOfCanvas;
    [Header("Time link")]
    [SerializeField] private TextMeshProUGUI timeText;
    public float elapsedTime;

    [Header("Less Hunger & Sleep")] 
    [SerializeField] private float hungerDecrementationValue = 0.1f;
    [SerializeField] private float sleepDecrementationValue = 0.07f;
    [SerializeField] private GameObject winCanvas;
    public static GameManager instance;
    public enum GameStateCanva
    {
        Menu,
        Game,
        GameOver,
        Win
    }
    
    
    
    private void SetCanvas(GameObject canvas)
    {
        foreach (GameObject obj in listOfCanvas)
        {
            obj.SetActive(false);
        }
        canvas.SetActive(true);
    }

    private void UpdateValuesOfParameters()
    {
        if (gameState != GameStateCanva.Game) return;
        sleep -= sleepDecrementationValue;
        hanger -= hungerDecrementationValue;
    }
    
    private void UpdatingSliders()
    {
        sliderHunger.value = hanger;
        sliderSleep.value = sleep;
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
    }

    public void StartMenu()
    {
        gameState = GameStateCanva.Menu;
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
        InvokeRepeating(nameof(UpdateValuesOfParameters),1f,1f);
        gameState = GameStateCanva.Menu;
        hangerStock = hanger;
        sleepStock = sleep;
    }

    private bool CheckIfTimeEnd()
    {
        if (elapsedTime - timeEnd > 0 && gameState == GameStateCanva.Game)
        {
            Debug.Log("Check fin");
            return StockManager.instance.CheckContract();
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
        if (sleep < sleepStock/2 || hanger < hangerStock/2)
        {
            PlayerCOntroller.instance.moveSpeed = PlayerCOntroller.instance.moveSpeedReduce;
        }
        else
        {
            PlayerCOntroller.instance.moveSpeed = PlayerCOntroller.instance.moveSpeedStock;
        }
        UpdatingSliders(); //update sliders to refresh value
        
        if (CheckIfTimeEnd() || sleep <= 0 || hanger <= 0)
        {
            gameState = GameStateCanva.GameOver;
        }
        
        elapsedTime += Time.deltaTime;
        timeText.text = "Timer: " + (timeEnd - elapsedTime).ToString("0.00");
        
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
            case GameStateCanva.Win : SetCanvas(winCanvas);
                PlayerCOntroller.instance.enabled = enabled;
                break;
        }
        
    }
}
