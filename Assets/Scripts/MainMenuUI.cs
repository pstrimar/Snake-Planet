using Cinemachine;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public static event Action onPlay;
    public GameObject PostProcessing;
    public Grow Grow;
    public GameObject FinalScoreText;
    public GameObject GamePlayUI;
    public GameObject mainMenuUI;
    public GameObject TitleText;
    public GameObject GameOverText;
    public GameObject ContinueButton;
    public GameObject PlayButton;

    public CinemachineVirtualCamera MainVCam;
    public CinemachineVirtualCamera IntroVCam;
    public SnakeMovement Movement;

    private void Awake()
    {
#if UNITY_ANDROID || UNITY_IOS
        PostProcessing.SetActive(false);
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

    public void Play()
    {
        // Switch to main camera, hide main menu UI, show gameplay UI, and enable movement/growth of snake
        Grow.enabled = true;
        TitleText.SetActive(false);
        mainMenuUI.SetActive(false);
        GamePlayUI.SetActive(true);
        MainVCam.Priority = 20;
        IntroVCam.enabled = false;

        onPlay?.Invoke();
    }

    public void Pause()
    {
        // Switch cameras and stop movement/growth of snake
        IntroVCam.enabled = true;
        Grow.enabled = false;
        Movement.enabled = false;
        MainVCam.Priority = 10;
        GamePlayUI.SetActive(false);
        mainMenuUI.SetActive(true);
        FinalScoreText.SetActive(false);
        GameOverText.SetActive(false);
        ContinueButton.SetActive(true);
        PlayButton.SetActive(false);
    }

    public void Continue()
    {
        // Switch cameras and start movement/growth of snake
        Grow.enabled = true;
        ContinueButton.SetActive(false);
        PlayButton.SetActive(true);
        mainMenuUI.SetActive(false);
        FinalScoreText.SetActive(true);
        GameOverText.SetActive(true);
        GamePlayUI.SetActive(true);
        MainVCam.Priority = 20;
        IntroVCam.enabled = false;
        Movement.enabled = true;
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    private void HandleGameOver()
    {
        StartCoroutine(GameOverCoroutine());
    }

    private IEnumerator GameOverCoroutine()
    {
        // Smooth transition to orbiting camera
        IntroVCam.enabled = true;
        MainVCam.Priority = 10;

        yield return new WaitForSeconds(1.5f);

        // Show ad on mobile
#if UNITY_ANDROID || UNITY_IOS
        AdManager.Instance.ShowInterstitialAd();
#endif

        // Show Game Over UI
        mainMenuUI.SetActive(true);
        GameOverText.SetActive(true);
        FinalScoreText.SetActive(true);
        GamePlayUI.SetActive(false);
        FinalScoreText.GetComponent<Text>().text = "Your score was: " + GetComponent<Score>().finalScore.ToString();
    }
}
