using System;
using TMPro;
using UnityEngine;
using UnityEngine.U2D.Animation;

[RequireComponent(typeof(SpriteRenderer))]
public class Block : MonoBehaviour
{
    [SerializeField] private Canvas _armoreIcon;
    [SerializeField] private TMP_Text _armoreText;

    private BlockInfo _info;
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

    public void Init(BlockInfo blockInfo)
    {
        _info = blockInfo;
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

        Damaging?.Invoke(); //FindObjectOfType<AudioManager>().Play("hitBlock");

        if (_currentStrength == 0)
        {
            Destroy(gameObject);
            Died?.Invoke(this);
            return;
        }
 
        _spriteRenderer.color = _info.Stages[_info.Stages.Count - _currentStrength].Color;
        _spriteRenderer.sprite = _info.Stages[_info.Stages.Count - _currentStrength].Sprite;
    }
}
