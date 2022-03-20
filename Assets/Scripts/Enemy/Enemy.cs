using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Face))]
[RequireComponent(typeof(AnimationController))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private List<Block> _blocks;
    [SerializeField] private int _minBlockCombo;
    [SerializeField] private float _destroyDelay;

    private Face _face;
    private Color _baseBlockColor;
    private AnimationController _animationController;

    public UnityAction EnemyDead;

    private void Awake()
    {
        _face = GetComponent<Face>();
        _animationController = GetComponent<AnimationController>();
        _baseBlockColor = _blocks[0].MaterialColor;
    }

    private void OnEnable()
    {
        for (int i = 0; i < _blocks.Count; i++)
        {
            _blocks[i].Hited.AddListener(OnBlockHit);
            _blocks[i].DefinePosition(i);
        }
    }

    private void OnDisable()
    {
        foreach (var block in _blocks)
        {
            block.Hited.RemoveListener(OnBlockHit);
        }
    }

    private void OnBlockHit(int blockPosition)
    {
        List<List<Block>> comboBlockList = GetListBlockCombo();

        if (IsGotBigCombo(in comboBlockList))
            BreakCombos(in comboBlockList);

        if (!_face.IsFaceMissing())
        {
            if (_blocks.Count >= _minBlockCombo)
                _face.PlayFaceChange();
            else
                _face.SetDeadFace();
        }

        if (IsKilled())
            DestroyRemnants();

        if (_blocks.Count > 0)
        {
            _animationController.PlayAnimation(_blocks, blockPosition);
        }
    }

    private List<List<Block>> GetListBlockCombo()
    {
        var comboBlockList = new List<List<Block>>();
        int listCounter = 0;

        comboBlockList.Add(new List<Block>());
        comboBlockList[listCounter].Add(_blocks[0]);

        for (int i = 1; i < _blocks.Count; i++)
        {
            if (_blocks[i].MaterialColor == _blocks[i - 1].MaterialColor)
            {
                comboBlockList[listCounter].Add(_blocks[i]);
            }
            else
            {
                comboBlockList.Add(new List<Block>());
                listCounter++;
                comboBlockList[listCounter].Add(_blocks[i]);
            }
        }

        return comboBlockList;
    }

    private bool IsGotBigCombo(in List<List<Block>> comboBlockList)
    {
        foreach (var list in comboBlockList)
        {
            if (list[0].MaterialColor != _baseBlockColor)
            {
                if (list.Count >= _minBlockCombo)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void BreakCombos(in List<List<Block>> comboBlockList)
    {
        bool isComboBroke = false;

        foreach (var list in comboBlockList)
        {
            if (!isComboBroke)
            {
                if (list[0].MaterialColor != _baseBlockColor)
                {
                    if (list.Count >= _minBlockCombo)
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            _blocks.Remove(list[i]);
                            list[i].DestroyItself();
                        }

                        isComboBroke = true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < list.Count; i++)
                {
                    _blocks.Remove(list[i]);
                    list[i].FreeItself();
                    list[i].Explosion.Explode();
                }
            }
        }
    }

    private bool IsKilled()
    {
        return _blocks.Count < _minBlockCombo;
    }

    private void DestroyRemnants()
    {
        EnemyDead?.Invoke();
        Destroy(gameObject, _destroyDelay);
    }
}
