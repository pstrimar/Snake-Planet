using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartLevelUI : MonoBehaviour
{
    public GameObject CountDownGO;
    private Text countDownText;

    private void OnEnable()
    {
        SnakeMovement.onStartLevel += HandleStartLevel;
    }

    private void OnDisable()
    {
        SnakeMovement.onStartLevel -= HandleStartLevel;
    }

    private void HandleStartLevel()
    {
        StartCoroutine(CountDown());
    }

    private IEnumerator CountDown()
    {
        CountDownGO.SetActive(true);
        countDownText = CountDownGO.GetComponent<Text>();
        countDownText.text = "3";
        yield return new WaitForSeconds(1);
        countDownText.text = "2";
        yield return new WaitForSeconds(1);
        countDownText.text = "1";
        yield return new WaitForSeconds(1);
        countDownText.text = "Go!";
        yield return new WaitForSeconds(1);
        CountDownGO.SetActive(false);
    }
}
