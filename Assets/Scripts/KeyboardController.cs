using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;
using Nfynt;

namespace Nfynt.Components
{
    public class KeyboardController : MonoBehaviour
    {
        public ComputerController compController;

        private AudioManager audMgr;

        private void Start()
        {
            audMgr = AudioManager.Instance;
        }

        private void OnEnable()
        {
            foreach(InteractionBehaviour ib in transform.GetComponentsInChildren<InteractionBehaviour>())
            {
                ib.OnContactEnd += delegate { KeyReleased(ib.gameObject.name); };
                ib.OnContactBegin += delegate { KeyPressed(); };
            }
        }

        public void KeyPressed()
        {
            audMgr.PlayClip(AudioManager.ClipType.KEYBOARDKEYPRESS, compController.audSrc, 0.5f);
        }

        public void KeyReleased(string key)
        {
            compController.KeyPressed(key);
            audMgr.PlayClip(AudioManager.ClipType.KEYBOARDKEYPRESS, compController.audSrc, 0.5f);
        }

        private void OnDisable()
        {
            foreach (InteractionBehaviour ib in transform.GetComponentsInChildren<InteractionBehaviour>())
            {
                ib.OnContactEnd -= delegate { KeyReleased(ib.gameObject.name); };
                ib.OnContactBegin -= delegate { KeyPressed(); };
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

