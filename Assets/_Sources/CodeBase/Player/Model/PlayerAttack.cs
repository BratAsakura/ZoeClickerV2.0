using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float _damage;

    private void OnEnable()
    {
        GlobalContext.UpgradeSystem.UpgradePurchased += OnUpgradePurchased;
    }

    private void OnDisable()
    {
        GlobalContext.UpgradeSystem.UpgradePurchased -= OnUpgradePurchased;
    }

    public void Attack(IDamageable target)
    {
        target.TakeDamage(_damage);
    }

    public void IncreaseDamage(float value)
    {
        _damage += value;
    }

    private void OnUpgradePurchased(UpgradeDataSO upgrade, float damage)
    {
        if (upgrade.Type != UpgradeType.Click)
            return;

        IncreaseDamage(damage);
    }
}