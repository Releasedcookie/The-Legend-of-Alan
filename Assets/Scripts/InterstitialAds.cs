using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterstitialAds : MonoBehaviour
{
    private string _adUnitId = "ca-app-pub-2169102625145244/3999240865";
    private InterstitialAd interstitialAd;

    // Start is called before the first frame update
    void Start()
    {
       LoadInterstitialAd();
    }

    public void LoadInterstitialAd()
    {

        // Debug.Log("GoogleAds: Loading Interstitial Ads...");
        // Debug.Log("GoogleAds: Loading Interstitial Ad...");
        // Clean up the old ad before loading a new one.
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        // create our request used to load the ad.
        var adRequest = new AdRequest();
        //adRequest.Keywords.Add("unity-admob-sample");

        // send the request to load the ad.
        InterstitialAd.Load(_adUnitId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    // Debug.LogError("GoogleAds: Interstitial ad failed to load an ad " + "with error : " + error);
                    return;
                }

                // Debug.Log("GoogleAds: Interstitial ad loaded with response : " + ad.GetResponseInfo());

                interstitialAd = ad;
            });
    }

    public void ShowAd()
    {
        // Debug.Log("GoogleAds: Ordered to Show interstitial Ads");

        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            // Debug.Log("GoogleAds: Showing interstitial ad.");
            interstitialAd.Show();
            RegisterEventHandlers(interstitialAd);
        }
        else
        {
            Debug.LogError("GoogleAds: Interstitial ad is not ready yet.");
            FindFirstObjectByType<FinishScript>().MoveToNextLevel();
        }
    }

    private void RegisterEventHandlers(InterstitialAd ad)
    {

        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("GoogleAds: Interstitial ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("GoogleAds: Interstitial ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("GoogleAds: Interstitial ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("GoogleAds: Interstitial ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
           //  Debug.Log("GoogleAds: Interstitial ad full screen content closed.");
            FindFirstObjectByType<FinishScript>().MoveToNextLevel();
            interstitialAd.Destroy();
            LoadInterstitialAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("GoogleAds: Interstitial ad failed to open full screen content " +
                           "with error : " + error);
            LoadInterstitialAd();
            FindFirstObjectByType<FinishScript>().MoveToNextLevel();
        };
    }
}
