using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    public List<Transform> bodyParts = new List<Transform>();
    public Joystick joystick;
    public MainMenuUI mainMenuUI;
    public FoodSpawner foodSpawner;
    public float minDistance;
    public int beginSize;
    public float speed = 1f;
    public float rotationSpeed = 50f;
    public float timeFromLastRetry;
    public GameObject bodyPrefab;
    public bool isAlive;
    public event Action onBodyPartAdded;
    public event Action onGameOver;
    public event Action onStartLevel;

    private float distance;
    private float startingMinDistance = 0.025f;
    private Transform currentBodyPart;
    private Transform previousBodyPart;

    private void OnEnable()
    {
        if (mainMenuUI != null)
        {
            mainMenuUI.onPlay += HandlePlay;
        }

        if (foodSpawner != null)
        {
            foodSpawner.onFoodPickup += HandleFoodPickup;
        }
    }

    private void OnDisable()
    {
        if (mainMenuUI != null)
        {
            mainMenuUI.onPlay -= HandlePlay;
        }

        if (foodSpawner != null)
        {
            foodSpawner.onFoodPickup -= HandleFoodPickup;
        }
    }

    void Update()
    {
        if (isAlive)
            Move();

#if UNITY_ANDROID || UNITY_IOS
        if (!isAlive && joystick.gameObject.activeSelf)
            joystick.gameObject.SetActive(false);
#endif
    }

    public IEnumerator StartLevel()
    {
        onStartLevel?.Invoke();

        timeFromLastRetry = Time.time;

        minDistance = startingMinDistance;

        transform.localScale = new Vector3(1, 1, 1);

        for (int i = bodyParts.Count - 1; i > 0; i--)
        {
            Destroy(bodyParts[i].gameObject);
            bodyParts.Remove(bodyParts[i]);
        }

        bodyParts[0].position = new Vector3(0, 5.141f, 0);
        bodyParts[0].rotation = Quaternion.identity;

        // Wait for countdown before moving
        yield return new WaitForSeconds(3);

        AddBodyParts(beginSize - 1);

        isAlive = true;
    }

    public void Move()
    {
        float currentSpeed = speed;

        float horizontal = 0;
        float vertical = 0;

        //Check if we are running either in the Unity editor or in a standalone build.
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL

        joystick.gameObject.SetActive(false);

        horizontal = Input.GetAxisRaw("Horizontal");

        vertical = Input.GetAxisRaw("Vertical");

        //Check if we are running on iOS, Android, Windows Phone 8 or Unity iPhone
#elif UNITY_ANDROID || UNITY_IOS

        joystick.gameObject.SetActive(true);

        horizontal = joystick.Horizontal;
        vertical = joystick.Vertical;

#endif

        if (vertical > .5f)
            currentSpeed *= 2f;

        bodyParts[0].Translate(bodyParts[0].forward * currentSpeed * Time.smoothDeltaTime, Space.World);

        if (Mathf.Abs(horizontal) > .2f)
            bodyParts[0].Rotate(Vector3.up * rotationSpeed * Time.deltaTime * horizontal);

        for (int i = 1; i < bodyParts.Count; i++)
        {
            currentBodyPart = bodyParts[i];
            previousBodyPart = bodyParts[i - 1];

            distance = Vector3.Distance(previousBodyPart.position, currentBodyPart.position);

            Vector3 newPos = previousBodyPart.position;

            float T = Time.deltaTime * distance / minDistance * currentSpeed;

            if (T > 0.5f)
                T = 0.5f;

            currentBodyPart.position = Vector3.Slerp(currentBodyPart.position, newPos, T);
            currentBodyPart.rotation = Quaternion.Slerp(currentBodyPart.rotation, previousBodyPart.rotation, T);
        }
    }

    public void AddBodyParts(int numberOfSegments)
    {
        for (int i = 0; i < numberOfSegments; i++)
        {
            Transform newPart = (Instantiate(bodyPrefab, bodyParts[bodyParts.Count - 1].position, bodyParts[bodyParts.Count - 1].rotation) as GameObject).transform;

            newPart.SetParent(transform);

            newPart.transform.localScale = bodyParts[0].localScale;

            bodyParts.Add(newPart);
        }

        if (isAlive)
            onBodyPartAdded?.Invoke();
    }

    public void Die()
    {
        isAlive = false;

        onGameOver?.Invoke();
    }

    private void HandlePlay()
    {
        StartCoroutine(StartLevel());
    }

    private void HandleFoodPickup(int foodValue)
    {
        AddBodyParts(foodValue);
    }
}
