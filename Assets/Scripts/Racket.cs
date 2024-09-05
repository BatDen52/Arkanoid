using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(IInputReader), typeof(Collider2D))]
public class Racket : MonoBehaviour
{
    [SerializeField] private float _speed = 150;
    [SerializeField] private float _pushDelay = 0.2f;
    [SerializeField] private float _pushCooldown = 0.5f;
    [SerializeField] private Transform _ballPosition;

    private IInputReader _inputReader;
    private WaitForSeconds _waitPushDelay;
    private WaitForSeconds _waitPushCooldown;
    private Rigidbody2D _rigidbody;
    private List<Ball> _balls = new();
    private bool _isPushing;
    private bool _canPush = true;

    public Vector3 BallPosition => _ballPosition.position;
    private bool _hasBalls => _balls.Count > 0;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputReader = GetComponent<IInputReader>();
        _waitPushDelay = new WaitForSeconds(_pushDelay);
        _waitPushCooldown = new WaitForSeconds(_pushCooldown);
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _inputReader.Direction * Vector2.right * _speed;

        if (_inputReader.GetIsPush())
        {
            if (_hasBalls)
            {
                _balls[0].Shoot();
                _balls.RemoveAt(0);
            }
            else if (_canPush)
            {
                StartCoroutine(Push());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isPushing && collision.gameObject.TryGetComponent(out Ball ball))
            ball.Push();
    }

    public void SetBull(Ball ball)
    {
        _balls.Add(ball);
    }

    private IEnumerator Push()
    {
        _canPush = false;
        _isPushing = true;
        yield return _waitPushDelay;
        _isPushing = false;
        yield return _waitPushCooldown;
        _canPush = true;
    }
}
