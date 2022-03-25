using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RightLeg : Limbs
{

    public override void RunAnimation()
    {
        Animator.SetBool(AnimatorRightLeg.Bool.IsRunning, true);
    }

    public static class AnimatorRightLeg
    {
        public static class Bool
        {
            public const string IsRunning = nameof(IsRunning);
        }
    }
}
