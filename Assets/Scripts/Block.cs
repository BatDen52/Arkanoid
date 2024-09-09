using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Block : MonoBehaviour
{
    [SerializeField] private Canvas _armoreIcon;
    [SerializeField] private TMP_Text _armoreText;
    [SerializeField] private ParticleSystem _dieEffect;

    private BlockInfo _info;
    private AudioManager _audioManager;
    private int _currentStrength = 1; 
    private SpriteRenderer _spriteRenderer;

    public event Action Damaging;
    public event Action<Block> Died;

    public int Reward => _info.Reward;
    public int ArmoreLevel => _info.ArmoreLevel;
    public bool IsArmored => _info.ArmoreLevel > 1;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(BlockInfo blockInfo, AudioManager audioManager)
    {
        _info = blockInfo;
        _audioManager = audioManager;
        _currentStrength = _info.Strength;
        _spriteRenderer.sprite = _info.Stages[_info.Stages.Count - _currentStrength].Sprite;
        _spriteRenderer.color = _info.Stages[_info.Stages.Count - _currentStrength].Color;
        _armoreIcon.gameObject.SetActive(IsArmored);
        _armoreText.text = ArmoreLevel.ToString();
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0)
            return;

        _currentStrength -= damage;

        Damaging?.Invoke(); 
        _audioManager.Play(ConstantsData.AudioData.HitBlock);

        if (_currentStrength == 0)
        {
            Instantiate(_dieEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Died?.Invoke(this);
            return;
        }
 
        _spriteRenderer.color = _info.Stages[_info.Stages.Count - _currentStrength].Color;
        _spriteRenderer.sprite = _info.Stages[_info.Stages.Count - _currentStrength].Sprite;
    }
}
