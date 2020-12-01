using System.Collections;
using UnityEngine;

public class PositionCamera : MonoBehaviour
{
    public GameObject vCam;

    private void OnEnable()
    {
        MainMenuUI.onPlay += HandleReplay;
    }

    private void OnDisable()
    {
        MainMenuUI.onPlay -= HandleReplay;
    }

    private void HandleReplay()
    {
        StartCoroutine(DisableAndReEnableCamera());
    }

    // Prevents jerky camera transition
    private IEnumerator DisableAndReEnableCamera()
    {
        vCam.SetActive(false);
        yield return new WaitForSeconds(.1f);
        vCam.SetActive(true);
    }
}
