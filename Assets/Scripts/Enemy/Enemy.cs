using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private List<Block> _blocks;

    private void OnEnable()
    {
        foreach (var block in _blocks)
            block.Hited += DestroyBlocks;
    }

    private void OnDisable()
    {
        foreach (var block in _blocks)
        {
            block.Hited -= DestroyBlocks;
        }
    }

    private void DestroyBlocks()
    {

    }
}
