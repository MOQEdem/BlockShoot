using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshRenderer))]
public class EnemyBlock : MonoBehaviour
{
    [SerializeField] private EnemyBlock _previousBlock;
    [SerializeField] private EnemyBlock _nextBlock;

    private MeshRenderer _blockRenderer;

    public MeshRenderer BlockRenderer => _blockRenderer;

    public event UnityAction Hited;

    private void Awake()
    {
        _blockRenderer = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        Hited += DestroyCombo;
    }

    private void OnDisable()
    {
        Hited -= DestroyCombo;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Bullet>(out Bullet bullet))
        {
            _blockRenderer.material.color = bullet.CartridgeRenderer.material.color;
            Hited?.Invoke();
        }
    }

    private void DestroyCombo()
    {
        if (CheckNeighboringBlocks())
        {
            Destroy(_previousBlock.gameObject);
            Destroy(_nextBlock.gameObject);
            Destroy(gameObject);
        }
    }

    private bool CheckNeighboringBlocks()
    {
        if (_blockRenderer.material.color == _previousBlock.BlockRenderer.material.color
            && _blockRenderer.material.color == _nextBlock.BlockRenderer.material.color)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
