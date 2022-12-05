using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    public static AdsManager instance;
    
    InterstitialAd interstitialAd;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
            
        DontDestroyOnLoad(gameObject);
    }

    void Start(){
        interstitialAd = GetComponent<InterstitialAd>();
    }

    public void ShowAd(){
        interstitialAd.ShowAd();
    }
}
