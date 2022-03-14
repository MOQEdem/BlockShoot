using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : Cartridge
{
    [SerializeField] private float _speed;
    [SerializeField] private ParticleSystem _flyEffect;
    [SerializeField] private ParticleSystem _hitEffect;
    [SerializeField] private float _lifetime;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.velocity = transform.TransformDirection(Vector3.forward * _speed);

        TransferColorToParticles(_flyEffect);
        TransferColorToParticles(_hitEffect);

        StartCoroutine(DestroyItself());
    }

    private void TransferColorToParticles(ParticleSystem particleSystem)
    {
        Color color = CartridgeRenderer.material.color;
        color.a = 1;
        var main = particleSystem.main;
        main.startColor = color;
    }

    private IEnumerator DestroyItself()
    {
        yield return new WaitForSeconds(_lifetime);

        Destroy(gameObject);
    }
}
