using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;

public class test : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Test: " +collision.gameObject.name);
    }
}
