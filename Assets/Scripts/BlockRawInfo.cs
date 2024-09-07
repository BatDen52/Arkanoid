using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BlockRawInfo
{
    private const int MaxRawLength = 11;

    [SerializeField] private List<BlockInfo> _blocks = new();

    public IReadOnlyList<BlockInfo> Blocks => _blocks;

    public void Validate()
    {
        while (_blocks.Count > MaxRawLength)
            _blocks.RemoveAt(_blocks.Count - 1);
    }
}
