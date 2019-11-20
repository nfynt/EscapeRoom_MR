using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nfynt.Behaviours;

namespace Nfynt.Managers
{
    public class LightManager : MonoBehaviour
    {
        public Light roomLight;
        public ButtonSwitchBehaviour bsb;

        private void Start()
        {
            roomLight.enabled = false;
        }

        private void OnEnable()
        {
            bsb.SwitchState += ToggleLight;
        }

        public void ToggleLight(bool isOn)
        {
            roomLight.enabled = isOn;
        }

        private void OnDisable()
        {
            bsb.SwitchState -= ToggleLight;
        }

    }
}