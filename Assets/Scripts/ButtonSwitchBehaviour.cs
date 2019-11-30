using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nfynt.Components
{
    [RequireComponent(typeof(AudioSource))]
    public class ButtonSwitchBehaviour : MonoBehaviour, IPowerDevice
    {
        enum State
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

        public Transform buttonObj;
        public float angleChange = 20f;

        private State currState = State.OFF;
        private AudioSource audSrc;
        private bool mainsOn;
        
        private void Start()
        {
            audSrc = GetComponent<AudioSource>();
            InitBehaviour();
        }

        void InitBehaviour()
        {
            if (currState == State.OFF)
            {
                buttonObj.transform.rotation = Quaternion.Euler(-buttonObj.right * angleChange);
            }
            else
            {
                buttonObj.transform.rotation = Quaternion.Euler(buttonObj.right * angleChange);
            }
            audSrc.Stop();
            audSrc.loop = false;
            if (PowerSupplyBehaviour.Instance != null)
                PowerSupplyBehaviour.Instance.AddDevice(this);
            else
            {
                PowerSupplyBehaviour pb = FindObjectOfType<PowerSupplyBehaviour>();
                pb.AddDevice(this);
            }
        }

        public void ToggleSwitchState()
        {
            if(currState==State.OFF)
            {
                currState = State.ON;
                buttonObj.transform.localRotation = Quaternion.Euler(buttonObj.right * angleChange);
                if (mainsOn)
                    SwitchState.Invoke(true);
                //audSrc.Play();
                AudioManager.Instance.PlayClip(AudioManager.ClipType.TORCH_BUTTON_CLICK, audSrc, 0.5f);
            }
            else
            {
                currState = State.OFF;
                buttonObj.transform.localRotation = Quaternion.Euler(-buttonObj.right * angleChange);
                if (mainsOn)
                    SwitchState.Invoke(false);
                //audSrc.Play();
                AudioManager.Instance.PlayClip(AudioManager.ClipType.TORCH_BUTTON_CLICK, audSrc, 0.5f);
            }
        }

        public void PowerSourceStateChanged(bool isOn)
        {
            mainsOn = isOn;
            if (currState== State.ON && !isOn)
            {
                SwitchState.Invoke(false);
            }
            else if(currState == State.ON && isOn)
            {
                SwitchState.Invoke(true);
            }
        }

        public void SetMainsState(bool isOn)
        {
            mainsOn = isOn;
        }
    }
}




/*
 __  _ _____   ____  _ _____  
|  \| | __\ `v' /  \| |_   _| 
| | ' | _| `. .'| | ' | | |   
|_|\__|_|   !_! |_|\__| |_|
 
*/
