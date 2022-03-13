using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Cartridge : MonoBehaviour
{
    private MeshRenderer _cartridgeRenderer;

    public MeshRenderer CartridgeRenderer => _cartridgeRenderer;

    private void Awake()
    {
        _cartridgeRenderer = GetComponent<MeshRenderer>();
    }

    public void SetColor(Color color)
    {
        _cartridgeRenderer.material.color = color;
    }
}
