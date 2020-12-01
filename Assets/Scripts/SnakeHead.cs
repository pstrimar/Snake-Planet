using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    public SnakeMovement snake;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Snake")
        {
            // Do not kill snake from collision with body part directly behind head
            if (collision.transform != snake.BodyParts[1] && snake.IsAlive)
            {
                if (Time.time - snake.TimeFromLastRetry > 5)
                    snake.Die();
            }
        }
    }
}
