using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerAds : MonoBehaviour
{
    private string _adUnitId = "ca-app-pub-2169102625145244/1528990197";
    BannerView _bannerView;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("GoogleAds: Loading Banner Ad...");
        LoadAd();
    }

    public void CreateBannerView()
    {
        // Debug.Log("GoogleAds: Creating banner view");

        // If we already have a banner, destroy the old one.
        if (_bannerView != null)
        {
            DestroyAd();
        }

        // Create a 320x50 banner at top of the screen
        int bannerHeight = 50; //Because AdSize.Banner measures 320x50
        int bannerWidth = 320;
        AdSize adSize = new AdSize(bannerWidth, bannerHeight);
        int w = (Screen.width / 2) - (bannerWidth / 2);
        int h = 0;
        _bannerView = new BannerView(_adUnitId, AdSize.Banner, w, h);
        // Debug.Log("GoogleAds: Banner Ad Loaded");
    }

    public void LoadAd()
    {
        // create an instance of a banner view first.
        if (_bannerView == null)
        {
            CreateBannerView();
        }
        // create our request used to load the ad.
        var adRequest = new AdRequest();
        //adRequest.Keywords.Add("unity-admob-sample");

        // send the request to load the ad.
         // Debug.Log("GoogleAds: Displaying banner ad...");
        _bannerView.LoadAd(adRequest);
        //Debug.Log("GoogleAds: Banner ad Displayed");
    }

    public void DestroyAd()
    {
        if (_bannerView != null)
        {
            // Debug.Log("GoogleAds: Destroying banner ad.");
            _bannerView.Destroy();
            _bannerView = null;
        }
    }

}
