using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test2 : MonoBehaviour
{
    public bool track;
    public Transform target;

    private void Start()
    {
    }

    private void Update()
    {
        if(track && target!=null)
        {
            Vector3 dir = target.position - transform.position;
            
            transform.rotation = Quaternion.LookRotation(dir,transform.forward)*Quaternion.AngleAxis(90,transform.right);
        }
    }

    private void OnDrawGizmos()
    {
        if (target==null) return;
        Vector3 dir = target.position - transform.position;

        Vector3 tar = new Vector3(dir.x, dir.y, dir.z);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, dir);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.up);
    }
}
