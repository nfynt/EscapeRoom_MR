using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

namespace Nfynt
{
    /// <summary>
    /// Singleton persistant class that controls the entire game
    /// </summary>
    public class GameManager : SingletonPersistent<GameManager>
    {
        private bool vrmodeEnabled;

        private void Start()
        {
            vrmodeEnabled = false;
        }

        /// <summary>
        /// Switch to VR and load main scene
        /// </summary>
        public void SwitchToVRAndLoadMainScene()
        {
            StartCoroutine(ToggleVR("Main"));
        }

        IEnumerator ToggleVR(string sceneName)
        {
            if (vrmodeEnabled)
                XRSettings.LoadDeviceByName("None");
            else
                XRSettings.LoadDeviceByName("OpenVR");
            yield return null;
            XRSettings.enabled = true;
            yield return null;
            vrmodeEnabled = !vrmodeEnabled;

            AsyncOperation load = SceneManager.LoadSceneAsync(sceneName);
            yield return new WaitUntil(() => !load.isDone);
            Debug.Log(sceneName + " loaded");
        }

        /// <summary>
        /// Switch to non vr mode and load home scene
        /// </summary>
        public void SwitchToNonVRAndLoadHome()
        {
            StartCoroutine(ToggleVR("Home"));
        }
    }
}




/*
 __  _ _____   ____  _ _____  
|  \| | __\ `v' /  \| |_   _| 
| | ' | _| `. .'| | ' | | |   
|_|\__|_|   !_! |_|\__| |_|
 
*/
