using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Explosion : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _power;
    [SerializeField] private float _tossing;
    [SerializeField] private float _rotationSpeed;

    private Rigidbody _rigidbody;

    public void Explode()
    {
        Vector3 newRotation = new Vector3(Random.Range(-100f, 100f), Random.Range(-100f, 100f), Random.Range(-100f, 100f));

        transform.RotateAround(transform.position, newRotation, _rotationSpeed);

        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);

        foreach (var hit in colliders)
        {
            Rigidbody rigitbody = hit.GetComponent<Rigidbody>();

            if (rigitbody != null)
                rigitbody.AddExplosionForce(_power, this.transform.position, _radius, _tossing, ForceMode.VelocityChange);
        }
    }
}
