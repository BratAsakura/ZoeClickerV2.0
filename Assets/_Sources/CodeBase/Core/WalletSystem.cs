using System;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class WalletSystem : MonoBehaviour, ISaveable
{
    private readonly byte _baseReward = 1;

    [SerializeField] private double _balance = 0;
    [SerializeField] private float _coefficient = 1.15f;

    private int _currentZoneLevel;

    public event Action<double> BalanceChanged;

    public double Balance => _balance;

    private void Awake()
    {
        GlobalContext.WalletSystem = this;
    }

    private void OnEnable()
    {
        GlobalContext.Enemy.UnitDied += OnUnitDied;
        GlobalContext.ZoneSystem.ZoneLevelChanged += OnZoneLevelChanged;
    }

    private void OnValidate()
    {
        if (_balance <= 0)
            _balance = 0;

        BalanceChanged?.Invoke(_balance);
    }

    private void OnDisable()
    {
        GlobalContext.Enemy.UnitDied -= OnUnitDied;
        GlobalContext.ZoneSystem.ZoneLevelChanged -= OnZoneLevelChanged;
    }

    public void AddMoney(double amount)
    {
        if (amount < 0)
            return;

        _balance += amount;
        BalanceChanged?.Invoke(_balance);
    }

    public void SpendMoney(double amount)
    {
        if (!CanAfford(amount))
        {
            return;
        }

        _balance -= amount;
        BalanceChanged?.Invoke(_balance);
    }

    public void Save(GameData data)
    {
        data.balance = _balance;
    }

    public void Load(GameData data)
    {
        _balance = data.balance;
        BalanceChanged?.Invoke(_balance);
    }

    private bool CanAfford(double amount) => _balance >= amount;

    private void OnZoneLevelChanged(int level)
    {
        _currentZoneLevel = level;
    }

    private void OnUnitDied()
    {
        if (_currentZoneLevel == 1)
        {
            AddMoney(_baseReward);
            return;
        }

        double reward = _baseReward * Math.Pow(_coefficient, _currentZoneLevel);

        AddMoney(reward);
    }
}