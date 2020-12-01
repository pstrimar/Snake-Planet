using System;
using UnityEngine;

public class FoodPickup : MonoBehaviour
{
    public int FoodValue = 5;
    public bool WasHereFirst;
    public static event Action<GameObject, int> onFoodPickedUp;

    private float rotateSpeed = 50f;
    private float timer = .1f;

    private void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        // If object has been in existance longer than timer, we say it was here first, 
        // so any new food spawned too close to this object will be destroyed instead of this object
        if (timer <= 0)
        {
            WasHereFirst = true;
            GetComponent<MeshRenderer>().enabled = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Head")
        {
            // Broadcast food pickup
            onFoodPickedUp?.Invoke(this.gameObject, FoodValue);
            Destroy(this.gameObject);
        }
    }

    // Avoid being placed too close to obstacles or food
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle" || other.gameObject.tag == "SpawnPoint" ||
            (other.gameObject.tag == "Food" && other.GetComponent<FoodPickup>().WasHereFirst))
        {
            FindObjectOfType<FoodSpawner>().foodList.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }
}
