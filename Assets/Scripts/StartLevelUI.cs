using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartLevelUI : MonoBehaviour
{
    public new Animation animation;
    public GameObject countDown;
    public SnakeMovement movement;
    private Text countDownText;

    private void OnEnable()
    {
        if (movement != null)
        {
            movement.onStartLevel += HandleStartLevel;
        }
    }

    private void OnDisable()
    {
        if (movement != null)
        {
            movement.onStartLevel -= HandleStartLevel;
        }
    }

    private void HandleStartLevel()
    {        
        StartCoroutine(CountDown());
    }

    private IEnumerator CountDown()
    {
        countDown.SetActive(true);
        countDownText = countDown.GetComponent<Text>();
        countDownText.text = "3";
        yield return new WaitForSeconds(1);
        countDownText.text = "2";
        yield return new WaitForSeconds(1);
        countDownText.text = "1";
        yield return new WaitForSeconds(1);
        countDownText.text = "Go!";
        yield return new WaitForSeconds(1);
        countDown.SetActive(false);
    }
}
