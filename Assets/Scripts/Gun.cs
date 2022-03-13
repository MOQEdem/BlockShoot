using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private List<Cartridge> _magazineOfCartridge;
    [SerializeField] private List<Color> _ñartridgeColors;
    [SerializeField] private Transform _muzzle;
    [SerializeField] private Bullet _bullet;

    private void Start()
    {
        foreach (var cartridge in _magazineOfCartridge)
        {
            SetBulletColor(cartridge);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Shoot();
    }

    private void Shoot()
    {
        Color color = _magazineOfCartridge[0].CartridgeRenderer.material.color;
        Bullet bullet = Instantiate<Bullet>(_bullet, _muzzle.position, _muzzle.rotation);
        bullet.SetColor(color);
    }

    private void SetBulletColor(Cartridge cartridge)
    {
        cartridge.SetColor(_ñartridgeColors[Random.Range(0, _ñartridgeColors.Count)]);
    }
}
