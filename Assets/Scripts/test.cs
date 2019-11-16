using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;

public class test : MonoBehaviour
{
    Vector3 currPos;
    Vector3 currRot;

    bool isHovering;
    bool grassped;
    InteractionBehaviour ib;

    private void Start()
    {
        currPos = transform.position;
        currRot = transform.rotation.eulerAngles;
        ib = GetComponent<InteractionBehaviour>();
    }

    public void HoverBegin()
    {
        isHovering = true;
    }

    public void HoverEnd()
    {
        isHovering = false;
    }

    public void GraspBegin()
    {
        grassped = true;
    }

    public void GraspEnd()
    {
        grassped = false;
    }

    private void Update()
    {
        if(isHovering)
        {
           // Leap.Vector handle = new Leap.Vector(transform.position.x,transform.position.y,transform.position.z);
            Leap.Vector pos = ib.closestHoveringHand.PalmPosition;
            Vector3 tar = new Vector3(pos.x, pos.y, pos.z);
            Vector3 dir = tar - transform.position;
            //Quaternion look = Quaternion.LookRotation(tar - transform.position, transform.up);
            //transform.rotation = look;
            //transform.rotation = Quaternion.EulerAngles(new Vector3(transform.rotation.eulerAngles.x, 0f, 0f));
            float rad = dir.magnitude;
            Debug.Log(rad);
            Debug.Log(tar);
            float phi = Mathf.Acos(dir.z / rad);
            float theta = Mathf.Acos(dir.x / (rad * Mathf.Sin(phi)));
            Debug.Log(phi.ToString() + "____" + theta.ToString());
            transform.rotation = Quaternion.EulerAngles(new Vector3(360f - (phi*Mathf.Rad2Deg+theta*Mathf.Rad2Deg), 0f, 0f));
        }
    }

    private void OnDrawGizmos()
    {
        if (ib==null || ib.closestHoveringHand == null) return;
        Leap.Vector dir = ib.closestHoveringHand.PalmPosition;
        
        Vector3 tar = new Vector3(dir.x, dir.y, dir.z);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, tar);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.up * 2);
    }
}
