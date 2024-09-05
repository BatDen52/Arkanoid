using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private Ball _prefab;
    [SerializeField] private Racket _racket;
    [SerializeField] private ForceView _forceView;

    private List<Ball> _balls = new();

    public event Action BallDestroyed;

    public void Spawn()
    {
        Ball ball = _balls.FirstOrDefault(i => i.gameObject.activeSelf == false);

        if (ball == null)
        {
            ball = Instantiate(_prefab);
            _balls.Add(ball);
        }

        _forceView.SetBall(ball);

        ball.Rest(_racket);

        ball.Died += OnDie;
        _racket.SetBull(ball);

        ball.gameObject.SetActive(true);
    }

    private void OnDie(Ball ball)
    {
        ball.Died -= OnDie;
        ball.gameObject.SetActive(false);

        BallDestroyed?.Invoke();
    }
}
