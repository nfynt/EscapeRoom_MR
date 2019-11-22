using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;

namespace Nfynt.Components {
    [RequireComponent(typeof(InteractionBehaviour))]
    public class JigsawPieceBehaviour : MonoBehaviour
    {
        private InteractionBehaviour intObj;
        private Rigidbody rBody;

        private void Start()
        {
            intObj = GetComponent<InteractionBehaviour>();
            rBody = GetComponent<Rigidbody>();
        }

        public void OnGrassped()
        {
            rBody.isKinematic = false;
        }

        public void GraspReleased()
        {
            rBody.isKinematic = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "JigsawBoard")
            {
                intObj.ReleaseFromGrasp();

            }
        }

        public void SnapOnBoard(Transform cObj)
        {
            rBody.isKinematic = true;
            transform.position = cObj.position;
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
