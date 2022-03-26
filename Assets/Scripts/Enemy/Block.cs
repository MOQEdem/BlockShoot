using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Explosion))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(BlockAnimation))]
public class Block : MonoBehaviour
{
    [SerializeField] private float _freeBlockDestroyDelay;
    [SerializeField] private ParticleSystem _deathEffect;
    [SerializeField] private Material _blockParts;
    [SerializeField] private Collider _collider;

    private Rigidbody _rigidbody;
    private MeshRenderer _blockRenderer;
    private BlockAnimation _blockAnimation;
    private Explosion _explosion;
    private int _position;

    public Hit Hited;

    public Color MaterialColor => _blockRenderer.material.color;
    public Explosion Explosion => _explosion;
    public BlockAnimation Animation => _blockAnimation;

    private void Awake()
    {
        if (Hited == null)
            Hited = new Hit();

        _blockRenderer = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
        _explosion = GetComponent<Explosion>();
        _collider = GetComponent<Collider>();
        _blockAnimation = GetComponent<BlockAnimation>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Bullet>(out Bullet bullet))
        {
            SetColor(bullet.MaterialColor);
            SetVisibility(1f);
            Hited?.Invoke(_position);
        }
    }

    public void DefinePosition(int position)
    {
        _position = position;
    }

    public virtual void DestroyItself()
    {
        _collider.enabled = false;
        _blockParts.color = MaterialColor;
        _deathEffect.Play();
        _deathEffect.transform.parent = null;
        Destroy(gameObject);
    }

    public void FreeItself()
    {
        transform.parent = null;
        _rigidbody.isKinematic = false;
        Destroy(gameObject, _freeBlockDestroyDelay);
    }

    public void SetColor(Color color)
    {
        _blockRenderer.material.color = color;
    }

    private void SetVisibility(float alphaChannel)
    {
        Color color = MaterialColor;
        color.a = alphaChannel;
        _blockRenderer.material.color = color;
    }

    public class Hit : UnityEvent<int> { }
}
