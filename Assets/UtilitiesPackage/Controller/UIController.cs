using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Portadown.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Portadown.UIKit
{
    [Serializable]
    public class UIScreen
    {
        public ScreenType screenType;
        public UIScreenView screenView;
    }
    [Serializable]
    public class UIPopup
    {
        public PopupType popupType;
        public UIPopupView popupView;
    }

    public enum ScreenType
    {
        None=0,
        Splash=1,
        Gameplay = 2,
        Menu=3,
        Gameover = 4        
    }
    public enum PopupType
    {
        None = 1,
        Menu = 2,
        PopupMSG = 3,
        PopupDownloding = 4,
        PopUpDownloadSize = 5
    }

    public class UIController : Singleton<UIController>
    {
        //public GenralText GenralText;
        public ScreenType StartScreen;
        public List<UIScreen> Screens;      
        public List<UIPopup> Popups;

        [SerializeField] ScreenType currentScreens;
        [SerializeField] List<UIPopup> currentPopup;
        //[HideInInspector]
        public ScreenType previousScreen;
        public static float AspectRatio;

        public override void OnAwake()
        {
            base.OnAwake();
            AspectRatio = Screen.width / (Screen.height * 1f);
        }

        private IEnumerator Start()
        {
            Application.targetFrameRate = Screen.currentResolution.refreshRate;
            
            yield return null;
            ShowScreen(StartScreen);

            yield return new WaitForSeconds(1f);

            //SavedDataHandler.instance.SetFirstLaunch();
        }
        
        public void ShowNextScreen(ScreenType screenType, float Delay = 0.2f)
        {
            //Events.ScreenChanged(false);
            if (currentScreens != ScreenType.None)
            {
                previousScreen = currentScreens;//.Last();
                HideScreen(previousScreen);
            }
            else
            {
                Delay = 0;
            }

            StartCoroutine(ExecuteAfterDelay(Delay, () =>
            {
                ShowScreen(screenType);
            }));
        }

        void ShowScreen(ScreenType screenType)
        {
            currentScreens = (screenType);
            getScreen(screenType).Show();
        }

        void HideScreen(ScreenType screenType)
        {
            getScreen(screenType).Hide();
            previousScreen = currentScreens;
        }

        public UIScreenView getScreen(ScreenType screenType)
        {
            return Screens.Find(screen => screen.screenType == screenType).screenView;
        }
        //public UIScreenView getScreen(ScreenType screenType)
        //{
        //    return Screens.Find(screen => screen.screenType == screenType).screenView;
        //}
        public ScreenType getCurrentScreen()
        {
            return currentScreens;
        }
        public UIPopupView GetPopup(PopupType popupType)
        {
            return Popups.Find(pop => pop.popupType == popupType).popupView;
        }
        UIPopup _popup = null;
        public void ShowPopup(PopupType popup)
        {
            Debug.Log(popup + " Show Call-Start");
            _popup = Popups.Find(x => x.popupType == popup);
            if (_popup == null) return;
            if (currentPopup.Contains(_popup)) return;
            //Events.ScreenChanged(false);
            //if (currentPopup.Count > 0)
            //{
            //    _popup.popupView.previousPopup = currentPopup.Last().popupView;
            //    _popup.popupView.previousPopup.ToggleRaycaster(false);
            //}
            //else
            //{
            //    _popup.popupView.previousPopup = getScreen(currentScreens);
            //    _popup.popupView.previousPopup.ToggleRaycaster(false);
            //}
            _popup.popupView.previousPopup = (currentPopup.Count == 0)?getScreen(currentScreens): currentPopup.Last().popupView;
            currentPopup.Add(_popup);
            _popup.popupView.Show();
            Debug.Log(popup + " Show Call-End");
        }
        //private void Update()
        //{
        //    OpenScreens();
        //    Debug.Log("POpup : "+currentPopup.Count);
        //}
        public void HidePopup(PopupType popupType)
        {
            Debug.Log("Start Hide Popup :"+ popupType + " || " + currentPopup.Count);
            //Events.ScreenChanged(false);
            GetPopup(popupType).Hide();
            currentPopup.Remove(currentPopup.Find(pop => pop.popupType == popupType));
            Debug.Log("End Hide Popup :" + popupType + " || " + currentPopup.Count);
            //if (currentPopup.Count == 0)
            //    Helper.Execute(this, () => getScreen(getCurrentScreen()).ToggleRaycaster(true), 0.8f);
        }
        
        IEnumerator ExecuteAfterDelay(float Delay, Action CallbackAction)
        {
            yield return new WaitForSeconds(Delay);

            CallbackAction();
        }
        public void OpenMsgBox()
        {
            //UIController.instance.ShowPopupMsg("Test", "Hello, Back Test", () => { Debug.LogError("Test Popup Close."); });
        }
        //public void ShowPopupMsg(string title, string msg, Action callback = null)
        //{
        //    GetPopup(PopupType.PopupMSG).GetComponent<PopupMsgUI>().SetMsg(title, msg, callback);
        //    ShowPopup(PopupType.PopupMSG);
        //}
        public void OpenMenuScreen()
        {
            ShowPopup(PopupType.Menu);
        }

        //public void ShowDownlodingPopup(string downloadAssetName)
        //{
        //    GetPopup(PopupType.PopupDownloding).GetComponent<PopupDownlodingUI>().DownlodAssetName = downloadAssetName;
        //    ShowPopup(PopupType.PopupDownloding);
        //}
        public void OpenScreens()
        {
            Debug.LogError("Opne screens : " + currentScreens);
        }
        public void ChangeOrientation(ScreenOrientation screenOrientation)
        {
            if(ScreenOrientation.Portrait == screenOrientation)
            {
                Screen.autorotateToPortrait = true;
                Screen.autorotateToPortraitUpsideDown = true;
                Screen.autorotateToLandscapeLeft = false;
                Screen.autorotateToLandscapeRight = false;
                Screen.orientation = ScreenOrientation.Portrait;
            }
            else
            {
                Screen.autorotateToPortrait = false;
                Screen.autorotateToPortraitUpsideDown = false;
                Screen.autorotateToLandscapeLeft = true;
                Screen.autorotateToLandscapeRight = true;
                Screen.orientation = ScreenOrientation.LandscapeLeft;
            }
            Helper.Execute(this,()=> Screen.orientation = ScreenOrientation.AutoRotation, 2f);
        }

        public async Task RefreshContent(ContentSizeFitter[] csf)
        {
            if(csf == null || csf.Length == 0) return;
            await Task.Delay(TimeSpan.FromSeconds(Time.deltaTime));
            foreach (var item in csf)
            {
                if (item == null) continue;
                item.enabled = false;
                await Task.Delay(TimeSpan.FromSeconds(Time.deltaTime));
                item.enabled = true;
            }
        }

        public string HtmlToStringParse(string mainStr)
        {
            mainStr = mainStr.Replace("<p>", "");
            mainStr = mainStr.Replace("</p>", "");
            mainStr = mainStr.Replace("<strong>", "<b>");
            mainStr = mainStr.Replace("</strong>", "</b>");
            mainStr = mainStr.Replace("&nbsp;", " ");
            return mainStr;
        }


        //public void ShowPopUpDownloadSize(string title, string msg, Action callback = null)
        //{
        //    GetPopup(PopupType.PopUpDownloadSize).GetComponent<ScreenPopDownloadSize>().SetMsg(title, msg, callback);
        //    ShowPopup(PopupType.PopUpDownloadSize);
        //}
        
    }

}