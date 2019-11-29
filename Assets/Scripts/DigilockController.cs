using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Nfynt.Components
{
    public class DigilockController : MonoBehaviour, IPowerDevice
    {
        public enum SafeState
        {
            OPEN, CLOSED
        }
        public float doorOpenAngle = 150f;

        [Header("Object References")]
        [SerializeField]
        private Transform lockerDoor;
        [SerializeField]
        private TextMeshPro digitalDisplay;
        [Space(10)]
        [SerializeField]
        private string passKey = "1234";
        private SafeState currState;
        private AudioManager audMgr;
        private string currStr = "";
        private bool mainsOn;

        private void Start()
        {
            audMgr = AudioManager.Instance;
            currStr = "";
            digitalDisplay.text = currStr;
            currState = SafeState.CLOSED;
            if (PowerSupplyBehaviour.Instance != null)
                PowerSupplyBehaviour.Instance.AddDevice(this);
            else
            {
                PowerSupplyBehaviour pb = FindObjectOfType<PowerSupplyBehaviour>();
                pb.AddDevice(this);
            }
        }

        void InitDigiLock()
        {
            currStr = "_";
            digitalDisplay.text = currStr;
            InvokeRepeating("UpdateDisplay", 1f, 1f);
        }

        void DInitDigiLock()
        {
            currStr = "";
            digitalDisplay.text = currStr;
            CancelInvoke();
        }

        void UpdateState()
        {
            if(currState== SafeState.CLOSED)
            {
                lockerDoor.Rotate(Vector3.up * doorOpenAngle, Space.Self);
                currState = SafeState.OPEN;
            }
            else
            {
                lockerDoor.Rotate(Vector3.zero);
                currState = SafeState.CLOSED;
            }
        }

        /*int n = 0;
        void Update()
        {
            if(Input.GetKeyUp(KeyCode.Q))
            {
                if (n == 0)
                    KeyTouched("1");
                else if (n == 1)
                    KeyTouched("2");
                else if (n == 2)
                    KeyTouched("3");
                else if (n == 3)
                    KeyTouched("4");
                else
                {
                    n = 0;
                    return;
                }
                n++;
            }
            if (Input.GetKeyUp(KeyCode.W))
                InitDigiLock();

            if (Input.GetKeyUp(KeyCode.Return))
                KeyTouched("ENT");
        }*/

        public void KeyTouched(string character)
        {
            if (!mainsOn) return;

            switch (character)
            {
                case "ESC":
                    if (currStr.EndsWith("_"))
                        currStr = currStr.Substring(0, currStr.Length - 2) + "_";
                    else
                        currStr = currStr.Substring(0, currStr.Length - 1) + "_";
                    break;
                case "ENT":
                    CheckForPass();
                    break;
                default:
                    if (currStr.Length <= 1)
                        currStr = character;
                    else if (currStr.Length < 4)
                    {
                        if (currStr.EndsWith("_"))
                            currStr = currStr.Substring(0, currStr.Length - 1) + character;
                        else
                            currStr += character;
                    }
                    else if (currStr.Length == 4 && currStr.EndsWith("_"))
                        currStr = currStr.Substring(0, currStr.Length - 1) + character;
                    break;
            }

            digitalDisplay.text = currStr;
            audMgr.PlayClip(AudioManager.ClipType.DIGILOCKKEY, null, 0.5f);
        }

        void CheckForPass()
        {
            if (currStr.EndsWith("_"))
                currStr = currStr.Substring(0, currStr.Length - 1);
            
            if (currStr.CompareTo(passKey) == 0)
            {
                currStr = "_";
                UpdateState();
                audMgr.PlayClip(AudioManager.ClipType.DIGILOCKOPEN, null, 0.5f);
            }
            else
            {
                Debug.Log(currStr + " != " + passKey);
            }
        }

        void UpdateDisplay()
        {
            if (currStr.EndsWith("_"))
            {
                currStr = currStr.TrimEnd('_');
            }
            else if (currStr.Length < 4)
                currStr += "_";

            digitalDisplay.text = currStr;
        }

        private void OnDestroy()
        {
            DInitDigiLock();
        }

        public void PowerSourceStateChanged(bool isOn)
        {
            mainsOn = isOn;
            if (isOn)
            {
                InitDigiLock();
            }
            else
            {
                DInitDigiLock();
            }
        }

        public void SetMainsState(bool isOn)
        {
            mainsOn = isOn;
            if (isOn)
                InitDigiLock();
        }
    }
}





/*
 __  _ _____   ____  _ _____  
|  \| | __\ `v' /  \| |_   _| 
| | ' | _| `. .'| | ' | | |   
|_|\__|_|   !_! |_|\__| |_|
 
*/

