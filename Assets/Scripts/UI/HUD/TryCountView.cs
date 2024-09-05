using System;
using TMPro;
using UnityEngine;

public class TryCountView : CountView
{
    [SerializeField] private Game _data;

    private void OnEnable()
    {
        _data.TryCountChanged += OnCountChanged;
    }

    private void OnDisable()
    {
        _data.TryCountChanged -= OnCountChanged;
    }
}
