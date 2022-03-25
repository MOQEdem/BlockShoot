using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Limbs : MonoBehaviour
{
    protected Animator Animator;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    public virtual void RunAnimation()
    {

    }
}
