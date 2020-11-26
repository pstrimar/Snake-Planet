using UnityEngine;

public class GravityBody : MonoBehaviour
{
    private Planet planet;

    private void Awake()
    {
        planet = FindObjectOfType<Planet>();

        GetComponent<Rigidbody>().useGravity = false;

        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void FixedUpdate()
    {
        if (planet)
        {
            planet.Attract(this.transform);
        }
    }
}
