using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using Nfynt;

namespace Nfynt.Managers
{
    /// <summary>
    /// To control main menu UI and settings
    /// </summary>
    public class MainMenuManager : Singleton<MainMenuManager>
    {
        [Header("UI References")]
        public Button startBtn;
        public Button creditsBtn;
        public Button quitBtn;
        public Button creditBackBtn;
        public GameObject creditContainer;
        public Toggle musicToggle;
        public Toggle sfxToggle;

        [Header("Others")]
        public AudioSource ambientAudio;

        private AudioManager audMgr;

        private void Start()
        {
            InitiUIElements();
            GameSettings.Instance.ApplyDefaultSettings();
            audMgr = AudioManager.Instance;
            audMgr.SetAmbientAudioSrc(ambientAudio);
        }

        void InitiUIElements()
        {
            startBtn.onClick.RemoveAllListeners();
            creditsBtn.onClick.RemoveAllListeners();
            quitBtn.onClick.RemoveAllListeners();
            creditBackBtn.onClick.RemoveAllListeners();
            musicToggle.onValueChanged.RemoveAllListeners();
            sfxToggle.onValueChanged.RemoveAllListeners();

            startBtn.onClick.AddListener(StartGame);
            creditsBtn.onClick.AddListener(ToggleCredits);
            creditBackBtn.onClick.AddListener(ToggleCredits);
            quitBtn.onClick.AddListener(QuitApplication);
            musicToggle.onValueChanged.AddListener((value) => { ToggleMusic(value); });
            sfxToggle.onValueChanged.AddListener((value) => { ToggleSFX(value); });
        }

        void StartGame()
        {
            GameManager.Instance.SwitchToVRAndLoadMainScene();
            audMgr.PlayClip(AudioManager.ClipType.UI_BUTTON_CLICK);
        }

        void ToggleCredits()
        {
            creditContainer.SetActive(!creditContainer.activeSelf);
            audMgr.PlayClip(AudioManager.ClipType.UI_BUTTON_CLICK);
        }

        void QuitApplication()
        {
            Application.Quit();
        }

        void ToggleMusic(bool value)
        {
            GameSettings.isMusicOn = value;
            audMgr.SetAmbientAudioSrc(ambientAudio);
            audMgr.PlayClip(AudioManager.ClipType.UI_BUTTON_CLICK);
        }

        void ToggleSFX(bool value)
        {
            GameSettings.isSFXOn = value;
            audMgr.PlayClip(AudioManager.ClipType.UI_BUTTON_CLICK);
        }
    }
}




/*
 __  _ _____   ____  _ _____  
|  \| | __\ `v' /  \| |_   _| 
| | ' | _| `. .'| | ' | | |   
|_|\__|_|   !_! |_|\__| |_|
 
*/
