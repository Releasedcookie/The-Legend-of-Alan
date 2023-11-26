using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardAds : MonoBehaviour
{

    private string _adUnitId = "ca-app-pub-2169102625145244/7941838386";
    private RewardedAd rewardedAd;

    // Start is called before the first frame update
    void Start()
    {
        LoadRewardedAd();
    }

    public void LoadRewardedAd()
    {
        // Debug.Log("GoogleAds: Loading Rewarded Ads...");

        // Clean up the old ad before loading a new one.
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        // Debug.Log("GoogleAds: Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();
        //adRequest.Keywords.Add("unity-admob-sample");

        // send the request to load the ad.
        RewardedAd.Load(_adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("GoogleAds: Rewarded ad failed to load an ad " + "with error : " + error);
                    return;
                }

               // Debug.Log("GoogleAds: Rewarded ad loaded with response : " + ad.GetResponseInfo());

                rewardedAd = ad;
            });
    }

    public void ShowRewardedAd()
    {
        // Debug.Log("GoogleAds: Ordered to Show Rewarded Ads");

        const string rewardMsg =
            "GoogleAds: Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                // TODO: Reward the user.
                Debug.Log("GoogleAds: " + String.Format(rewardMsg, reward.Type, reward.Amount));
                FindFirstObjectByType<LivesSystem>().resetLives();
                RegisterEventHandlers(rewardedAd);
            });
        }
    }

    private void RegisterEventHandlers(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("GoogleAds: Rewarded ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("GoogleAds: Rewarded ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("GoogleAds: Rewarded ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("GoogleAds: Rewarded ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("GoogleAds: Rewarded ad full screen content closed.");
            FindFirstObjectByType<LivesSystem>().resetLives();
            rewardedAd.Destroy();
            LoadRewardedAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("GoogleAds: Rewarded ad failed to open full screen content " +
                           "with error : " + error);
            FindFirstObjectByType<LivesSystem>().resetLives();
            rewardedAd.Destroy();
            LoadRewardedAd();
        };
    }

}
