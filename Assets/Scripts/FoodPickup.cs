using UnityEngine;

public class FoodPickup : MonoBehaviour
{
    public bool wasHereFirst;
    private float rotateSpeed = 50f;
    private float timer = .1f;

    private void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            wasHereFirst = true;
            GetComponent<MeshRenderer>().enabled = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Head")
        {
            FindObjectOfType<FoodSpawner>().foodList.Remove(this.gameObject);
            FindObjectOfType<FoodSpawner>().BroadcastFoodPickup();
            Destroy(this.gameObject);
        }
    }

    // Avoid being placed too close to obstacles or food
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle" || other.gameObject.tag == "SpawnPoint" ||
            (other.gameObject.tag == "Food" && other.GetComponent<FoodPickup>().wasHereFirst))
        {
            FindObjectOfType<FoodSpawner>().foodList.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }
}
