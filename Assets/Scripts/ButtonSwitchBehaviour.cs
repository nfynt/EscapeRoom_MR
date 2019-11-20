using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nfynt.Behaviours
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
        public AudioClip buttonPressClip;

        private AudioSource audSrc;

        void InitBehaviour()
        {
            if(currState==State.OFF)
            {
                buttonObj.transform.localRotation = Quaternion.Euler(-buttonObj.right * angleChange);
            }
            else
            {
                buttonObj.transform.localRotation = Quaternion.Euler(buttonObj.right * angleChange);
            }
            audSrc.clip = buttonPressClip;
            audSrc.loop = false;
        }

        private void Start()
        {
            audSrc = GetComponent<AudioSource>();
            InitBehaviour();
        }

        public void ToggleSwitchState()
        {
            if(currState==State.OFF)
            {
                currState = State.ON;
                buttonObj.transform.localRotation = Quaternion.Euler(buttonObj.right * angleChange);
                SwitchState.Invoke(true);
                audSrc.Play();

            }
            else
            {
                currState = State.OFF;
                buttonObj.transform.localRotation = Quaternion.Euler(-buttonObj.right * angleChange);
                SwitchState.Invoke(false);
                audSrc.Play();
            }
        }
    }
}