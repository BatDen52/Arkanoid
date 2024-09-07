using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    private const int MaxRawCount = 10;
    private const int RawYOffset = 10;
    private const int BlockXStartPosition = 8;
    private const int BlockXOffset = 18;

    [SerializeField] private Block _prefab;
    [SerializeField] private Transform _rawsContainer;
    [SerializeField] private RectTransform _rawTemplate;
    [SerializeField] private List<BlockRawInfo> _rawsData = new();

    private List<Block> _blocks = new();

    public event Action<int> BlockDestroyed;
    public event Action AllBlockDestroyed;

    private void OnValidate()
    {
        while (_rawsData.Count > MaxRawCount)
            _rawsData.RemoveAt(_rawsData.Count - 1);

        foreach (BlockRawInfo raw in _rawsData)
            raw.Validate();
    }

    private void Awake()
    {
        for (int i = 0; i < _rawsData.Count; i++)
        {
            RectTransform raw = Instantiate(_rawTemplate, _rawsContainer);
            raw.localPosition = Vector3.down * RawYOffset * i;

            for (int j = 0; j < _rawsData[i].Blocks.Count; j++)
            {
                if (_rawsData[i].Blocks[j] == null)
                    continue;

                Block block = Instantiate(_prefab, raw.transform);

                block.Init(_rawsData[i].Blocks[j]);
                block.Died += OnDied;

                (block.transform as RectTransform).anchoredPosition =
                    new Vector3(BlockXOffset * j + BlockXStartPosition, _rawTemplate.sizeDelta.y / 2, 0);

                _blocks.Add(block);
            }
        }
    }

    private void OnDied(Block block)
    {
        block.Died -= OnDied;

        BlockDestroyed?.Invoke(block.Reward);

        _blocks.Remove(block);

        if (_blocks.Count == 0)
            AllBlockDestroyed?.Invoke();
    }
}