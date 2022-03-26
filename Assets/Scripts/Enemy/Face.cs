using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : MonoBehaviour
{
    [SerializeField] private Sprite _faceNormal;
    [SerializeField] private Sprite _faceHited;
    [SerializeField] private Sprite _faceDead;
    [SerializeField] private SpriteRenderer _face;
    [SerializeField] private float _normalizationTime;

    public void PlayFaceChange()
    {
        StartCoroutine(ShowHitedFace());
    }

    public void SetDeadFace()
    {
        _face.sprite = _faceDead;
    }

    public bool IsFaceMissing()
    {
        return _face == null;
    }

    private IEnumerator ShowHitedFace()
    {
        var _recoilTime = new WaitForSeconds(_normalizationTime);

        _face.sprite = _faceHited;

        yield return _recoilTime;

        if (!IsFaceMissing())
            _face.sprite = _faceNormal;
    }
}
