using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{
    private void OnEnable()
    {
        SnakeMovement.onGameOver += HandleGameOver;
    }

    private void OnDisable()
    {
        SnakeMovement.onGameOver -= HandleGameOver;
    }

    private void HandleGameOver()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Advertisement.IsReady("video"))
            {
                Advertisement.Show("video");
            }
        }
    }
}
