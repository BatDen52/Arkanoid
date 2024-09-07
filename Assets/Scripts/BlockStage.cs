using System;
using UnityEngine;

[Serializable]
public class BlockStage
{
    [SerializeField] private Color _color;
    [SerializeField] private Sprite _sprite;

    public Color Color => _color;
    public Sprite Sprite => _sprite;
}