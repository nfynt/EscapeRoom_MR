using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nfynt.Utility {
    public class SkyboxMove : MonoBehaviour
    {
        public Vector2 moveSpeed;
        Vector2 Offset = new Vector2(0f, 0f);
        Color col;
        Renderer rend;

        private void OnEnable()
        {
            rend = GetComponent<MeshRenderer>();
        }

        private void LateUpdate()
        {
            Offset += moveSpeed * Time.deltaTime;
                rend.material.SetTextureOffset("_MainTex", Offset);
        }
    }
}




/*
 __  _ _____   ____  _ _____  
|  \| | __\ `v' /  \| |_   _| 
| | ' | _| `. .'| | ' | | |   
|_|\__|_|   !_! |_|\__| |_|
 
*/

