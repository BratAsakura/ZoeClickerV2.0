using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ZoneView))]
public class Zone : MonoBehaviour
{
    private const int MaxKill = 10;

    [SerializeField] private int _level;
    [SerializeField] private int _countKills;

    private bool _isComplete = false;

    public event Action<int> KillGoalReached;

    private void OnEnable()
    {
        GlobalContext.Enemy.UnitDied += ProcessKill;
        GlobalContext.ZoneSystem.ZoneProgressRestored += SetLevel;
    }

    private void SetLevel(int level, int countKills)
    {
        _level = level;
        _countKills = countKills;
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

        if (_countKills != MaxKill)
            return;

        KillGoalReached?.Invoke(_countKills);
        _isComplete = true;
    }
}