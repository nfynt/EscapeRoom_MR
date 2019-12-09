using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nfynt.Utility;
using Nfynt.Managers;

namespace Nfynt.Components
{
	[RequireComponent(typeof(AudioSource))]
	public class DoorController : MonoBehaviour
	{
		public float rotateOnY = 100f;
		public Transform doorRef;
		public GameObject blockedWall;
		public AudioSource audSrc;

		private Quaternion rotation;
		MainSceneManager msMgr;

		private void Start()
		{
			rotation = doorRef.rotation;
			blockedWall.SetActive(false);
			audSrc = GetComponent<AudioSource>();
			if (MainSceneManager.Instance != null)
				msMgr = MainSceneManager.Instance;
			else
				msMgr = FindObjectOfType<MainSceneManager>();
		}

		private void Update()
		{
#if UNITY_EDITOR
			if (Input.GetKeyDown(KeyCode.P))
				OpenDoor();
			if (Input.GetKeyDown(KeyCode.O))
				BreakTheWall();
#endif
		}

		public void OpenDoor()
		{
			doorRef.Rotate(doorRef.right, rotateOnY);
			blockedWall.SetActive(true);
			AudioManager.Instance.PlayClip(AudioManager.ClipType.DOOROPEN, audSrc);
		}

		public void BreakTheWall()
		{
			//blockedWall.SetActive(false);
			blockedWall.GetComponent<BlockedWall>().FadeWall();
			AudioManager.Instance.PlayClip(AudioManager.ClipType.FADEWALL,audSrc);
		}

		public void WallFaded()
		{
			msMgr.BlockedWallDestroyed();
		}
	}
}




/*
 __  _ _____   ____  _ _____  
|  \| | __\ `v' /  \| |_   _| 
| | ' | _| `. .'| | ' | | |   
|_|\__|_|   !_! |_|\__| |_|
 
*/

