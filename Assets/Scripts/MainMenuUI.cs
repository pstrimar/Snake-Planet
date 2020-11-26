using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public event Action onPlay;
    public GameObject postProcessing;
    public GameObject snake;
    public GameObject finalScoreText;
    public GameObject currentScoreText;
    public GameObject bestScoreText;
    public GameObject mainMenuUI;
    public GameObject titleText;
    public GameObject gameOverText;

    public CinemachineVirtualCamera mainVCam;
    public CinemachineVirtualCamera introVCam;
    public SnakeMovement movement;
    public Button pauseButton;

    private bool isPaused;

    private void Awake()
    {
        #if UNITY_ANDROID || UNITY_IOS
        postProcessing.SetActive(false);
        #endif
    }

    private void Start()
    {
        currentScoreText.SetActive(false);
        bestScoreText.SetActive(false);
    }

    private void OnEnable()
    {
        if (movement != null)
        {
            movement.onGameOver += HandleGameOver;
        }
    }

    private void OnDisable()
    {
        if (movement != null)
        {
            movement.onGameOver -= HandleGameOver;
        }
    }

    public void Play()
    {
        snake.GetComponent<Grow>().enabled = true;
        titleText.SetActive(false);
        mainMenuUI.SetActive(false);
        currentScoreText.SetActive(true);
        bestScoreText.SetActive(true);
        mainVCam.Priority = 20;
        introVCam.enabled = false;
        pauseButton.gameObject.SetActive(true);

        onPlay?.Invoke();
    }

    public void Pause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            introVCam.enabled = true;
            snake.GetComponent<Grow>().enabled = false;
            movement.enabled = false;
            mainVCam.Priority = 10;
            pauseButton.GetComponentInChildren<Text>().text = "►";
        } 
        else
        {
            introVCam.enabled = false;
            snake.GetComponent<Grow>().enabled = true;
            movement.enabled = true;
            mainVCam.Priority = 20;
            pauseButton.GetComponentInChildren<Text>().text = "||";
        }
        
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    private void HandleGameOver()
    {
        introVCam.enabled = true;
        mainVCam.Priority = 10;
        mainMenuUI.SetActive(true);
        gameOverText.SetActive(true);
        finalScoreText.SetActive(true);
        currentScoreText.SetActive(false);
        bestScoreText.SetActive(false);
        pauseButton.gameObject.SetActive(false);
        finalScoreText.GetComponent<Text>().text = "Your score was: " + GetComponent<Score>().finalScore.ToString();
    }
}
