using UnityEngine;

public class DayNight : MonoBehaviour
{
    [SerializeField] Material daySkyBox;
    [SerializeField] Material nightSkyBox;
    [SerializeField] GameObject dayButton;
    [SerializeField] GameObject nightButton;
    [SerializeField] GameObject dayLight;
    [SerializeField] GameObject nightLight;

    // Sets the skybox, lighting, fog, and UI for day mode
    public void DayMode()
    {
        RenderSettings.skybox = daySkyBox;
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.Linear;
        RenderSettings.fogColor = new Color(1, .43f, .24f);
        RenderSettings.fogStartDistance = 0;
        RenderSettings.fogEndDistance = 70;
        nightButton.SetActive(false);
        dayButton.SetActive(true);
        dayLight.SetActive(true);
        nightLight.SetActive(false);
    }

    // Sets the skybox, lighting, fog, and UI for night mode
    public void NightMode()
    {
        RenderSettings.skybox = nightSkyBox;
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.Linear;
        RenderSettings.fogColor = new Color(.78f, .22f, .59f);
        RenderSettings.fogStartDistance = -10;
        RenderSettings.fogEndDistance = 100;
        dayButton.SetActive(false);
        nightButton.SetActive(true);
        nightLight.SetActive(true);
        dayLight.SetActive(false);
    }
}
