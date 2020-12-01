using System;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject foodPrefab;
    public GameObject planet;
    public float foodHeight = 1f;
    public int startingFood = 15;    
    public List<GameObject> foodList = new List<GameObject>();

    private new AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        MainMenuUI.onPlay += HandleReplay;
        FoodPickup.onFoodPickedUp += HandleFoodPickup;
    }

    private void OnDisable()
    {
        MainMenuUI.onPlay -= HandleReplay;
        FoodPickup.onFoodPickedUp -= HandleFoodPickup;
    }

    private void Update()
    {
        // Spawn food if we have less than our starting count
        while (foodList.Count < startingFood)
        {
            SpawnFood();
        }
    }

    // Spawn on a random position on the planet and orient the food
    public void SpawnFood()
    {
        Vector3 spawnPosition = UnityEngine.Random.onUnitSphere * (planet.GetComponent<SphereCollider>().radius * planet.transform.localScale.x);

        GameObject food = Instantiate(foodPrefab, spawnPosition, Quaternion.identity);

        foodList.Add(food);

        food.transform.LookAt(planet.transform.position);
        food.transform.Rotate(-90, 0, 0);
    }

    private void HandleFoodPickup(GameObject food, int foodValue)
    {
        audio.Play();
        foodList.Remove(food);
    }

    // Destroy all food and clear list
    private void HandleReplay()
    {
        foreach (GameObject food in foodList)
        {
            Destroy(food);
        }

        foodList.Clear();
    }
}
