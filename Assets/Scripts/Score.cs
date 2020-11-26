using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int finalScore { get { return currentScore; } private set { } }
    public SnakeMovement movement;
    public MainMenuUI mainMenuUI;
    public Text currentScoreText;
    public Text bestScoreText;
    private int currentScore;
    private int bestScore;

    private void Awake()
    {
        currentScore = 0;
        bestScore = 0;
        currentScoreText.text = "Score: " + currentScore.ToString();
        bestScoreText.text = "Best: " + bestScore.ToString();
    }

    private void OnEnable()
    {
        if (movement != null)
        {
            movement.onBodyPartAdded += UpdateScore;
        }

        if (mainMenuUI != null)
        {
            mainMenuUI.onPlay += HandlePlay;
        }
    }

    private void OnDisable()
    {
        if (movement != null)
        {
            movement.onBodyPartAdded -= UpdateScore;
        }

        if (mainMenuUI != null)
        {
            mainMenuUI.onPlay -= HandlePlay;
        }
    }

    private void UpdateScore()
    {
        currentScore++;
        currentScoreText.text = "Score: " + currentScore.ToString();

        if (bestScore < currentScore)
        {
            bestScore++;
            bestScoreText.text = "Best: " + bestScore.ToString();
        }
    }

    private void HandlePlay()
    {
        currentScore = 0;
        currentScoreText.text = "Score: " + currentScore.ToString();
    }
}
