using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "Upgrades/UpgradeData")]
public class UpgradeDataSO : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private float _baseCost;
    [SerializeField] private int _baseValue;
    [SerializeField] private float _costMultiplier;
    [SerializeField] private float _valueMultiplier;
    [SerializeField] private UpgradeType _type;

    public string Name => _name;
    public int BaseValue => _baseValue;
    public float BaseCost => _baseCost;
    public float CostMultiplier => _costMultiplier;
    public float ValueMultiplier => _valueMultiplier;
    public UpgradeType Type => _type;
}
