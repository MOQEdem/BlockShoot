using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Legs : Limbs
{
    public override void RunAnimation()
    {
        Animator.SetBool(AnimatorLegs.Bool.IsRunning, true);
    }

    public static class AnimatorLegs
    {
        public static class Bool
        {
            public const string IsRunning = nameof(IsRunning);
        }
    }
}
