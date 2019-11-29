using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace Nfynt.Tracking
{
    public class StoolTracker : Singleton<StoolTracker>
    {
        static List<InputDevice> devices = new List<InputDevice>();

        public Transform stoolObj;
        public InputDeviceRole controllerRole;

        private InputDevice controller;
        private InputDevice tracker;

        private void Start()
        {
            //InputDevices.GetDevicesWithRole(controllerRole, devices);
            //if (devices.Count > 0)
            //    controller = devices[0];

            var allDevices = new List<InputDevice>();
            InputDevices.GetDevices(allDevices);
            tracker = allDevices.Find(d => d.role == controllerRole);
        }

        void Update()
        {
            if (tracker != null)
            {
                tracker.TryGetFeatureValue(CommonUsages.devicePosition, out var pos);
                stoolObj.position = pos;
                tracker.TryGetFeatureValue(CommonUsages.deviceRotation, out var rot);
                stoolObj.rotation = rot;

                Debug.Log(pos);
            }
            //if (controller != null && stoolObj != null)
            //{
            //    Vector3 pos;
            //    Quaternion rot;
            //    controller.TryGetFeatureValue(CommonUsages.devicePosition, out pos);
            //        stoolObj.position = pos;
            //    controller.TryGetFeatureValue(CommonUsages.deviceRotation, out rot);
            //        stoolObj.rotation = rot;

            //}

        }
    }
}




/*
 __  _ _____   ____  _ _____  
|  \| | __\ `v' /  \| |_   _| 
| | ' | _| `. .'| | ' | | |   
|_|\__|_|   !_! |_|\__| |_|
 
*/
