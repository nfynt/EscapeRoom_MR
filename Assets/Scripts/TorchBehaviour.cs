using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;
using Leap.Unity.Interaction;

namespace Nfynt.Components
{
    [RequireComponent(typeof(AudioSource))]
    public class TorchBehaviour : MonoBehaviour
    {
        public enum States
        {
            OFF, LIGHT, UV, SABER
        }
        public Transform buttonTrans;
        public GameObject normLight;
        public GameObject uvLight;
        public GameObject saberLight;
        [Header("Texture references")]
        public Texture2D offIcon;
        public Texture2D torchIcon;
        public Texture2D uvIcon;
        public Texture2D saberIcon;

        [Tooltip("object will lerp to its hoverColor when a hand is nearby.")]
        public bool useHover = true;

        [Tooltip("use its primaryHoverColor when the primary hover of an InteractionHand.")]
        public bool usePrimaryHover = false;

        [Header("InteractionBehaviour Colors")]
        public Color defaultColor = Color.Lerp(Color.black, Color.white, 0.1F);
        public Color suspendedColor = Color.red;
        public Color hoverColor = Color.Lerp(Color.black, Color.white, 0.7F);
        public Color primaryHoverColor = Color.Lerp(Color.black, Color.white, 0.8F);

        [Header("InteractionButton Colors")]
        [Tooltip("This color only applies if the object is an InteractionButton or InteractionSlider.")]
        public Color pressedColor = Color.white;


        private States currState;
        private States lastState;
        private bool readyToswitch;
        private Vector3 normalButtonPos;
        [SerializeField]
        private float pressHeight = 0.2f;
        private Material buttonMat;
        private Material buttonIconMat;
        private InteractionBehaviour btnIntObj;
        private AudioSource audSrc;

        private void Awake()
        {
            btnIntObj = buttonTrans.GetComponent<InteractionBehaviour>();
            buttonMat = buttonTrans.GetComponent<MeshRenderer>().material;
            buttonIconMat = buttonTrans.GetChild(0).GetComponent<MeshRenderer>().material;
            audSrc = GetComponent<AudioSource>();
        }

        private void Start()
        {
            lastState = States.SABER;
            currState = States.OFF;
            readyToswitch = false;
            UpdateTorchState(currState);
            normalButtonPos = buttonTrans.localPosition;
            audSrc.loop = false;
            audSrc.playOnAwake = false;
            audSrc.Stop();
        }

        void Update()
        {
            if (buttonMat != null)
            {
                Color targetColor = defaultColor;
                if (btnIntObj.isPrimaryHovered && usePrimaryHover)
                    targetColor = primaryHoverColor;
                else
                {
                    if (btnIntObj.isHovered && useHover)
                    {
                        float glow = btnIntObj.closestHoveringControllerDistance.Map(0F, 0.2F, 1F, 0.0F);
                        targetColor = Color.Lerp(defaultColor, hoverColor, glow);
                    }
                }

                if (btnIntObj.isSuspended)
                    targetColor = suspendedColor;

                if (btnIntObj is InteractionButton && (btnIntObj as InteractionButton).isPressed)
                    targetColor = pressedColor;

                // Lerp actual material color to the target color.
                buttonMat.color = Color.Lerp(buttonMat.color, targetColor, 30F * Time.deltaTime);
            }
        }

        void UpdateTorchState(States newState)
        {
            switch(newState)
            {
                case States.OFF:
                    normLight.SetActive(false);
                    uvLight.SetActive(false);
                    saberLight.SetActive(false);
                    buttonIconMat.mainTexture = offIcon;
                    break;
                case States.LIGHT:
                    normLight.SetActive(true);
                    uvLight.SetActive(false);
                    saberLight.SetActive(false);
                    buttonIconMat.mainTexture = torchIcon;
                    break;
                case States.UV:
                    normLight.SetActive(false);
                    uvLight.SetActive(true);
                    saberLight.SetActive(false);
                    buttonIconMat.mainTexture = uvIcon;
                    break;
                case States.SABER:
                    normLight.SetActive(false);
                    uvLight.SetActive(false);
                    saberLight.SetActive(true);
                    buttonIconMat.mainTexture = saberIcon;
                    break;
            }
            currState = newState;
        }

        public void ButtonContactBegin()
        {
            readyToswitch = true;
            buttonTrans.localPosition -= new Vector3(0f, pressHeight, 0f);
        }

        public void ButtonContactEnd()
        {
            if (!readyToswitch || btnIntObj.GetHoverDistance(buttonTrans.position)<pressHeight) return;

            readyToswitch = false;
            buttonTrans.localPosition = normalButtonPos;
            TorchButtonPressed();
        }

        public void ButtonHoverEnd()
        {
            if (!readyToswitch) return;

            readyToswitch = false;
            buttonTrans.localPosition = normalButtonPos;
            TorchButtonPressed();
        }

        public void TorchButtonPressed()
        {
            AudioManager.Instance.PlayClip(AudioManager.ClipType.TORCH_BUTTON_CLICK, audSrc);

            if (currState != States.OFF)
            {
                lastState = currState;
                UpdateTorchState(States.OFF);
            }
            else
            {
                if (lastState == States.LIGHT) UpdateTorchState(States.UV);
                else if (lastState == States.UV) UpdateTorchState(States.SABER);
                else if (lastState == States.SABER) UpdateTorchState(States.LIGHT);
                lastState = currState;
            }
        }
    }
}




/*
 __  _ _____   ____  _ _____  
|  \| | __\ `v' /  \| |_   _| 
| | ' | _| `. .'| | ' | | |   
|_|\__|_|   !_! |_|\__| |_|
 
*/
