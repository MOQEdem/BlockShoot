using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMover : MonoBehaviour
{
    private float oXRotation;
    private float oYRotation;

    private void Start()
    {
        oXRotation = 0;
        oYRotation = 0;
    }

    private void Update()
    {
        oXRotation += Input.GetAxis("Mouse X");
        oYRotation += Input.GetAxis("Mouse Y");

        transform.rotation = Quaternion.Euler(-oYRotation, oXRotation, 0f);
    }
}
