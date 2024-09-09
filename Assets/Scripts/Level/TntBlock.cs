using UnityEngine;

public class TntBlock : Block
{
    [SerializeField] private int _damage = 1;

    private RectTransform _transform;

    protected override void Awake()
    {
        base.Awake();

        _transform = transform as RectTransform;
    }

    protected override void Die()
    {
        Detonate();

        base.Die();
    }

    private void Detonate()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(_transform.position, _transform.sizeDelta * 2, 0);

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out Block block))
                block.TakeDamage(_damage);
        }

        AudioManager.Play(ConstantsData.AudioData.Boom);
    }
}
