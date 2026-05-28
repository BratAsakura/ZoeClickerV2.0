using System;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class UpgradeSystem : MonoBehaviour
{
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
}
