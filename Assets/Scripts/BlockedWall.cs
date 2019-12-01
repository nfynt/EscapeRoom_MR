using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nfynt.Components;

namespace Nfynt.Utility {
    public class BlockedWall : MonoBehaviour
    {
        public Vector2 moveSpeed;
        public DoorController door;
        Vector2 Offset = new Vector2(0f, 0f);
        Color col;
        Renderer rend;
        bool fading;

        private void OnEnable()
        {
            rend = GetComponent<MeshRenderer>();
        }

        private void LateUpdate()
        {
            if (fading)
            {
                col = new Color(col.r, col.g, col.b, col.a - 0.1f);
            }
            else
            {
                Offset += moveSpeed * Time.deltaTime;
                rend.material.SetTextureOffset("_MainTex", Offset);
            }
        }

        public void FadeWall()
        {
            fading = true;
            StartCoroutine(Fade());
        }

        IEnumerator Fade()
        {
            col = rend.material.color;
            yield return new WaitUntil(() => col.a < 0.1f);
            door.WallFaded();
        }
    }
}




/*
 __  _ _____   ____  _ _____  
|  \| | __\ `v' /  \| |_   _| 
| | ' | _| `. .'| | ' | | |   
|_|\__|_|   !_! |_|\__| |_|
 
*/

