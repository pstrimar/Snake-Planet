using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    public SnakeMovement snake;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Snake")
        {
            if (collision.transform != snake.bodyParts[1] && snake.isAlive)
            {
                if (Time.time - snake.timeFromLastRetry > 5)
                    snake.Die();
            }
        }
    }
}
