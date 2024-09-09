using System;
using TMPro;
using UnityEngine;

public class BlockCountView : CountView
{
    [SerializeField] private BlockSpawner _spawner;
    [SerializeField] private TMP_Text _totalText;

    private void Awake()
    {
        _spawner.AllSpawned += OnAllSpawned;
    }

    private void OnEnable()
    {
        _spawner.BlocksCountChanged += OnCountChanged;
    }

    private void OnDisable()
    {
        _spawner.BlocksCountChanged -= OnCountChanged;
    }

    private void OnAllSpawned(int count)
    {
        _spawner.AllSpawned -= OnAllSpawned;
        _totalText.text = count.ToString();
        OnCountChanged(count);
    }
}