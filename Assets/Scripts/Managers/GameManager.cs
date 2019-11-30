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
            StartCoroutine(ToggleVR(GameSettings.mainSceneName));
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

        private void Update()
        {
            //Reload current scene on Ctrl + R
            if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyUp(KeyCode.R))
            {
                StartCoroutine(ResetCurrentLevel());
            }
        }

        /// <summary>
        /// Switch to non vr mode and load home scene
        /// </summary>
        public void SwitchToNonVRAndLoadHome()
        {
            StartCoroutine(ToggleVR(GameSettings.menuSceneName));
        }

        IEnumerator ResetCurrentLevel()
        {
            Debug.Log("Reloading current scene");
            string currScene = SceneManager.GetActiveScene().name;

            AsyncOperation unload = SceneManager.LoadSceneAsync(currScene);
            Resources.UnloadUnusedAssets();
            yield return new WaitUntil(() => !unload.isDone);

            Debug.Log(currScene + " Unloaded!");

            AsyncOperation load = SceneManager.LoadSceneAsync(currScene,LoadSceneMode.Single);
            yield return new WaitUntil(() => !load.isDone);

            Debug.Log(currScene + " Loaded!");

        }
    }
}




/*
 __  _ _____   ____  _ _____  
|  \| | __\ `v' /  \| |_   _| 
| | ' | _| `. .'| | ' | | |   
|_|\__|_|   !_! |_|\__| |_|
 
*/
