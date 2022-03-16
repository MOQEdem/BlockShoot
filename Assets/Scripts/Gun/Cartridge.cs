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
    protected void SetInvisibility()
    {
        Color color = CartridgeRenderer.material.color;
        color.a = 0;
        _cartridgeRenderer.material.color = color;
    }

    public void SetColor(Color color)
    {
        color.a = 1;
        _cartridgeRenderer.material.color = color;
    }
}
