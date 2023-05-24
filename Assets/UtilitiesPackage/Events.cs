

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{

    public static event Action<float> OnDownlodProgress;
    public static event Action OnDownlodFailed;

    public static event Action<string, bool> OnNearestSculpture;

    //public static event Action OnLanguageChanged;
    public static event Action<string> OnLanguageChanged;

    public static event Action<bool> RaycasterOnOff;


    public static event Action<bool> OnScreenChange;
    public static event Action OnGameOver;

    public static event Action OnGameRestart;


    public static void GameOver()
    {
        Debug.Log("GameOver called");
        OnGameOver?.Invoke();
    }

    public static void GameRestart()
    {
        OnGameRestart?.Invoke();
    }
    public static void ScreenChange(bool val)
    {
        OnScreenChange?.Invoke(val);
    }

    
   

    public static void EventRayCasterOnOff(bool rayCasterOff)
    {
        //Debug.Log("on or off "+rayCasterOff);
        if (RaycasterOnOff != null)
            RaycasterOnOff(rayCasterOff);
    }

    //public static void LanguageChanged()
    //{
    //    if (OnLanguageChanged != null)
    //        OnLanguageChanged();
    //}
    public static void LanguageChanged(string langId)
    {
        if (OnLanguageChanged != null)
            OnLanguageChanged(langId);
    }
}