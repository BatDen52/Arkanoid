using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(ParticleSystem))]
public class Ball : MonoBehaviour
{
    private const float FoceProgressionCoefficient = 0.5f;

    [SerializeField] private float _speed = 100.0f;
    [SerializeField] private int _damage = 1;

    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private ParticleSystem _particle;
    private Transform _transform;
    private int _force = 1;
    private float _forceFactor = 1;

    public int Force
    {
        get => _force;
        private set
        {
            _force = value;
            ForceChenged?.Invoke(_force);
        }
    }

    public event Action<Ball> Died;
    public event Action<int> ForceChenged;
    public event Action Hiting;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _particle = GetComponent<ParticleSystem>();
        _transform = transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Block block))
            block.TakeDamage(_damage);

        if (collision.gameObject.TryGetComponent<Racket>(out _))
            Hit(collision.collider);

        if (collision.gameObject.TryGetComponent<DeadZone>(out _))
            Die();
    }

    public void Rest(Racket racket)
    {
        _transform.position = racket.BallPosition;
        _transform.parent = racket.transform;
        _transform.rotation = Quaternion.identity;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.isKinematic = true;
        _collider.enabled = false;
        _particle.Stop(); 
        Force = 1;
        _forceFactor = 1;
    }

    public void Shoot()
    {
        _transform.parent = null;
        _collider.enabled = true;
        _rigidbody.isKinematic = false;
        _particle.Play();
        _rigidbody.velocity = Vector2.up * _speed;
    }

    public void Push(int force = 1)
    {
        Force += force;
        _forceFactor += MathF.Exp(-Force * FoceProgressionCoefficient);
    }

    private void Hit(Collider2D collider)
    {
        Hiting?.Invoke();//FindObjectOfType<AudioManager>().Play("hitRacket");

        float hitFactor = CalculateHitFactor(transform.position, collider.transform.position, collider.bounds.size.x);
        Vector2 newDirection = new Vector2(hitFactor, 1).normalized;

        _rigidbody.velocity = newDirection * _speed * _forceFactor;
    }

    private float CalculateHitFactor(Vector2 ballPos, Vector2 racketPos, float racketWidth)
    {
        return (ballPos.x - racketPos.x) / racketWidth;
    }

    private void Die()
    {
        Died?.Invoke(this);
    }
}
