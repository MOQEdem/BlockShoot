using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LeftLeg : Limbs
{
    public override void RunAnimation()
    {
        Animator.SetBool(AnimatorLeftLeg.Bool.IsRunning, true);
    }

    public static class AnimatorLeftLeg
    {
        public static class Bool
        {
            public const string IsRunning = nameof(IsRunning);
        }
    }
}
