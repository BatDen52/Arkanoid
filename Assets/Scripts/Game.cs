using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private int _startTryCount = 3;
    [SerializeField] private BallSpawner _ballSpawner;
    [SerializeField] private BlockSpawner _blockSpawner;
    [SerializeField] private GameMenu _menu;
    [SerializeField] private TMP_Text _scoreText;

    private int _tryCount;
    private int _score;

    public event Action<int> TryCountChanged;
    public event Action Lose;

    public int TryCount => _tryCount;

    private void Awake()
    {
        _tryCount = _startTryCount;
    }

    private void OnEnable()
    {
        _ballSpawner.BallDestroyed += OnBallDestroyed;
        _blockSpawner.BlockDestroyed += OnBlockDestroyed;
        _blockSpawner.AllBlockDestroyed += OnAllBlockDestroyed;
    }

    private void OnDisable()
    {
        _ballSpawner.BallDestroyed -= OnBallDestroyed;
        _blockSpawner.BlockDestroyed -= OnBlockDestroyed;
        _blockSpawner.AllBlockDestroyed -= OnAllBlockDestroyed;
    }

    private void Start()
    {
        _ballSpawner.Spawn();
        TryCountChanged?.Invoke(_tryCount);
    }

    private void OnBallDestroyed()
    {
        _tryCount--;

        TryCountChanged?.Invoke(_tryCount);

        if (_tryCount == 0)
        {
            Lose?.Invoke();
            _menu.OpenFailWindow();
        }
        else
        {
            _ballSpawner.Spawn();
        }
    }

    private void OnBlockDestroyed(int reward)
    {
        _score += reward;
        _scoreText.text = "Score: " + _score;
    }

    private void OnAllBlockDestroyed()
    {
        _menu.OpenWinWindow();
    }
}
