using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public static event Action onPlay;
    public GameObject postProcessing;
    public Grow grow;
    public GameObject finalScoreText;
    public GameObject GamePlayUI;
    public GameObject mainMenuUI;
    public GameObject titleText;
    public GameObject gameOverText;

    public CinemachineVirtualCamera mainVCam;
    public CinemachineVirtualCamera introVCam;
    public SnakeMovement movement;
    public Text pauseButtonText;

    private bool isPaused;

    private void Awake()
    {
        #if UNITY_ANDROID || UNITY_IOS
        postProcessing.SetActive(false);
        #endif
    }

    private void Start()
    {
        GamePlayUI.SetActive(false);
    }

    private void OnEnable()
    {
        SnakeMovement.onGameOver += HandleGameOver;
    }

    private void OnDisable()
    {
        SnakeMovement.onGameOver -= HandleGameOver;
    }

    // Switch to main camera, hide main menu UI, show gameplay UI, and enable movement/growth of snake
    public void Play()
    {
        grow.enabled = true;
        titleText.SetActive(false);
        mainMenuUI.SetActive(false);
        GamePlayUI.SetActive(true);
        mainVCam.Priority = 20;
        introVCam.enabled = false;

        onPlay?.Invoke();
    }

    public void Pause()
    {
        isPaused = !isPaused;

        // Switch cameras and stop movement/growth of snake
        if (isPaused)
        {
            introVCam.enabled = true;
            grow.enabled = false;
            movement.enabled = false;
            mainVCam.Priority = 10;
            pauseButtonText.text = "►";
        } 
        // Switch cameras and enable movement/growth of snake
        else
        {
            introVCam.enabled = false;
            grow.enabled = true;
            movement.enabled = true;
            mainVCam.Priority = 20;
            pauseButtonText.text = "||";
        }
        
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    private void HandleGameOver()
    {
        // Smooth transition to orbiting camera
        introVCam.enabled = true;
        mainVCam.Priority = 10;

        // Show Game Over UI
        mainMenuUI.SetActive(true);
        gameOverText.SetActive(true);
        finalScoreText.SetActive(true);
        GamePlayUI.SetActive(false);
        finalScoreText.GetComponent<Text>().text = "Your score was: " + GetComponent<Score>().finalScore.ToString();
    }
}
