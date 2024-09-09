using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    private const int MaxRawCount = 20;
    private const int RawYOffset = 10;
    private const int BlockXStartPosition = 8;
    private const int BlockXOffset = 18;

    [SerializeField] private Transform _rawsContainer;
    [SerializeField] private RectTransform _rawTemplate;
    [SerializeField] private List<BlockRawInfo> _rawsData = new();

    private AudioManager _audioManager;
    private List<Block> _blocks = new();

    public event Action<int> BlockDestroyed;
    public event Action<int> AllSpawned;
    public event Action<int> BlocksCountChanged;
    public event Action AllBlockDestroyed;

    private void OnValidate()
    {
        while (_rawsData.Count > MaxRawCount)
            _rawsData.RemoveAt(_rawsData.Count - 1);

        foreach (BlockRawInfo raw in _rawsData)
            raw.Validate();
    }

    private void Start()
    {
        for (int i = 0; i < _rawsData.Count; i++)
        {
            RectTransform raw = Instantiate(_rawTemplate, _rawsContainer);
            raw.localPosition = Vector3.down * RawYOffset * i;

            for (int j = 0; j < _rawsData[i].Blocks.Count; j++)
            {
                if (_rawsData[i].Blocks[j] == null)
                    continue;

                Block block = Instantiate(_rawsData[i].Blocks[j].Prefab, raw.transform);

                block.Init(_rawsData[i].Blocks[j], _audioManager);
                block.Died += OnDied;

                (block.transform as RectTransform).anchoredPosition =
                    new Vector3(BlockXOffset * j + BlockXStartPosition, _rawTemplate.sizeDelta.y / 2, 0);

                if (block is not Enemy)
                    _blocks.Add(block);
            }
        }

        AllSpawned?.Invoke(_blocks.Count);
    }

    private void OnDestroy()
    {
        foreach (Block block in _blocks)
            block.Died -= OnDied;
    }

    public void Init(AudioManager audioManager)
    {
        _audioManager = audioManager;
    }

    private void OnDied(Block block)
    {
        block.Died -= OnDied;

        BlockDestroyed?.Invoke(block.Reward);

        _blocks.Remove(block);

        BlocksCountChanged?.Invoke(_blocks.Count);

        if (_blocks.Count == 0)
            AllBlockDestroyed?.Invoke();
    }
}