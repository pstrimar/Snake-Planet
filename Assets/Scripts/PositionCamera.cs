using System.Collections;
using UnityEngine;

public class PositionCamera : MonoBehaviour
{
    public MainMenuUI mainMenuUI;
    public GameObject vCam;

    private void OnEnable()
    {
        if (mainMenuUI != null)
        {
            mainMenuUI.onPlay += HandleReplay;
        }
    }

    private void OnDisable()
    {
        if (mainMenuUI != null)
        {
            mainMenuUI.onPlay -= HandleReplay;
        }
    }

    private void HandleReplay()
    {
        StartCoroutine(DisableAndReEnableCamera());
    }

    private IEnumerator DisableAndReEnableCamera()
    {
        vCam.SetActive(false);
        yield return new WaitForSeconds(.1f);
        vCam.SetActive(true);
    }
}
