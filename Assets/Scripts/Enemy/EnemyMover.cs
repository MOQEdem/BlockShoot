using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _speed;

    private IEnumerator MoveToPlayer()
    {
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
