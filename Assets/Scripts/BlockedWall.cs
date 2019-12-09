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
		//Color col;
		Renderer rend;
		bool fading;

		private void OnEnable()
		{
			rend = GetComponent<MeshRenderer>();
		}

		private void LateUpdate()
		{
			if (!fading)
			{
				Offset += moveSpeed * Time.deltaTime;
				rend.material.SetTextureOffset("_MainTex", Offset);
			}
		}

		/// <summary>
		/// Fade wall in secs
		/// </summary>
		/// <param name="dur"></param>
		public void FadeWall(float dur=3f)
		{
			fading = true;
			StartCoroutine(Fade(dur));
		}

		IEnumerator Fade(float dur=3f)
		{
			yield return new WaitForSeconds(dur);
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

