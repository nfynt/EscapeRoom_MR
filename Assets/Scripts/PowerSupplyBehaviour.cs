using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Examples;

namespace Nfynt.Components
{
    public class PowerSupplyBehaviour : Singleton<PowerSupplyBehaviour>
    {
        public enum PSState
        {
            OFF, ON
        }

        public Transform handle;
        public TransformTool tTool;

        private PSState currState;
        private float currHandleRot;
        private float onZAng = 150f;
        private float offZAnf = 10f;
        private bool checkAng;
        private AudioManager audMgr;

        private List<IPowerDevice> powerDevices;

        private void Awake()
        {
            powerDevices = new List<IPowerDevice>();
        }

        private void Start()
        {
#if !UNITY_EDITOR
            currState = PSState.OFF;
#endif
            currHandleRot = handle.localRotation.eulerAngles.z;
            if (tTool == null)
                tTool = GetComponentInChildren<TransformTool>();
            checkAng = false;
            audMgr = AudioManager.Instance;

            SwitchState(currState,true);
        }

        public void AddDevice(IPowerDevice device)
        {
            if (powerDevices.Contains(device))
                return;

            powerDevices.Add(device);
            if (currState == PSState.ON)
                device.SetMainsState(true);
            else
                device.SetMainsState(false);
        }

        private void Update()
        {
            if(checkAng)
            {
                currHandleRot = handle.localRotation.eulerAngles.z;
                if(currHandleRot<5f || currHandleRot>155f)
                {
                    if (currHandleRot < 90f)
                        SwitchState(PSState.OFF);
                    else
                        SwitchState(PSState.ON);
                }
            }
        }

        public void SwitchState(PSState state, bool skipAud=false)
        {
            if (state == PSState.ON)
            {
                handle.localRotation = Quaternion.Euler(0f, 0f, onZAng);
                foreach (IPowerDevice ip in powerDevices)
                    ip.PowerSourceStateChanged(true);
            }
            else
            {
                handle.localRotation = Quaternion.Euler(0f, 0f, offZAnf);
                foreach (IPowerDevice ip in powerDevices)
                    ip.PowerSourceStateChanged(false);
            }

            tTool.enabled = false;
            audMgr.PlayClip(AudioManager.ClipType.PS_LEVER_STATE);

        }

        public void ToggleCheckAngle(bool val)
        {
            checkAng = val;
            if (!val)
            {
                if (currHandleRot < 90f)
                    SwitchState(PSState.OFF);
                else
                    SwitchState(PSState.ON);

            }
        }

        public void EnableTransTool()
        {
            tTool.enabled = true;
        }
    }
}




/*
 __  _ _____   ____  _ _____  
|  \| | __\ `v' /  \| |_   _| 
| | ' | _| `. .'| | ' | | |   
|_|\__|_|   !_! |_|\__| |_|
 
*/
