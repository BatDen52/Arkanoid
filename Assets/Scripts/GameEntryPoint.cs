using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEntryPoint : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private BallSpawner _ballSpawner;
    [SerializeField] private BlockSpawner _blockSpawner;
    [SerializeField] private GameMenu _menu;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Racket _racket;
    [SerializeField] private TMP_Text _levelNumberText;
    [SerializeField] private ForceView _forceView;
    [SerializeField] private Tutorial _tutorial;

    private void Awake()
    {
        _game.Init(_ballSpawner, _blockSpawner, _menu, _scoreText, _levelNumberText);
        _ballSpawner.Init(_racket, _forceView);
        _tutorial?.Init(_inputReader, _racket, _ballSpawner);

        _game.enabled = true;
    }
}
