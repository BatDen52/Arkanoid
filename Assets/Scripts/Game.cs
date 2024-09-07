using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    private const string LEVEL_SCENE_SUBNAME = "Level";

    [SerializeField] private int _startTryCount = 3;

    private BallSpawner _ballSpawner;
    private BlockSpawner _blockSpawner;
    private GameMenu _menu;
    private TMP_Text _levelNumberText;
    private TMP_Text _scoreText;
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

    public void Init(BallSpawner ballSpawner, BlockSpawner blockSpawner, GameMenu menu, 
        TMP_Text scoreText, TMP_Text levelNumberText)
    {
        _ballSpawner = ballSpawner;
        _blockSpawner = blockSpawner;
        _menu = menu;
        _scoreText = scoreText;
        _levelNumberText = levelNumberText;

        _levelNumberText.text = $"{LEVEL_SCENE_SUBNAME} {SceneManager.GetActiveScene().buildIndex}";
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
