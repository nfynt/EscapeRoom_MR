using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;

namespace Nfynt.Components {
    [RequireComponent(typeof(InteractionBehaviour))]
    public class JigsawPieceBehaviour : MonoBehaviour
    {
        public float zOffset=0.1f;
        private InteractionBehaviour intObj;
        private Rigidbody rBody;
        private JigsawBoardController jbController;
        public int boardPos = 1;
        public Collider connectedCollider;
        private Collider targetColider;

        private void Start()
        {
            intObj = GetComponent<InteractionBehaviour>();
            rBody = GetComponent<Rigidbody>();
            //zOffset = GetComponent<Collider>().bounds.extents.z;
            jbController = JigsawBoardController.Instance;
        }

        public void OnGrassped()
        {
            rBody.isKinematic = false;
            if (connectedCollider != null)
                jbController.ReleaseSlot(connectedCollider);
            connectedCollider = null;
        }

        public void GraspReleased()
        {
            rBody.isKinematic = false;
        }

        public void SetTargetCollider(Collider co)
        {
            this.targetColider = co;
        }

        public bool Solved()
        {
            if (connectedCollider!=null && connectedCollider == targetColider)
                return true;

            return false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "JigsawBoard" && jbController.IsSlotAvailable(other))
            {
                intObj.ReleaseFromGrasp();
                SnapOnBoard(other.transform);
                connectedCollider = other;
                jbController.SlotUsed(other, boardPos);
            }
        }

        public void SnapOnBoard(Transform cObj)
        {
            rBody.isKinematic = true;
            transform.position = cObj.position - cObj.forward * zOffset;
            transform.rotation = cObj.rotation;
        }
    }

}




/*
 __  _ _____   ____  _ _____  
|  \| | __\ `v' /  \| |_   _| 
| | ' | _| `. .'| | ' | | |   
|_|\__|_|   !_! |_|\__| |_|
 
*/
