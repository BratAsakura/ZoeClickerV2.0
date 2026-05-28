using System;
using TMPro;
using UnityEngine;

public class UpgradeView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private TextMeshProUGUI _nameText;

    private UpgradeDataSO _data;

    private void Awake()
    {
        _levelText.SetText("Level: 0");
    }

    private void OnEnable()
    {
        GlobalContext.UpgradeSystem.UpgradeProgressed += OnUpgradeProgressed;
    }

    private void OnDisable()
    {
        GlobalContext.UpgradeSystem.UpgradeProgressed -= OnUpgradeProgressed;
    }

    public void Init(UpgradeDataSO data)
    {
        _data = data;
        _nameText.SetText(data.Name);
        _costText.SetText("Cost: " + data.BaseCost.ToString());
    }

    private void OnUpgradeProgressed(UpgradeDataSO upgrade, int level)
    {
        if (upgrade != _data)
            return;

        _levelText.SetText($"Level: {level}");
        _costText.SetText($"Cost: {NumberFormatter.Format(upgrade.BaseCost * Math.Pow(upgrade.CostMultiplier, level))}");
    }
}
