using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;

namespace Nfynt.Tracking
{
    [RequireComponent(typeof(Rigidbody))]
    public class MirroCodeBehaviour : MonoBehaviour
    {

        private Rigidbody rigidbody;
        private Vector3 posSt;
        private Quaternion rotSt;
        private InteractionBehaviour ib;

        private void Start()
        {
            posSt = transform.position;
            rotSt = transform.rotation;
            ib = GetComponent<InteractionBehaviour>();
            rigidbody = GetComponent<Rigidbody>();
            rigidbody.isKinematic = true;
            ResetFrame();
        }

        void ResetFrame()
        {
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;
            transform.position = posSt;
            transform.rotation = rotSt;
        }

        public void ObjectGrabbed()
        {
            rigidbody.isKinematic = false;
            rigidbody.useGravity = true;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.tag=="Nail")
            {
                ib.ReleaseFromGrasp();
                ResetFrame();
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

