using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHand : Limbs
{
    public override void RunAnimation()
    {
        Animator.SetBool(AnimatorRightHand.Bool.IsRunning, true);
    }

    public static class AnimatorRightHand
    {
        public static class Bool
        {
            public const string IsRunning = nameof(IsRunning);
        }
    }
}
