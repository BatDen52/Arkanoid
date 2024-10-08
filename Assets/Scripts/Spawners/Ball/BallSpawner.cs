using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private Ball _prefab;

    private Racket _racket;
    private AudioManager _audioManager;
    private ForceView _forceView;
    private List<Ball> _balls = new();

    public event Action<Ball> BallSpawned;
    public event Action BallDestroyed;

    public void Init(Racket racket, ForceView forceView, AudioManager audioManager)
    {
        _racket = racket;
        _forceView = forceView;
        _audioManager = audioManager;
    }

    public void Spawn()
    {
        Ball ball = _balls.FirstOrDefault(i => i.gameObject.activeSelf == false);

        if (ball == null)
        {
            ball = Instantiate(_prefab);
            ball.Init(_audioManager);
            _balls.Add(ball);
        }

        _forceView.SetBall(ball);

        ball.Rest(_racket);

        ball.Died += OnDie;
        _racket.SetBull(ball);

        ball.gameObject.SetActive(true);

        BallSpawned?.Invoke(ball);
    }

    private void OnDie(Ball ball)
    {
        ball.Died -= OnDie;
        ball.gameObject.SetActive(false);

        BallDestroyed?.Invoke();
    }
}
