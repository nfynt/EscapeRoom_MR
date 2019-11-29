using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Nfynt.Components;

namespace Nfynt.Tracking
{
    public class TorchTracker : Singleton<TorchTracker>
    {
        static List<InputDevice> devices = new List<InputDevice>();

        public Transform torchObj;
        public InputDeviceRole controllerRole;
        public TorchBehaviour tb;

        private InputDevice controller;
        private bool pressedState;

        void Update()
        {
            InputDevices.GetDevicesWithRole(controllerRole, devices);
            if (devices.Count > 0)
                controller = devices[0];

            if (controller != null && torchObj != null)
            {
                Vector3 pos;
                Quaternion rot;
                if (controller.TryGetFeatureValue(CommonUsages.devicePosition, out pos))
                    torchObj.position = pos;
                if (controller.TryGetFeatureValue(CommonUsages.deviceRotation, out rot))
                    torchObj.rotation = rot;

                //float triggerVal=0f;
                bool touch = false;
                if (controller.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out touch) && touch)
                {
                    Debug.Log("Touch Pressed: " + touch.ToString());
                    // Debug.Log("Trigger Pressed: "+triggerVal.ToString());
                    if (!pressedState)
                    {
                        tb.TorchButtonPressed();
                        pressedState = true;
                    }
                }else if (pressedState)
                {
                    pressedState = false;
                }
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
