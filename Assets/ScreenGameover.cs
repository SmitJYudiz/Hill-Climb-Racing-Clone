using System.Collections;
using System.Collections.Generic;
using Master.UIKit;
using Portadown.UIKit;
using UnityEngine;

public class ScreenGameover : UIScreenView
{

    private void Start()
    {
        Events.OnGameOver += ShowGameOverScreen;
    }

    public void OnClickPlayAgain()
    {
        //player's position reset:




        //hide our gameover screen now
    }

    void ShowGameOverScreen()
    {
        UIController.instance.ShowNextScreen(ScreenType.Gameover);
    }
}
