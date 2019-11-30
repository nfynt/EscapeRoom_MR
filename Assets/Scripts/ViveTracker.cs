using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace Nfynt.Tracking
{
    public class ViveTracker : Singleton<ViveTracker>
    {
        static List<InputDevice> devices = new List<InputDevice>();

        void Start()
        {
            InputDevices.GetDevices(devices);

            foreach (var device in devices)
            {
                Debug.Log(string.Format("Device found with name '{0}' and role '{1}'", device.name, device.role.ToString()));
            }
        }

        public List<InputDevice> GetTrackedDevices(InputDeviceRole role)
        {
            List<InputDevice> dev = new List<InputDevice>();
            
            foreach (InputDevice id in devices)
                if (id.role == role)
                    dev.Add(id);

            return dev;
        }

        public void RefreshDeviceList()
        {
            InputDevices.GetDevices(devices);
            foreach (var device in devices)
            {
                Debug.Log(string.Format("Device found with name '{0}' and role '{1}'", device.name, device.role.ToString()));
            }
        }

        public InputDevice GetFirstDevice(InputDeviceRole role)
        {
            InputDevice dev = devices.Find(d => d.role == role);
            return dev;
        }
    }
}




/*
 __  _ _____   ____  _ _____  
|  \| | __\ `v' /  \| |_   _| 
| | ' | _| `. .'| | ' | | |   
|_|\__|_|   !_! |_|\__| |_|
 
*/
