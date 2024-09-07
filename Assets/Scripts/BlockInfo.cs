using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/BlockInfo", order = 0)]
public class BlockInfo : ScriptableObject
{
    [SerializeField] private int _reward;
    [SerializeField] private int _strength = 1;
    [SerializeField] private int _armoreLevel = 1;
    [SerializeField] private List<BlockStage> _stages;

    public int Reward => _reward;
    public int Strength => _strength;
    public int ArmoreLevel => _armoreLevel;
    public IReadOnlyList<BlockStage> Stages => _stages;


    private void OnValidate()
    {
        while (_stages.Count < _strength)
            _stages.Add(new BlockStage());

        while (_stages.Count > _strength)
            _stages.RemoveAt(_stages.Count - 1);
    }
}
