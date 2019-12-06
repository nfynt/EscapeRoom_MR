using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nfynt.Components;
#if UNITY_EDITOR
using UnityEngine.XR;
#endif

namespace Nfynt.Managers
{
    public class MainSceneManager : Singleton<MainSceneManager>
    {

        [Header("Others")]
        public AudioSource ambientAudio;
        public DoorController doorController;
        public GameObject playSceneObj;
        public GameObject endSceneObj;

        private AudioManager audMgr;
        private bool puzzleSolved = false;
        private bool pcUnlocked = false;

        private void Awake()
        {
#if UNITY_EDITOR
            StartCoroutine(SwitchVRMode());
#endif
        }

        private void Start()
        {
            endSceneObj.SetActive(false);
            playSceneObj.SetActive(true);
            audMgr = AudioManager.Instance;
            audMgr.SetAmbientAudioSrc(ambientAudio);
            puzzleSolved = false;
            pcUnlocked = false;
        }

        public void PuzzleSolved()
        {
            puzzleSolved = true;
            Debug.Log("PuzzleSolved");
            if (pcUnlocked)
                doorController.OpenDoor();
        }

        public void ComputerUnlocked()
        {
            pcUnlocked = true;
            Debug.Log("Computer Unlocked");
            if (puzzleSolved)
                doorController.OpenDoor();
        }

        public void BreakTheWall()
        {
            doorController.BreakTheWall();
            Debug.Log("Breaknig the wall!");
        }

        public void BlockedWallDestroyed()
        {
            playSceneObj.SetActive(false);
            endSceneObj.SetActive(true);
        }

        public void GameFinished()
        {
            Debug.Log("Game Finished!");
            GameManager.Instance.SwitchToNonVRAndLoadHome();
        }

#if UNITY_EDITOR
        IEnumerator SwitchVRMode()
        {
            XRSettings.LoadDeviceByName("OpenVR");
            yield return null;
            XRSettings.enabled = true;
        }
#endif
    }
}




/*
 __  _ _____   ____  _ _____  
|  \| | __\ `v' /  \| |_   _| 
| | ' | _| `. .'| | ' | | |   
|_|\__|_|   !_! |_|\__| |_|
 
*/
