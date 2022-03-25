using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LeftHand : Limbs
{
    public override void RunAnimation()
    {
        Animator.SetBool(AnimatorLeftHand.Bool.IsRunning, true);
    }

    public static class AnimatorLeftHand
    {
        public static class Bool
        {
            public const string IsRunning = nameof(IsRunning);
        }
    }
}
