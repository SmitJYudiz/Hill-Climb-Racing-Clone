using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Portadown.UIKit
{
    public class UIScreenView : UIView
    {
        
    }

    public class UIPopupView : UIView
    {
        [HideInInspector]
        public UIView previousPopup = null;

        public override void OnScreenHideAnimationCompleted()
        {
            base.OnScreenHideAnimationCompleted();
            if (previousPopup != null)
            {
                previousPopup.ToggleRaycaster(true);
                previousPopup = null;
            }
        }
    }
}