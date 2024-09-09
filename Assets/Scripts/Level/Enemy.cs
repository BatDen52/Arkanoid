using UnityEngine;

[RequireComponent (typeof(BorderToBorderMover))]
public class Enemy : Block
{
    private BorderToBorderMover _mover;

    private void Awake()
    {
        _mover = GetComponent<BorderToBorderMover> ();
    }

    private void Start()
    {
        _mover.StartMove();
    }

    public override void TakeDamage(int damage)
    {

    }
}
