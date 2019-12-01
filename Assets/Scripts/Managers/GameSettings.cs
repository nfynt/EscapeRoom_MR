using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nfynt;

namespace Nfynt
{
    public class GameSettings : SingletonPersistent<GameSettings>
    {
        /// <summary>
        /// Audio effects
        /// </summary>
        public static bool isSFXOn=true;
        /// <summary>
        /// Ambient music
        /// </summary>
        public static bool isMusicOn=true;

        public void ApplyDefaultSettings()
        {
            isSFXOn = true;
            isMusicOn = true;
        }
        public static string mainSceneName="MainScene";
        public static string menuSceneName = "Home";
    }
}




/*
 __  _ _____   ____  _ _____  
|  \| | __\ `v' /  \| |_   _| 
| | ' | _| `. .'| | ' | | |   
|_|\__|_|   !_! |_|\__| |_|
 
*/
