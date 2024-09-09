using System;
using System.Collections;
using UnityEngine;

public class BorderToBorderMover : MonoBehaviour
{
    [SerializeField] private bool _playOnAwake = false;
    [SerializeField] private float _speed = 10;

    private Vector3[] _directions = { Vector3.left, Vector3.right };
    private int _directionIndex = 0;

    private void Awake()
    {
        if (_playOnAwake)
            StartMove();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Border>(out _) ||
            collision.gameObject.TryGetComponent<Block>(out _))
            _directionIndex = ++_directionIndex % _directions.Length;
    }

    public void StartMove()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (enabled)
        {
            transform.transform.Translate(_directions[_directionIndex] * _speed * Time.deltaTime);
            yield return null;
        }
    }
}
