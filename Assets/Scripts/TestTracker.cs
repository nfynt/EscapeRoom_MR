using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace Nfynt.Tracking
{
    public class TestTracker : MonoBehaviour
    {
        public Transform stoolObj;
        public InputDeviceRole controllerRole;
        public int ind = 0;

        private List<InputDevice> tracker = new List<InputDevice>();

        private void Start()
        {

            tracker = ViveTracker.Instance.GetTrackedDevices(controllerRole);
            Debug.Log(tracker.Count.ToString() + " devices found of type: " + controllerRole.ToString());
        }

        void Update()
        {
            if (tracker.Count > ind && tracker[ind].isValid)
            {
                tracker[ind].TryGetFeatureValue(CommonUsages.devicePosition, out var pos);
                stoolObj.position = pos;
                tracker[ind].TryGetFeatureValue(CommonUsages.deviceRotation, out var rot);
                stoolObj.rotation = rot;

                Debug.Log(pos);
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
