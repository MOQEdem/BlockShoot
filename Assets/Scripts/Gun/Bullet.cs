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
    [SerializeField] private float _deathDelay;
    [SerializeField] private Collider _triggerCollider;

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
        Color color = MaterialColor;
        color.a = 1;
        var main = particleSystem.main;
        main.startColor = color;
    }

    private IEnumerator DestroyItself()
    {
        yield return new WaitForSeconds(_lifetime);

        _hitEffect.Play();
        Destroy();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Block>(out Block enemyBlock))
        {
            Destroy();
        }
    }

    public void Destroy()
    {
        _triggerCollider.enabled = false;
        _flyEffect.Stop();
        _hitEffect.Play();
        SetInvisibility();
        Destroy(gameObject, _deathDelay);
    }
}
