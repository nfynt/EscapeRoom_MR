using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;

namespace Nfynt.Components
{
    public class KeyboardController : MonoBehaviour
    {
        public string currStr;
        
        private void OnEnable()
        {
            foreach(InteractionBehaviour ib in transform.GetComponentsInChildren<InteractionBehaviour>())
            {
                ib.OnContactEnd += delegate { KeyPressed(ib.gameObject.name); };
            }
        }

        public void KeyPressed(string key)
        {
            //Debug.Log("Key Pressed: " + key);
            switch(key)
            {
                case "BSP":
                    currStr = currStr.Substring(0, currStr.Length - 1);
                    break;
                case "RTN":
                    currStr = "";
                    break;
                default:
                    currStr += key;
                    break;
            }
        }
    }
}




/*
 __  _ _____   ____  _ _____  
|  \| | __\ `v' /  \| |_   _| 
| | ' | _| `. .'| | ' | | |   
|_|\__|_|   !_! |_|\__| |_|
 
*/

