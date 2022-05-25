using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsExample : MonoBehaviour
{

    void Start()
    {
        Advertisement.Initialize("4766532");
    }
    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady("Rewarded_iOS"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("Rewarded_iOS", options);
        }
    }


    public void ShowInterAd()
    {
        if (Advertisement.IsReady("Interstitial_Android"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("Interstitial_Android", options);
        }
    }


    void ShowAds()
    {
        Advertisement.Show();
    }

    public void ShowAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }


    }
    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                //
                // YOUR CODE TO REWARD THE GAMER
                // Give coins etc.
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }
}