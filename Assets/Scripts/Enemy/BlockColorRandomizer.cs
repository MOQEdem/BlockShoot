using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockColorRandomizer : MonoBehaviour
{
    [SerializeField] private List<Color> _colors;
    [SerializeField] private int _numberOfRandom;

    public void RandomizeBlockColors(List<Block> blocks)
    {
        for (int i = 0; i < _numberOfRandom; i++)
        {
            Color color = _colors[Random.Range(0, _colors.Count)];
            color.a = 1f;
            blocks[Random.Range(0, blocks.Count)].SetColor(color);
        }
    }
}
