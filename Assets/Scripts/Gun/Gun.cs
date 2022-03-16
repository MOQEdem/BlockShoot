using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Gun : MonoBehaviour
{
    [SerializeField] private List<Cartridge> _magazineOfCartridge;
    [SerializeField] private List<Color> _cartridgeColors;
    [SerializeField] private Transform _muzzle;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private ParticleSystem _shootEffect;
    [SerializeField] private float _recoilSpeed;

    private bool _isReadyToShoot = true;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        foreach (var cartridge in _magazineOfCartridge)
        {
            SetCartridgeColor(cartridge);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_isReadyToShoot)
            {
                Shoot();
                Reload();
                PlayAnimation();
                StartCoroutine(Recoil());
            }
        }
    }

    private void Shoot()
    {
        Color color = _magazineOfCartridge[0].CartridgeRenderer.material.color;
        SetEffectColor(color);
        _shootEffect.Play();
        Bullet bullet = Instantiate<Bullet>(_bullet, _muzzle.position, _muzzle.rotation);
        bullet.SetColor(color);
    }

    private void SetEffectColor(Color color)
    {
        color.a = 1;
        var main = _shootEffect.main;
        main.startColor = color;
    }

    private void SetCartridgeColor(Cartridge cartridge)
    {
        cartridge.SetColor(_cartridgeColors[Random.Range(0, _cartridgeColors.Count)]);
    }

    private void Reload()
    {
        for (int i = 0; i < _magazineOfCartridge.Count - 1; i++)
        {
            Color color = _magazineOfCartridge[i + 1].CartridgeRenderer.material.color;
            _magazineOfCartridge[i].SetColor(color);
        }

        SetCartridgeColor(_magazineOfCartridge[_magazineOfCartridge.Count - 1]);
    }

    private void PlayAnimation()
    {
        _animator.SetTrigger(AnimatorGun.Trigger.Shoot);
    }

    private IEnumerator Recoil()
    {
        var _recoilTime = new WaitForSeconds(_recoilSpeed);

        _isReadyToShoot = false;

        yield return _recoilTime;

        _isReadyToShoot = true;
    }

    public static class AnimatorGun
    {
        public static class Trigger
        {
            public const string Shoot = nameof(Shoot);
        }
    }
}
