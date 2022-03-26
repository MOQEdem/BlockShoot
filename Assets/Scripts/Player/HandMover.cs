using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMover : MonoBehaviour
{
    private float _oXRotation;
    private float _oYRotation;

    private void Start()
    {
        _oXRotation = 0;
        _oYRotation = 0;
    }

    private void Update()
    {
        _oXRotation += Input.GetAxis("Mouse X");
        _oYRotation += Input.GetAxis("Mouse Y");

        transform.rotation = Quaternion.Euler(-_oYRotation, _oXRotation, 0f);
    }
}
