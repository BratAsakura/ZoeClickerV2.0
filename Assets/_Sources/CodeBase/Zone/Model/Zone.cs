using System;
using UnityEngine;

//[RequireComponent(typeof(ZoneView))]
public class Zone : MonoBehaviour
{
    public const int MaxKill = 10;

    [SerializeField] private int _level;
    [SerializeField] private int _countKills;

    private bool _isComplete = false;
    public int CountKills => _countKills;

    public event Action<int> KillGoalReached;
    public event Action<int> KillCount;

    private void OnEnable()
    {
        GlobalContext.Enemy.UnitDied += ProcessKill;
        GlobalContext.ZoneSystem.ZoneProgressRestored += SetLevel;
    }

    private void SetLevel(int level, int countKills, bool isComplete)
    {
        _level = level;
        _countKills = countKills;
        _isComplete = isComplete;
    }

    private void OnDisable()
    {
        GlobalContext.Enemy.UnitDied -= ProcessKill;
        GlobalContext.ZoneSystem.ZoneProgressRestored -= SetLevel;
    }

    private void ProcessKill()
    {
        if (_isComplete)
            return;

        _countKills++;
        KillCount?.Invoke(_countKills);

        if (_countKills != MaxKill)
            return;

        _isComplete = true;
        KillGoalReached?.Invoke(_countKills);
    }
}