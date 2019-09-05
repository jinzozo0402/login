using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
public class GameController : MonoBehaviour {

    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject Start;
    void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            FB.ActivateApp();
        }
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            // ...
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }

    #region Login/Logout
    public void FacebookLogin()
    {
        var permissions = new List<string>() { "public_profile", "email", "user_friends" };
        FB.LogInWithReadPermissions(permissions);
        MainMenu.SetActive(true);
        Start.SetActive(false);
    }

    public void FacebookLogout()
    {
        FB.LogOut();
        MainMenu.SetActive(false);
        Start.SetActive(true);
    }
    #endregion
    
    public void FacebookShare()
    {
        FB.ShareLink(new System.Uri("https://www.facebook.com"), "Check it out!"," " , new System.Uri("https://www.facebook.com/places/Things-to-do-in-Ho-Chi-Minh-City-Vietnam/108458769184495/?__xts__%5B0%5D=68.ARCRu8ruoISM1ZnJ4WdDxCgejNnW2hPJ6fvFuUg7WxbHqrAgUuRvpwC1xgDEgM23qwC-T1VXEY_nTW9-EKpjPrOT-E13XA7DwU-t6RuVIktGNmMAmAnuYSTpRj2sZzfUa-hdSte-FGfNsntEUnIr_wPB5Nm7HvtdKXWaRwRYZRYw1zt-PmimxLi416HyXLNvxIhlVRAAPIEdGDLLhB0ciYaTuF-NEaUoRCBirLqMY4UffJfb5lYDqjcUJFbDFGag1ZEovXZDuK5cLf6eZL5VcgdmPoHch2sZe2QZV1ZRTVYPv7vLLnbjWfYb5co_Myken7xJIJy81rfQNzaNyzBiwXegb7k9tY-Hdk29of6o5W-mAGEd0K1va2gkkurnYEGqy6dypo9pImuK0UK1y6AVaVkdSyq9CcrSz3lj8Z8mV9QCs64Wdlo3AYAOAMJDwBhwsXb0dVejMw2dm0c&__tn__=-R"));
    }
    string MyEscapeURL(string url)
    {
        return WWW.EscapeURL(url).Replace("+","% 20");
    }
    public void OpendWeb()
    {
        string address = "東宝江戸川橋ビール３F江戸川橋駅";
        string addressNew = MyEscapeURL(address);
        Debug.Log("address New: " + addressNew);
        string str = "http://maps.google.com/maps?q=" + addressNew ;
        Debug.Log("url: " + str);
        Application.OpenURL(str);
    }
    
}
