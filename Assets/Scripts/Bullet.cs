using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : Cartridge
{
    [SerializeField] private float _speed;
    [SerializeField] private ParticleSystem _flyEffect;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.velocity = transform.TransformDirection(Vector3.forward * _speed);

        Color color = CartridgeRenderer.material.color;
        color.a = 1;
        var main = _flyEffect.main;
        main.startColor = color;
    }
}
