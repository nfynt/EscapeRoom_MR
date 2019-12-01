using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nfynt.Components;

namespace Nfynt.Managers
{
    public class LightController : MonoBehaviour
    {
        public List<Light> roomLight=new List<Light>();
        public ButtonSwitchBehaviour bsb;

        private void Start()
        {
            ToggleLight(false);
        }

        private void OnEnable()
        {
            bsb.SwitchState += ToggleLight;
        }

        public void ToggleLight(bool isOn)
        {
            foreach(Light ll in roomLight)
            ll.enabled = isOn;
        }

        private void OnDisable()
        {
            bsb.SwitchState -= ToggleLight;
        }

    }
}




/*
 __  _ _____   ____  _ _____  
|  \| | __\ `v' /  \| |_   _| 
| | ' | _| `. .'| | ' | | |   
|_|\__|_|   !_! |_|\__| |_|
 
*/
