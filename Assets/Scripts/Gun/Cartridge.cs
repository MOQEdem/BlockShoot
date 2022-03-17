using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Cartridge : MonoBehaviour
{
    private MeshRenderer _renderer;

    public Color MaterialColor => _renderer.material.color;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }
    protected void SetInvisibility()
    {
        Color color = MaterialColor;
        color.a = 0;
        _renderer.material.color = color;
    }

    public void SetColor(Color color)
    {
        color.a = 1;
        _renderer.material.color = color;
    }
}
