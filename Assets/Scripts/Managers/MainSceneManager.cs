using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEngine.XR;
#endif

namespace Nfynt.Managers
{
    public class MainSceneManager : Singleton<MainSceneManager>
    {

        [Header("Others")]
        public AudioSource ambientAudio;

        private AudioManager audMgr;

        private void Awake()
        {
#if UNITY_EDITOR
            StartCoroutine(SwitchVRMode());
#endif
        }

        private void Start()
        {
            audMgr = AudioManager.Instance;
            audMgr.SetAmbientAudioSrc(ambientAudio);
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
