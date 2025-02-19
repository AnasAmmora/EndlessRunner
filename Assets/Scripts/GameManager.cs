using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } 
    public bool isPaused { get; private set; }
    public bool isStarted { get; private set; }

    [SerializeField] GameObject StartGameButton;

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

    private void Start()
    {
        isPaused = false;
        isStarted = false;
    }

    public void StartGame()
    {
        isStarted = true;
        StartGameButton.SetActive(false);
    }

    public void EndGame()
    {
        //isPaused = false;
        //isStarted = false;
        //StartGameButton.SetActive(true);
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

}
