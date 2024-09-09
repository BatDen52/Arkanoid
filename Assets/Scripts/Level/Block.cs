using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Block : MonoBehaviour
{
    [SerializeField] private Canvas _armoreIcon;
    [SerializeField] private TMP_Text _armoreText;
    [SerializeField] private ParticleSystem _dieEffect;

    protected AudioManager AudioManager;
    private BlockInfo _info;
    private int _currentStrength = 1;
    private SpriteRenderer _spriteRenderer;

    public event Action Damaging;
    public event Action<Block> Died;

    public int Reward => _info.Reward;
    public int ArmoreLevel => _info.ArmoreLevel;
    public bool IsArmored => _info.ArmoreLevel > 1;

    protected virtual void Awake()
    {
        _spriteRenderer ??= GetComponent<SpriteRenderer>();
    }

    public void Init(BlockInfo blockInfo, AudioManager audioManager)
    {

        _info = blockInfo;
        AudioManager = audioManager;
        _currentStrength = _info.Strength;
        RefreshView();

        if (_armoreIcon != null)
            _armoreIcon.gameObject.SetActive(IsArmored);

        if (_armoreText != null)
            _armoreText.text = ArmoreLevel.ToString();
    }

    private void RefreshView()
    {
        if (_info.Stages.Count < _currentStrength)
            return;

        _spriteRenderer ??= GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _info.Stages[_info.Stages.Count - _currentStrength].Sprite;
        _spriteRenderer.color = _info.Stages[_info.Stages.Count - _currentStrength].Color;
    }

    public virtual void TakeDamage(int damage)
    {
        if (damage <= 0)
            return;

        _currentStrength = Mathf.Clamp(_currentStrength - damage, 0, _info.Strength);

        Damaging?.Invoke();
        AudioManager.Play(ConstantsData.AudioData.HitBlock);

        if (_currentStrength == 0)
        {
            if (_dieEffect != null)
                Instantiate(_dieEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);
            Die();
            return;
        }

        RefreshView();
    }

    protected virtual void Die()
    {
        Died?.Invoke(this);
    }
}
