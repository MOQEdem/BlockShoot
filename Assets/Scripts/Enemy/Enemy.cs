using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Face))]
[RequireComponent(typeof(AnimationController))]
[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(BlockColorRandomizer))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private List<Block> _blocks;
    [SerializeField] private int _minBlockCombo;
    [SerializeField] private float _destroyDelay;
    [SerializeField] private float _delay—hainOfExplosions;
    [SerializeField] private float _comboFindingDelay;
    [SerializeField] private BlockColorRandomizer _randomizer;

    private Face _face;
    private EnemyMover _enemyMover;
    private Color _baseBlockColor;
    private AnimationController _animationController;

    public UnityAction Died;

    public EnemyMover Mover => _enemyMover;

    private void Awake()
    {
        _face = GetComponent<Face>();
        _enemyMover = GetComponent<EnemyMover>();
        _animationController = GetComponent<AnimationController>();
        _randomizer = GetComponent<BlockColorRandomizer>();

        _baseBlockColor = _blocks[0].MaterialColor;
        _randomizer.RandomizeBlockColors(_blocks);
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
        StartCoroutine(AnalyzeOfBlockComposition(GetListBlockCombo()));

        if (!_face.IsFaceMissing())
        {
            if (_blocks.Count >= _minBlockCombo)
                _face.PlayFaceChange();
            else
                _face.SetDeadFace();
        }

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
                        }

                        StartCoroutine(DestroyChainOfBlocks(list));

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
        Died?.Invoke();

        Destroy(gameObject, _destroyDelay);
    }

    private IEnumerator AnalyzeOfBlockComposition(List<List<Block>> comboBlockList)
    {
        var _delayTime = new WaitForSeconds(_comboFindingDelay);

        yield return _delayTime;

        if (IsGotBigCombo(in comboBlockList))
            BreakCombos(in comboBlockList);

        if (IsKilled())
            DestroyRemnants();
    }

    private IEnumerator DestroyChainOfBlocks(List<Block> blocks)
    {
        var delay—hainOfExplosions = new WaitForSeconds(_delay—hainOfExplosions);

        for (int i = blocks.Count - 1; i >= 0; i--)
        {
            blocks[i].DestroyItself();

            yield return delay—hainOfExplosions;
        }
    }
}
