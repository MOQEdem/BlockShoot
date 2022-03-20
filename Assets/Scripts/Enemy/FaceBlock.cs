using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceBlock : Block
{
    [SerializeField] private SpriteRenderer _face;

    public override void DestroyItself()
    {
        _face.enabled = false;
        base.DestroyItself();

    }
}
