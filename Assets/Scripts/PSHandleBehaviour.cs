using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;
using Leap.Unity.Interaction;
using Leap.Unity.Examples;
using UnityEngine.Events;

namespace Nfynt.Components
{
    [RequireComponent(typeof(InteractionBehaviour))]
    public class PSHandleBehaviour : TransformHandle
    {
        //UP is ON
        //DOWN is OFF
        public enum States
        {
            ON, OFF
        }

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

        private Material handleMat;
        private InteractionBehaviour intObj;
        private TransformTool transTool;

        protected override void Start()
        {
            base.Start();

            intObj = GetComponent<InteractionBehaviour>();
            transTool = GetComponentInParent<TransformTool>();
            handleMat = transform.GetComponentInChildren<MeshRenderer>().material;

            intObj.OnGraspedMovement += OnGraspedMovement;
        }

        private void OnGraspedMovement(Vector3 presolvePos, Quaternion presolveRot, Vector3 solvedPos, Quaternion solvedRot, List<InteractionController> controllers)
        {
            Vector3 presolveToolToHandle = presolvePos - _tool.transform.position;
            Vector3 solvedToolToHandleDirection = (solvedPos - _tool.transform.position).normalized;
            Vector3 constrainedToolToHandle = Vector3.ProjectOnPlane(solvedToolToHandleDirection, (presolveRot * Vector3.up)).normalized * presolveToolToHandle.magnitude;
            Quaternion deltaRotation = Quaternion.FromToRotation(presolveToolToHandle, constrainedToolToHandle);

            // Notify the tool about the calculated rotation.
            _tool.NotifyHandleRotation(deltaRotation);

            // Move the object back to its original position, to be moved correctly later on by the Transform Tool.
            _intObj.rigidbody.position = presolvePos;
            _intObj.rigidbody.rotation = presolveRot;
        }

        void Update()
        {
            if (handleMat != null)
            {
                Color targetColor = defaultColor;
                if (intObj.isPrimaryHovered && usePrimaryHover)
                    targetColor = primaryHoverColor;
                else
                {
                    if (intObj.isHovered && useHover)
                    {
                        float glow = intObj.closestHoveringControllerDistance.Map(0F, 0.2F, 1F, 0.0F);
                        targetColor = Color.Lerp(defaultColor, hoverColor, glow);
                    }
                }

                if (intObj.isSuspended)
                    targetColor = suspendedColor;

                if (intObj is InteractionButton && (intObj as InteractionButton).isPressed)
                    targetColor = pressedColor;

                // Lerp actual material color to the target color.
                handleMat.color = Color.Lerp(handleMat.color, targetColor, 30F * Time.deltaTime);
            }
        }

        public void CheckHandelState()
        {

        }
    }
}




/*
 __  _ _____   ____  _ _____  
|  \| | __\ `v' /  \| |_   _| 
| | ' | _| `. .'| | ' | | |   
|_|\__|_|   !_! |_|\__| |_|
 
*/

