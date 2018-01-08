using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class LostObjectsResetter : MonoBehaviour {
    public Transform resetPosition;
    
    void OnTriggerEnter(Collider other)
    {
        Component[] rigidbodies = other.gameObject.GetComponentsInParent<Rigidbody>();
        foreach(Rigidbody rb in rigidbodies)
        {
            rb.transform.position = resetPosition.position;
            rb.velocity = Vector3.zero;
        }
    }
}
