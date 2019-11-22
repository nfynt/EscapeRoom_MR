using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Examples;

namespace Nfynt.Components
{
    public class PowerSupplyHandle : MonoBehaviour
    {
        public PowerSupplyBehaviour psb;
        public Color activationColor = Color.yellow;
        [SerializeField]
        private Renderer rend;
        private Material materialInstance;

        private Color originalColor;

        void Start()
        {
            if (rend == null)
                rend = GetComponent<Renderer>();
            if (psb == null)
                psb = GetComponentInParent<PowerSupplyBehaviour>();
            materialInstance = rend.material;
            originalColor = materialInstance.color;
        }

        public void SetToActivationColor()
        {
            materialInstance.color = activationColor;
            psb.ToggleCheckAngle(true);
        }

        public void SetToOriginalColor()
        {
            materialInstance.color = originalColor;
            psb.ToggleCheckAngle(false);
        }

        public void ShowRenderer()
        {
            //rend.enabled = true;
            psb.EnableTransTool();
        }

        public void HideRenderer()
        {
            //rend.enabled = false;
        }

    }
}




/*
 __  _ _____   ____  _ _____  
|  \| | __\ `v' /  \| |_   _| 
| | ' | _| `. .'| | ' | | |   
|_|\__|_|   !_! |_|\__| |_|
 
*/
