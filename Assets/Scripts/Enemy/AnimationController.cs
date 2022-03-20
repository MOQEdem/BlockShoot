using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private float _animationDelay;

    public void PlayAnimation(List<Block> blocks, int hitBlock)
    {
        hitBlock = Mathf.Clamp(hitBlock, 0, blocks.Count - 1);
        blocks[hitBlock].Animation.PlayHitAnimation();
        StartCoroutine(PlayAnimationToTail(blocks, hitBlock));
        StartCoroutine(PlayAnimationToHead(blocks, hitBlock));
    }

    private IEnumerator PlayAnimationToHead(List<Block> blocks, int hitBlock)
    {
        var _delayTime = new WaitForSeconds(_animationDelay);

        for (int i = hitBlock + 1; i < blocks.Count; i++)
        {
            blocks[i].Animation.PlayHitAnimation();

            yield return _delayTime;
        }
    }

    private IEnumerator PlayAnimationToTail(List<Block> blocks, int hitBlock)
    {
        var _delayTime = new WaitForSeconds(_animationDelay);

        for (int i = hitBlock - 1; i >= 0; i--)
        {
            yield return _delayTime;

            blocks[i].Animation.PlayHitAnimation();
        }
    }
}
