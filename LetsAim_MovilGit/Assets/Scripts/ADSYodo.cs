using System;
using UnityEngine;
using Yodo1.MAS;

public class ADSYodo : MonoBehaviour
{

    private int retryAttempt = 0;
    void Awake()
    {
        Yodo1U3dMas.InitializeMasSdk();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Yodo1AdBuildConfig config = new Yodo1AdBuildConfig().enableUserPrivacyDialog(true);

        // Update the agreement link
        config = config.userAgreementUrl("Your user agreement url");

        // Update the privacy link
        config = config.privacyPolicyUrl("Your privacy policy url");

        Yodo1U3dMas.SetAdBuildConfig(config);
        Yodo1U3dMasCallback.OnSdkInitializedEvent += (success, error) =>
{
    if (success)
    {
        Debug.Log("[Yodo1 Mas] The initialization has succeeded");
        SetupEventCallbacks();
        LoadRewardAd();
    }
    else
    {
        Debug.Log("[Yodo1 Mas] The initialization has failed with error " + error.ToString());
    }
};

        int age = Yodo1U3dMas.GetUserAge();

        int attStatus = Yodo1U3dMas.GetAttrackingStatus();
        switch (attStatus)
        {
            case Yodo1U3dAttrackingStatus.NotDetermined: break;
            case Yodo1U3dAttrackingStatus.Restricted: break;
            case Yodo1U3dAttrackingStatus.Denied: break;
            case Yodo1U3dAttrackingStatus.Authorized: break;
            case Yodo1U3dAttrackingStatus.SystemLow: break;  // iOS version below 14
        }
    }

    private void SetupEventCallbacks()
    {
        Yodo1U3dRewardAd.GetInstance().OnAdLoadedEvent += OnRewardAdLoadedEvent;
        Yodo1U3dRewardAd.GetInstance().OnAdLoadFailedEvent += OnRewardAdLoadFailedEvent;
        Yodo1U3dRewardAd.GetInstance().OnAdOpenedEvent += OnRewardAdOpenedEvent;
        Yodo1U3dRewardAd.GetInstance().OnAdOpenFailedEvent += OnRewardAdOpenFailedEvent;
        Yodo1U3dRewardAd.GetInstance().OnAdClosedEvent += OnRewardAdClosedEvent;
        Yodo1U3dRewardAd.GetInstance().OnAdEarnedEvent += OnRewardAdEarnedEvent;
    }

    private void LoadRewardAd()
    {
        Yodo1U3dRewardAd.GetInstance().LoadAd();
    }

    private void OnRewardAdLoadedEvent(Yodo1U3dRewardAd ad)
    {
        // Code to be executed when an ad finishes loading.
        retryAttempt = 0;
        Yodo1U3dRewardAd.GetInstance().ShowAd();
    }

    private void OnRewardAdLoadFailedEvent(Yodo1U3dRewardAd ad, Yodo1U3dAdError adError)
    {
        // Code to be executed when an ad request fails.
        retryAttempt++;
        double retryDelay = Math.Pow(2, Math.Min(6, retryAttempt));
        Invoke("LoadRewardAd", (float)retryDelay);
    }

    private void OnRewardAdOpenedEvent(Yodo1U3dRewardAd ad)
    {
        // Code to be executed when an ad opened
    }

    private void OnRewardAdOpenFailedEvent(Yodo1U3dRewardAd ad, Yodo1U3dAdError adError)
    {
        // Code to be executed when an ad open fails.
        LoadRewardAd();
    }

    private void OnRewardAdClosedEvent(Yodo1U3dRewardAd ad)
    {
        // Code to be executed when the ad closed
        LoadRewardAd();
    }

    private void OnRewardAdEarnedEvent(Yodo1U3dRewardAd ad)
    {
        // Code executed when getting rewards
    }

    public void ShowReward()
    {
        bool isLoaded = Yodo1U3dRewardAd.GetInstance().IsLoaded();
        if(isLoaded) Yodo1U3dRewardAd.GetInstance().ShowAd();
    }

}
