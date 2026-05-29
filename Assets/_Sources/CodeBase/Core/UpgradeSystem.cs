using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class UpgradeSystem : MonoBehaviour, ISaveable
{
    [SerializeField] private UpgradeDataSO[] _upgrades;

    private Dictionary<UpgradeDataSO, int> _currentLevel = new();

    public event Action<UpgradeDataSO, float> UpgradePurchased;
    public event Action<UpgradeDataSO, int> UpgradeProgressed;

    private void Awake()
    {
        GlobalContext.UpgradeSystem = this;
    }

    public bool TryBuy(UpgradeDataSO upgrade)
    {
        _currentLevel.TryGetValue(upgrade, out int level);

        double costUpgrade = upgrade.BaseCost * Math.Pow(upgrade.CostMultiplier, level);

        if (GlobalContext.WalletSystem.Balance < costUpgrade)
        {
            return false;
        }

        GlobalContext.WalletSystem.SpendMoney(costUpgrade);
        _currentLevel[upgrade] = level + 1;

        float damage = upgrade.BaseValue * Mathf.Pow(upgrade.ValueMultiplier, _currentLevel[upgrade]);

        UpgradePurchased?.Invoke(upgrade, damage);
        UpgradeProgressed?.Invoke(upgrade, _currentLevel[upgrade]);
        return true;
    }

    public void Save(GameData data)
    {
        Dictionary<string, int> levels = new Dictionary<string, int>();

        foreach (KeyValuePair<UpgradeDataSO, int> pair in _currentLevel)
            levels[pair.Key.Name] = pair.Value;

        data.upgradeLevels = levels;
    }

    public void Load(GameData data)
    {
        foreach (UpgradeDataSO upgrade in _upgrades)
        {
            if (data.upgradeLevels == null)
                return;

            if (data.upgradeLevels.TryGetValue(upgrade.Name, out int level))
            {
                _currentLevel[upgrade] = level;
                float damage = upgrade.BaseValue * Mathf.Pow(upgrade.ValueMultiplier, _currentLevel[upgrade]);
                UpgradePurchased?.Invoke(upgrade, damage);
                UpgradeProgressed?.Invoke(upgrade, _currentLevel[upgrade]);
            }
        }
    }
}
