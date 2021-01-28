using UnityEngine;
using UnityEngine.Advertisements;

#if UNITY_ANDROID || UNITY_IOS
public class AdManager : MonoBehaviour
{
#if UNITY_IOS
    private string gameId = "3913138";
#elif UNITY_ANDROID
    private string gameId = "3913139";
#endif
    bool testMode = false;

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
#endif
