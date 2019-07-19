// Expose center of mass to allow it to be set from
// the inspector.
using UnityEngine;
using System.Collections;

public class ExposeCenterOfMass : MonoBehaviour
{
    public Vector3 com;
    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = com;
    }
}
