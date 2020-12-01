using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] float gravity = -100f;

    public void Attract(Transform playerTransform)
    {
        Vector3 gravityUp = (playerTransform.position - transform.position).normalized;
        Vector3 localUp = playerTransform.up;

        // Adds gravity force downward in direction of planet center
        playerTransform.GetComponent<Rigidbody>().AddForce(gravityUp * gravity);

        // Rotates player to align local up with gravity up
        Quaternion targetRotation = Quaternion.FromToRotation(localUp, gravityUp) * playerTransform.rotation;
        playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, 50f * Time.fixedDeltaTime);
    }
}
