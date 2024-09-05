using System;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private int _strength = 1;

    public event Action Damaging;
    public event Action Died;

    public void TakeDamage(int damage)
    {
        if (damage <= 0)
            return;

        _strength -= damage;
        Damaging?.Invoke(); //FindObjectOfType<AudioManager>().Play("hitBlock");

        if (_strength == 0)
        {
            Destroy(gameObject);
            Died?.Invoke();
        }
    }
}
