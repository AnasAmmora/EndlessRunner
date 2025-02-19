using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } 
    [HideInInspector] public bool isPaused { get; private set; }
    [HideInInspector] public bool isStarted { get; private set; }

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI timerUI;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private TextMeshProUGUI bestScore;
    [SerializeField] private TextMeshProUGUI LastScore;



    private float timer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    void Start()
    {
        isPaused = false;
        isStarted = false;
        UpdateUI();
    }

    void Update()
    {
        if (isStarted)
        {
            FunctionName();
        }
    }

    private void FunctionName()
    {
        timer += 1 * Time.deltaTime;
        timerUI.text = timer + "";
    }

    public void StartGame()
    {
        isStarted = true;
        startPanel.SetActive(false);
    }

    public void EndGame()
    {
        SaveData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetPauseState(bool pauseState)
    {
        isPaused = pauseState;

        if (pauseState)
        {
            Time.timeScale = 0f; 
            Debug.Log("Game is paused.");
        }
        else
        {
            Time.timeScale = 1f;
            Debug.Log("Game is resumed.");
        }
    }

    private void SaveData()
    {
        GameData.Instance.LastScore = timer;
        if(GameData.Instance.LastScore >= GameData.Instance.BestScore)
        {
            GameData.Instance.BestScore = GameData.Instance.LastScore;
        }

        //Save Coine Data
    }
    private void UpdateUI()
    {
        startPanel.SetActive(true);
        bestScore.text = "Best Score\n" + GameData.Instance.BestScore;
        LastScore.text = "Last Score\n" + GameData.Instance.LastScore;
    }
}
