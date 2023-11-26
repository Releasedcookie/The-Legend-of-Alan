using GoogleMobileAds.Api;
using GoogleMobileAds;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Init_GoogleAds : MonoBehaviour
{
    public static Init_GoogleAds instance;
    void Start()
    {
        //DontDestroyChildOnLoad(this.gameObject);

        //Debug.Log("GoogleAds: Initializing Admob...");

        MobileAds.Initialize(initStatus => {
            // Debug.Log("GoogleAds: GoogleAds has Initialized");
        });
    }

    public static void DontDestroyChildOnLoad(GameObject child)
    {
        Transform parentTransform = child.transform;

        // If this object doesn't have a parent then its the root transform.
        while (parentTransform.parent != null)
        {
            // Keep going up the chain.
            parentTransform = parentTransform.parent;
        }

        GameObject.DontDestroyOnLoad(parentTransform.gameObject);
    }
}
