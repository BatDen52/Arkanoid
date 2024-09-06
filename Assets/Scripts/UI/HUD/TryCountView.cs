using UnityEngine;

public class TryCountView : CountView
{
    [SerializeField] private Game _data;

    private void OnEnable()
    {
        _data.TryCountChanged += OnCountChanged;
        OnCountChanged(_data.TryCount);
    }

    private void OnDisable()
    {
        _data.TryCountChanged -= OnCountChanged;
    }
}
