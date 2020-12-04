using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    public List<Transform> BodyParts = new List<Transform>();
    public Joystick Joystick;
    public float TimeFromLastRetry;
    public bool IsAlive;
    public float MinDistance;

    public static event Action onBodyPartAdded;
    public static event Action onGameOver;
    public static event Action onStartLevel;

    [SerializeField] GameObject bodyPrefab;
    [SerializeField] float startingMinDistance = 0.025f;
    [SerializeField] int beginSize;
    [SerializeField] float speed = 1f;
    [SerializeField] float rotationSpeed = 50f;

    private float distance;
    private Transform currentBodyPart;
    private Transform previousBodyPart;

    private void OnEnable()
    {
        MainMenuUI.onPlay += HandlePlay;
        FoodPickup.onFoodPickedUp += HandleFoodPickup;
    }

    private void OnDisable()
    {
        MainMenuUI.onPlay -= HandlePlay;
        FoodPickup.onFoodPickedUp -= HandleFoodPickup;
    }

    private void Start()
    {
#if UNITY_STANDALONE || UNITY_WEBGL

        Joystick.gameObject.SetActive(false);
#endif
    }
    void Update()
    {
        // Only allow movement when alive
        if (IsAlive)
            Move();
    }

    public IEnumerator StartLevel()
    {
        // Broadcast start level
        onStartLevel?.Invoke();

        TimeFromLastRetry = Time.time;

        MinDistance = startingMinDistance;

        // Reset scale to 1
        transform.localScale = new Vector3(1, 1, 1);

        for (int i = BodyParts.Count - 1; i > 0; i--)
        {
            Destroy(BodyParts[i].gameObject);
            BodyParts.Remove(BodyParts[i]);
        }

        // Set position of head of snake to top of planet
        BodyParts[0].position = new Vector3(0, 5.141f, 0);
        BodyParts[0].rotation = Quaternion.identity;

#if UNITY_ANDROID || UNITY_IOS
        // Reset joystick position and values
        Joystick.enabled = true;
        Joystick.gameObject.SetActive(true);

        Joystick.Horizontal = 0;
        Joystick.Vertical = 0;
#endif

        // Wait for countdown before moving
        yield return new WaitForSeconds(3);

        // Already starting with the head so we subtract 1
        AddBodyParts(beginSize - 1);

        IsAlive = true;
    }

    public void Move()
    {
        float currentSpeed = speed;

        float horizontal = 0;
        float vertical = 0;

        //Check if we are running either in the Unity editor, WebGL or in a standalone build.
#if UNITY_STANDALONE || UNITY_WEBGL

        horizontal = Input.GetAxisRaw("Horizontal");

        vertical = Input.GetAxisRaw("Vertical");

        //Check if we are running on iOS or Android
#elif UNITY_ANDROID || UNITY_IOS

        horizontal = Joystick.Horizontal;
        vertical = Joystick.Vertical;

        // Set horizontal to 1 if greater than .4
        if (horizontal > .4f)
            horizontal = Mathf.CeilToInt(horizontal);

        // Set horizontal to -1 if less than -.4
        if (horizontal < -.4f)
            horizontal = Mathf.FloorToInt(horizontal);

#endif
        // Double our speed if pressing forward
        if (vertical > .3f)
            currentSpeed *= 2f;

        // Translate head of snake forward with our speed
        BodyParts[0].Translate(BodyParts[0].forward * currentSpeed * Time.smoothDeltaTime, Space.World);

        // Rotate left or right based on horizontal input
        if (Mathf.Abs(horizontal) > .2f)
            BodyParts[0].Rotate(Vector3.up * rotationSpeed * Time.deltaTime * horizontal);

        for (int i = 1; i < BodyParts.Count; i++)
        {
            currentBodyPart = BodyParts[i];
            previousBodyPart = BodyParts[i - 1];

            distance = Vector3.Distance(previousBodyPart.position, currentBodyPart.position);

            Vector3 newPos = previousBodyPart.position;

            float T = Time.deltaTime * distance / MinDistance * currentSpeed;

            if (T > 0.5f)
                T = 0.5f;

            // Follow previous body part position and rotation over time
            currentBodyPart.position = Vector3.Slerp(currentBodyPart.position, newPos, T);
            currentBodyPart.rotation = Quaternion.Slerp(currentBodyPart.rotation, previousBodyPart.rotation, T);
        }
    }

    // Adds a number of body parts at the tail position and sets their scale, and adds them to our list
    private void AddBodyParts(int numberOfSegments)
    {
        for (int i = 0; i < numberOfSegments; i++)
        {
            Transform newPart = (Instantiate(bodyPrefab, BodyParts[BodyParts.Count - 1].position, BodyParts[BodyParts.Count - 1].rotation) as GameObject).transform;

            newPart.SetParent(transform);

            newPart.transform.localScale = BodyParts[0].localScale;

            BodyParts.Add(newPart);
        }

        if (IsAlive)
            onBodyPartAdded?.Invoke();
    }

    public void Die()
    {
        IsAlive = false;

        // Hide joystick when dead on mobile
#if UNITY_ANDROID || UNITY_IOS
        Joystick.enabled = false;
        Joystick.gameObject.SetActive(false);
#endif

        onGameOver?.Invoke();
    }

    private void HandlePlay()
    {
        StartCoroutine(StartLevel());
    }

    // Adds body parts based on value of food we picked up
    private void HandleFoodPickup(GameObject food, int foodValue)
    {
        AddBodyParts(foodValue);
    }
}
