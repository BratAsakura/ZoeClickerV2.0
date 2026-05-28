using UnityEngine;

public class PlayerPassiveAttack : MonoBehaviour
{
    [SerializeField] private float _dps;

    private float _timer = 0;

    private void OnEnable()
    {
        GlobalContext.UpgradeSystem.UpgradePurchased += OnUpgradePurchased;
    }

    private void Update()
    {
        if (_dps == 0)
            return;

        _timer += Time.deltaTime;

        if (_timer >= 1f)
        {
            _timer = 0;
            GlobalContext.Enemy.TakeDamage(_dps);
        }
    }

    private void OnDisable()
    {
        GlobalContext.UpgradeSystem.UpgradePurchased -= OnUpgradePurchased;
    }

    public void IncreaseDps(float value)
    {
        _dps += value;
    }

    private void OnUpgradePurchased(UpgradeDataSO upgrade, float damage)
    {
        if (upgrade.Type != UpgradeType.PassiveDps)
            return;

        IncreaseDps(damage);
    }
}