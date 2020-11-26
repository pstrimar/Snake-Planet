using System;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject foodPrefab;
    public GameObject planet;
    public MainMenuUI mainMenuUI;
    public float foodHeight = 1f;
    public int startingFood = 15;
    public int foodValue = 3;
    public List<GameObject> foodList = new List<GameObject>();
    public event Action<int> onFoodPickup;

    private new AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        if (mainMenuUI != null)
        {
            mainMenuUI.onPlay += HandleReplay;
        }
    }

    private void OnDisable()
    {
        if (mainMenuUI != null)
        {
            mainMenuUI.onPlay -= HandleReplay;
        }
    }

    private void Update()
    {
        while (foodList.Count < startingFood)
        {
            SpawnFood();
        }
    }

    public void SpawnFood()
    {
        Vector3 spawnPosition = UnityEngine.Random.onUnitSphere * (planet.GetComponent<SphereCollider>().radius * planet.transform.localScale.x);

        GameObject food = Instantiate(foodPrefab, spawnPosition, Quaternion.identity);

        foodList.Add(food);

        food.transform.LookAt(planet.transform.position);
        food.transform.Rotate(-90, 0, 0);
    }

    public void BroadcastFoodPickup()
    {
        audio.Play();
        onFoodPickup?.Invoke(foodValue);
    }

    private void HandleReplay()
    {
        foreach (GameObject food in foodList)
        {
            Destroy(food);
        }

        foodList.Clear();
    }
}
