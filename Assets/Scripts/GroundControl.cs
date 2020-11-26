using UnityEngine;

public class GroundControl : MonoBehaviour
{
    public bool onGround;
    private float distanceToGround;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(transform.position, -transform.up, out hit, 10))
        {
            distanceToGround = hit.distance;

            if (distanceToGround < 0.2f)
                onGround = true;
            else
                onGround = false;
        }
    }
}
