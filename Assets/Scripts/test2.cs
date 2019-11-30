using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class test2 : MonoBehaviour
{
    static List<InputDevice> devices = new List<InputDevice>();

    public Transform torchObj;
    public InputDeviceRole controllerRole;
    public Transform trackedObj1;

    private InputDevice controller;
    private InputDevice tracker;

    void Start()
    {
        InputDevices.GetDevices(devices);

        foreach (var device in devices)
        {
            Debug.Log(string.Format("Device found with name '{0}' and role '{1}'", device.name, device.role.ToString()));
        }

        controller = devices.Find(d => d.role == controllerRole);
        tracker = devices.Find(d => d.role == InputDeviceRole.HardwareTracker);
    }

    void Update()
    {
        InputDevices.GetDevicesWithRole(controllerRole, devices);
        if (devices.Count > 0)
            controller = devices[0];

        if (tracker != null && trackedObj1 != null)
        {
            tracker.TryGetFeatureValue(CommonUsages.devicePosition, out var pos);
            trackedObj1.position = pos;
            tracker.TryGetFeatureValue(CommonUsages.deviceRotation, out var rot);
            trackedObj1.rotation = rot;
        }

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
            if (controller.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out touch))
            {
                Debug.Log("Touch Pressed: " + touch.ToString());
                // Debug.Log("Trigger Pressed: "+triggerVal.ToString());
            }
        }

    }
}
