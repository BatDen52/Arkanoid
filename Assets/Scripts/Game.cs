using System;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private int _startTryCount = 3;
    [SerializeField] private BallSpawner _ballSpawner;

    private int _tryCount;

    public event Action<int> TryCountChanged;

    private void Awake()
    {
        _tryCount = _startTryCount;
        TryCountChanged?.Invoke(_tryCount);
    }

    private void OnEnable()
    {
        _ballSpawner.BallDestroyed += OnBallDestroyed;
    }

    private void OnDisable()
    {
        _ballSpawner.BallDestroyed -= OnBallDestroyed;
    }

    private void Start()
    {
        _ballSpawner.Spawn();
    }

    private void OnBallDestroyed()
    {
        _tryCount--;

        TryCountChanged?.Invoke(_tryCount);

        if (_tryCount == 0)
            FindObjectOfType<GameEntryPoint>().GameStop();
        else
            _ballSpawner.Spawn();
    }
}
