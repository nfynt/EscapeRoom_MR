using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Nfynt.Components
{
    public class ComputerController : MonoBehaviour, IPowerDevice
    {
        enum State
        {
            OFF,
            SCREENSAVER,
            LOGIN,
            END
        };

        public GameObject screenPanel;
        public GameObject screenSaver;
        public GameObject loginScreen;
        public GameObject loggedScreen;

        public VideoPlayer vidPlyr;
        public TMP_InputField passwordInput;
        public TextMeshProUGUI errorTxt;
        public AudioSource audSrc;

        [SerializeField]
        private string userPassword = "1234";
        private string currPass="";
        private State currState = State.OFF;
        private bool mainsOn;
        private AudioManager audMgr;

        void Start()
        {
            if (PowerSupplyBehaviour.Instance != null)
                PowerSupplyBehaviour.Instance.AddDevice(this);
            else
            {
                PowerSupplyBehaviour pb = FindObjectOfType<PowerSupplyBehaviour>();
                pb.AddDevice(this);
            }
            audMgr = AudioManager.Instance;
        }

        public void KeyPressed(string key)
        {
            //Debug.Log("Key Pressed: " + key);

            if (!mainsOn) return;

            if(currState==State.SCREENSAVER)
            {
                UpdateState(State.LOGIN);
                return;
            }

            if (currState != State.LOGIN) return;

            switch (key)
            {
                case "BSP":
                    if (currPass.Length > 0)
                        currPass = currPass.Substring(0, currPass.Length - 1);
                    break;
                case "RTN":
                    if(currPass.CompareTo(userPassword)==0)
                    {
                        //login success
                        UpdateState(State.END);
                        audMgr.PlayClip(AudioManager.ClipType.LOGIN, audSrc);
                    }
                    else
                    {
                        //login failed
                        errorTxt.text = "last attempt failed!";
                        audMgr.PlayClip(AudioManager.ClipType.LOGINFAILED, audSrc);
                    }
                    currPass = "";
                    break;
                default:
                    currPass += key;
                    break;
            }
            passwordInput.text = currPass;
        }

        void UpdateState(State newState)
        {
            switch(newState)
            {
                case State.OFF:
                    screenPanel.SetActive(false);
                    vidPlyr.Stop();
                    currPass = "";
                    break;
                case State.SCREENSAVER:
                    screenPanel.SetActive(true);
                    screenSaver.SetActive(true);
                    loginScreen.SetActive(false);
                    loggedScreen.SetActive(false);
                    vidPlyr.Play();
                    break;
                case State.LOGIN:
                    screenSaver.SetActive(false);
                    loginScreen.SetActive(true);
                    vidPlyr.Stop();
                    currPass = "";
                    errorTxt.text = "";
                    passwordInput.text = "";
                    break;
                case State.END:
                    loginScreen.SetActive(false);
                    loggedScreen.SetActive(true);
                    break;
            }
            currState = newState;
        }

        public void PowerSourceStateChanged(bool isOn)
        {
            mainsOn = isOn;
            if (isOn)
                UpdateState(State.SCREENSAVER);
            else
                UpdateState(State.OFF);
        }

        public void SetMainsState(bool isOn)
        {
            mainsOn = isOn;
            if (isOn)
                UpdateState(State.SCREENSAVER);
            else
                UpdateState(State.OFF);
        }
    }
}




/*
 __  _ _____   ____  _ _____  
|  \| | __\ `v' /  \| |_   _| 
| | ' | _| `. .'| | ' | | |   
|_|\__|_|   !_! |_|\__| |_|
 
*/

