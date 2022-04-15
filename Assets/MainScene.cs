using UnityEngine;
using UnityEngine.SceneManagement;

using GoogleMobileAds.Api;
using GoogleMobileAds.Placement;

public class MainScene : MonoBehaviour
{
    InterstitialAdGameObject interstitialAd;
    RewardedAdGameObject rewardedAdGameObject;

    void Start()
    {
        rewardedAdGameObject = MobileAds.Instance.GetAd<RewardedAdGameObject>("Rewarded Ad");

        interstitialAd = MobileAds.Instance
            .GetAd<InterstitialAdGameObject>("Interstitial Ad");

        MobileAds.Initialize((initStatus) => {
            Debug.Log("Initialized MobileAds");
        });
        // interstitialAd.LoadAd();
        rewardedAdGameObject.LoadAd();
    }

    public void OnClickShowGameSceneButton()
    {
        // Display an interstitial ad
        // interstitialAd.ShowIfLoaded();
        rewardedAdGameObject.ShowIfLoaded();

        // Load a scene named "GameScene"
        // SceneManager.LoadScene("GameScene");
    }

    public void OnCloseButton()
    {
        Debug.Log("広告を閉じた");
    }

    public void OnCloseRewardButton()
    {
        Debug.Log("リワードもろた");
    }
}