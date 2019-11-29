using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class test2 : MonoBehaviour
{
    static List<InputDevice> devices = new List<InputDevice>();
    [SerializeField]
    InputDeviceRole role;

    // Update is called once per frame
    void Update()
    {
        InputDevices.GetDevicesWithRole(role, devices);
        if (devices.Count > 0)
        {
            InputDevice device = devices[0];
            Vector3 position;
            if (device.TryGetFeatureValue(CommonUsages.devicePosition, out position))
                this.transform.position = position;
            Quaternion rotation;
            if (device.TryGetFeatureValue(CommonUsages.deviceRotation, out rotation))
                this.transform.rotation = rotation;
        }
    }
}
