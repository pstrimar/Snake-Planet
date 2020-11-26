using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{
    public SnakeMovement movement;

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
