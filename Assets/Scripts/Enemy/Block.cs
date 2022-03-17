using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshRenderer))]
public class Block : MonoBehaviour
{
    [SerializeField] private Block _previousBlock;
    [SerializeField] private Block _nextBlock;

    public Block PreviousBlock => _previousBlock;
    public Block NextBlock => _nextBlock;

    private MeshRenderer _blockRenderer;

    public Color MaterialColor => _blockRenderer.material.color;

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
            _blockRenderer.material.color = bullet.MaterialColor;
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
        int comboCount = 1;

        if (MaterialColor == _previousBlock.MaterialColor)
        {
            comboCount++;

            if (MaterialColor == _previousBlock.PreviousBlock.MaterialColor)
            {
                comboCount++;
            }
        }

        if (MaterialColor == _nextBlock.MaterialColor)
        {
            comboCount++;

            if (MaterialColor == _nextBlock.NextBlock.MaterialColor)
            {
                comboCount++;
            }
        }

        return comboCount >= 3;
    }
}
