using System;
using System.Collections.Generic;
using UnityEngine;

public class ZoneSystem : MonoBehaviour
{
    [SerializeField] private Zone _activeZone;
    private int _currentLevel;
    private int _maxUnlockedLevel;
    private Dictionary<int, int> _progressInZone = new();

    public event Action<int> ZoneLevelChanged;
    public event Action<int, int> ZoneProgressRestored;

    private void Awake()
    {
        GlobalContext.ZoneSystem = this;
    }

    private void OnEnable()
    {
        _activeZone.KillGoalReached += OnKillGoalReached;
    }

    private void OnDisable()
    {
        _activeZone.KillGoalReached -= OnKillGoalReached;
    }

    private void OnKillGoalReached(int count)
    {
        _progressInZone[_currentLevel] = count;
        UnlockNextLevel();
    }

    private void SetLevel(int level)
    {
        if (level > _maxUnlockedLevel || level <= 0)
            return;

        _currentLevel = level;
        _progressInZone.TryGetValue(_currentLevel, out int savedProgress);
        ZoneLevelChanged?.Invoke(_currentLevel);
        ZoneProgressRestored?.Invoke(_currentLevel, savedProgress);
    }

    private void UnlockNextLevel()
    {
        _maxUnlockedLevel++;
        _progressInZone.Add(_maxUnlockedLevel, 0);
        SetLevel(_maxUnlockedLevel);
    }
}