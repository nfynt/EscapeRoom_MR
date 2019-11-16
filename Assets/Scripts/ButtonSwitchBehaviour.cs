using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nfynt.Components
{
    [RequireComponent(typeof(AudioSource))]
    public class ButtonSwitchBehaviour : MonoBehaviour
    {
        public enum State
        {
            ON,
            OFF
        };

        /// <summary>
        /// Delegate to define the switch state for On and off
        /// </summary>
        /// <param name="isOn"></param>
        public delegate void SwitchStateChanged(bool isOn);
        public event SwitchStateChanged SwitchState;

        public State currState = State.OFF;
        public Transform buttonObj;
        public float angleChange = 20f;

        private AudioSource audSrc;
        
        private void Start()
        {
            audSrc = GetComponent<AudioSource>();
            InitBehaviour();
        }

        void InitBehaviour()
        {
            if (currState == State.OFF)
            {
                buttonObj.transform.localRotation = Quaternion.Euler(-buttonObj.right * angleChange);
            }
            else
            {
                buttonObj.transform.localRotation = Quaternion.Euler(buttonObj.right * angleChange);
            }
            audSrc.Stop();
            audSrc.loop = false;
        }

        public void ToggleSwitchState()
        {
            if(currState==State.OFF)
            {
                currState = State.ON;
                buttonObj.transform.localRotation = Quaternion.Euler(buttonObj.right * angleChange);
                SwitchState.Invoke(true);
                //audSrc.Play();
                AudioManager.Instance.PlayClip(AudioManager.ClipType.HEAVY_BUTTON_CLICK, audSrc, 0.5f);
            }
            else
            {
                currState = State.OFF;
                buttonObj.transform.localRotation = Quaternion.Euler(-buttonObj.right * angleChange);
                SwitchState.Invoke(false);
                //audSrc.Play();
                AudioManager.Instance.PlayClip(AudioManager.ClipType.HEAVY_BUTTON_CLICK, audSrc, 0.5f);
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
