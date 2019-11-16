using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nfynt;

namespace Nfynt
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : SingletonPersistent<AudioManager>
    {
        public enum ClipType
        {
            UI_BUTTON_CLICK,
            TORCH_BUTTON_CLICK,
            HEAVY_BUTTON_CLICK
        }

        [Header("Ambient Clips")]
        public AudioClip homeClip;
        public AudioClip mainClip;

        [Space(15)]

        [Header("SFX Clips")]
        public AudioClip clickClip;
        public AudioClip torchButtonClip;

        private AudioSource ambientAudSrc;
        private AudioSource audSrc;

        private void Start()
        {
            audSrc = GetComponent<AudioSource>();
            audSrc.Stop();
            audSrc.playOnAwake = false;
            audSrc.loop = false;
        }

        public void SetAmbientAudioSrc(AudioSource src)
        {
            ambientAudSrc = src;
            if(GameSettings.isMusicOn)
            {
                if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Home")
                    ambientAudSrc.clip = homeClip;
                else
                    ambientAudSrc.clip = mainClip;
                ambientAudSrc.loop = true;
                ambientAudSrc.Play();
            }
            else
            {
                ambientAudSrc.Stop();
            }
        }

        public void PlayClip(ClipType cType, AudioSource audioSrc=null, float resetTime = -1f)
        {
            if (!GameSettings.isSFXOn)
                return;

            if (audioSrc == null)
                audioSrc = audSrc;

            switch (cType)
            {
                case ClipType.UI_BUTTON_CLICK:
                    audioSrc.clip = clickClip;
                    break;
                case ClipType.TORCH_BUTTON_CLICK:
                    break;
            }
            audSrc.Play();

            if (resetTime > 0f)
                StartCoroutine(ResetAudioSrc(resetTime));
        }

        IEnumerator ResetAudioSrc(float time, AudioSource audioSrc = null)
        {
            yield return new WaitForSeconds(time);
            if (audioSrc == null)
                audSrc.Stop();
            else
                audioSrc.Stop();
        }
    }
}




/*
 __  _ _____   ____  _ _____  
|  \| | __\ `v' /  \| |_   _| 
| | ' | _| `. .'| | ' | | |   
|_|\__|_|   !_! |_|\__| |_|
 
*/
