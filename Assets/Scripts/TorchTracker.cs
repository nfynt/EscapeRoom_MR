using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Nfynt.Components;

namespace Nfynt.Tracking
{
    public class TorchTracker : Singleton<TorchTracker>
    {
        public Transform torchObj;
        public InputDeviceRole controllerRole;
        public int ind = 0;
        public TorchBehaviour tb;

        private List<InputDevice> tracker = new List<InputDevice>();
        private bool pressedState;

        private void Start()
        {
            tracker = ViveTracker.Instance.GetTrackedDevices(controllerRole);
        }

        void Update()
        {
            if (tracker.Count>ind && tracker[ind].isValid && torchObj != null)
            {
                if (tracker[ind].TryGetFeatureValue(CommonUsages.devicePosition, out var pos))
                    torchObj.position = pos;
                if (tracker[ind].TryGetFeatureValue(CommonUsages.deviceRotation, out var rot))
                    torchObj.rotation = rot;

                //float triggerVal=0f;
                if (tracker[ind].TryGetFeatureValue(CommonUsages.primary2DAxisClick, out var touch) && touch)
                {
                    //Debug.Log("Touch Pressed: " + touch.ToString());
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
            else
            {
                if (tracker.Count > ind)
                    tracker = ViveTracker.Instance.GetTrackedDevices(controllerRole);
                else
                {
                    ViveTracker.Instance.RefreshDeviceList();
                    tracker = ViveTracker.Instance.GetTrackedDevices(controllerRole);
                    Debug.LogWarning("Invalid index number");
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
