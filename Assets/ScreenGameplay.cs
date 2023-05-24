using System.Collections;
using System.Collections.Generic;
using Master.UIKit;
using Portadown.UIKit;
using UnityEngine;

public class ScreenGameplay : UIScreenView
{
    public override void OnAwake()
    {
        base.OnAwake();
    }
    private void Start()
    {
       Events.OnGameRestart += ShowGamePlayScreen;
    }
    public override void OnScreenShowAnimationCompleted()
    {
        base.OnScreenShowAnimationCompleted();
        Debug.Log("gmplay anim completed");
    }

    public override void OnScreenHideCalled()
    {
        base.OnScreenHideCalled();
        Debug.Log("gmplay hide called");
    }
    public void ShowGamePlayScreen()
    {
        Debug.Log("reched here...");
        UIController.instance.ShowNextScreen(ScreenType.Gameplay);
    }
}
