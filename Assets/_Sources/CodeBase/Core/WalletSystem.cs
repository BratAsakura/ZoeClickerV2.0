using System;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class WalletSystem : MonoBehaviour
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

    public static string FormatBalance(double money)
    {
        const int Threshold = 1000;
        int index = 0;

        string[] suffixes =
            {
                "", "K", "M", "B", "T", "q", "Q", "s", "S",
                "Sp", "Oc", "No", "Dc", "Un", "Du", "Tr", "Qt", "Qi", "Se",
                "SpT", "OcT", "NoT", "DcT", "UnT", "DuT", "TrT"
            };

        while (money >= Threshold && index < suffixes.Length - 1)
        {
            money /= Threshold;
            index++;
        }

        string formatted = money % 1 == 0 ? money.ToString("F0") : money.ToString("F1");

        return formatted + suffixes[index];
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