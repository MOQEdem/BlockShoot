using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _delayBeforMove;

    public UnityAction MovementComplete;

    private IEnumerator Move(List<Transform> waypoints)
    {
        yield return new WaitForSeconds(_delayBeforMove);

        int currentPoint = 0;

        while (currentPoint < waypoints.Count)
        {
            Transform target = waypoints[currentPoint];
            transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

            if (transform.position == target.position)
            {
                currentPoint++;
            }

            yield return null;
        }

        MovementComplete?.Invoke();
    }

    private IEnumerator LookForEnemy(Enemy enemy)
    {
        Vector3 direction = enemy.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);

        while (transform.rotation != rotation)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);

            yield return null;
        }
    }

    public void StartMoving(List<Transform> waypoints)
    {
        StartCoroutine(Move(waypoints));
    }

    public void ChangeTrackingEnemy(Enemy enemy)
    {
        StartCoroutine(LookForEnemy(enemy));
    }
}
