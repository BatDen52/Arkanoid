using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Tutorial : MonoBehaviour
{
    private const int PushDistance = 20;
    private const float NeededDirectionValue = 0.75f;

    [SerializeField] private GameObject _shootPanel;
    [SerializeField] private GameObject _movePanel;
    [SerializeField] private GameObject _pushPanel;

    private IInputReader _inputReader;
    private BallSpawner _ballSpawner;
    private Racket _racket;
    private Ball _ball;

    public void Init(IInputReader inputReader, Racket racket, BallSpawner ballSpawner)
    {
        _inputReader = inputReader;
        _ballSpawner = ballSpawner;
        _racket = racket;

        if (ballSpawner != null)
        {
            ballSpawner.BallSpawned += OnBallSpawned;
        }

        StartCoroutine(Show());
    }

    private IEnumerator Show()
    {
        if (_shootPanel != null && _racket != null)
            yield return StartCoroutine(ShowShoot());

        if (_movePanel != null)
            yield return StartCoroutine(ShowMove());

        if (_ballSpawner != null && _pushPanel != null && _racket != null && _ball != null)
            yield return StartCoroutine(ShowPush());
    }

    private IEnumerator ShowShoot()
    {
        TimeManager.Pause();
        _shootPanel.SetActive(true);
        _shootPanel.transform.DOScale(_shootPanel.transform.localScale * 1.25f, 1)
            .SetLoops(-1, LoopType.Yoyo)
            .SetUpdate(true);
        yield return new WaitWhile(() => _inputReader.GetIsPush() == false);
        TimeManager.Run();
        _shootPanel.SetActive(false);
        _racket.Shoot();
    }

    private IEnumerator ShowMove()
    {
        _movePanel.SetActive(true);
        yield return new WaitWhile(() => Mathf.Abs(_inputReader.Direction) < NeededDirectionValue);
        _movePanel.SetActive(false);
    }

    private IEnumerator ShowPush()
    {
        yield return new WaitWhile(
            () =>
                (_ball.transform.position - _racket.transform.position).sqrMagnitude < PushDistance * PushDistance
            );
        yield return new WaitWhile(
            () =>
                (_ball.transform.position - _racket.transform.position).sqrMagnitude > PushDistance * PushDistance
            );
        yield return StartCoroutine(ShowShoot());
    }

    private void OnBallSpawned(Ball ball)
    {
        _ballSpawner.BallSpawned -= OnBallSpawned;
        _ball = ball;
    }
}