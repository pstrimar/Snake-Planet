using UnityEngine;

public class GravityBody : MonoBehaviour
{
    private Planet planet;

    private void Awake()
    {
        planet = FindObjectOfType<Planet>();

        // Turn gravity off since we will be using our planet's gravity
        GetComponent<Rigidbody>().useGravity = false;

        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void FixedUpdate()
    {
        if (planet != null)
        {
            planet.Attract(this.transform);
        }
    }
}
