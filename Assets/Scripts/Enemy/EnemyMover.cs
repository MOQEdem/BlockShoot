using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _speed;
    [SerializeField] private List<Limbs> _limbs;

    private IEnumerator MoveToPlayer()
    {
        transform.LookAt(_player.transform);

        foreach (var limb in _limbs)
        {
            limb.RunAnimation();
        }

        while (_player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);

            yield return null;
        }
    }

    public void ActivateMovement()
    {
        StartCoroutine(MoveToPlayer());
    }
}
