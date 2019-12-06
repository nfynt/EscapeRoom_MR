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
        public float coolTime = 1f;

        private AudioManager audMgr;
        private bool cooling = false;

        private void Start()
        {
            audMgr = AudioManager.Instance;
            cooling = false;
        }

        private void OnEnable()
        {
            foreach(InteractionBehaviour ib in transform.GetComponentsInChildren<InteractionBehaviour>())
            {
                ib.OnContactEnd += delegate { KeyReleased(); };
                ib.OnContactBegin += delegate { KeyPressed(ib.gameObject.name); };
            }
        }

        public void KeyPressed(string key)
        {
            if (cooling) return;

            compController.KeyPressed(key);
            audMgr.PlayClip(AudioManager.ClipType.KEYBOARDKEYPRESS, compController.audSrc, 0.5f);
            cooling = true;
            Invoke("SetCoolingOff", 1.5f);
        }

        public void KeyReleased()
        {
            audMgr.PlayClip(AudioManager.ClipType.KEYBOARDKEYPRESS, compController.audSrc, 0.5f);
        }

        private void OnDisable()
        {
            foreach (InteractionBehaviour ib in transform.GetComponentsInChildren<InteractionBehaviour>())
            {
                ib.OnContactEnd -= delegate { KeyReleased(); };
                ib.OnContactBegin -= delegate { KeyPressed(ib.gameObject.name); };
            }

            CancelInvoke();
        }

        void SetCoolingOff()
        {
            cooling = false;
        }
    }
}




/*
 __  _ _____   ____  _ _____  
|  \| | __\ `v' /  \| |_   _| 
| | ' | _| `. .'| | ' | | |   
|_|\__|_|   !_! |_|\__| |_|
 
*/

