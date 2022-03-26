using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BlockAnimation : MonoBehaviour
{
    private Animator _animator;

    public Animator Animator => _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayHitAnimation()
    {
        _animator.SetTrigger(AnimatorBlock.Trigger.Hit);
    }

    public static class AnimatorBlock
    {
        public static class Trigger
        {
            public const string Hit = nameof(Hit);
        }
    }
}
