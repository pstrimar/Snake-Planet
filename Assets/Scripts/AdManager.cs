using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance;

#if UNITY_IOS
    private string gameId = "3913138";
#elif UNITY_ANDROID
    private string gameId = "3913139";
#endif
    bool testMode = false;

    private void Awake()
    {
        if (Instance != null)
        {
            if (Instance != this)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        // Initialize the Ads service:
        Advertisement.Initialize(gameId, testMode);
    }

    public void ShowInterstitialAd()
    {
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }
        else
        {
            Debug.Log("Interstitial ad not ready at the moment! Please try again later!");
        }
    }
}
