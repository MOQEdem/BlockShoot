using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RayFire;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Explosion))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(BlockAnimation))]
[RequireComponent(typeof(RayfireRigid))]
public class Block : MonoBehaviour
{
    [SerializeField] private float _destroyDelay;
    [SerializeField] private ParticleSystem _deathEffect;
    [SerializeField] private Material _blockParts;
    [SerializeField] private Collider _collider;
    [SerializeField] private RayfireBomb _bomb;

    private RayfireRigid _rayfireRigid;
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
        _rayfireRigid = GetComponent<RayfireRigid>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Bullet>(out Bullet bullet))
        {
            SetColor(bullet.MaterialColor);
            _deathEffect.startColor = bullet.MaterialColor;
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
        _rayfireRigid.Demolish();
        _collider.enabled = false;
        _bomb.Explode(0f);
        _blockParts.color = MaterialColor;
        _deathEffect.transform.parent = null;
        _deathEffect.Play();
        Destroy(gameObject);
    }

    public void FreeItself()
    {
        transform.parent = null;
        _rigidbody.isKinematic = false;
        Destroy(gameObject, _destroyDelay);
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
