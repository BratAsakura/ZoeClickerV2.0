using System;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-20)]
public class ZoneSystem : MonoBehaviour, ISaveable
{
    [SerializeField] private Zone _activeZone;
    private int _currentLevel;
    private int _maxUnlockedLevel;
    private Dictionary<int, int> _progressInZone = new();

    public event Action<int> ZoneLevelChanged;
    public event Action<int, int, bool> ZoneProgressRestored;

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

    public void Save(GameData data)
    {
        data.currentZoneLevel = _currentLevel;
        data.maxUnlockedLevel = _maxUnlockedLevel;
        data.zoneProgress = _progressInZone;
    }

    public void Load(GameData data)
    {
        if (data == null)
        {
            InitDefault();
            return;
        }

        _currentLevel = data.currentZoneLevel;
        _maxUnlockedLevel = data.maxUnlockedLevel;
        _progressInZone = data.zoneProgress ?? new Dictionary<int, int>();
        SetLevel(_currentLevel);
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
        ZoneProgressRestored?.Invoke(_currentLevel, savedProgress, savedProgress >= Zone.MaxKill);
    }

    private void UnlockNextLevel()
    {
        _maxUnlockedLevel++;
        _progressInZone.Add(_maxUnlockedLevel, 0);
        SetLevel(_maxUnlockedLevel);
    }

    private void InitDefault()
    {
        _maxUnlockedLevel = 1;
        _progressInZone.Add(1, 0);
        SetLevel(1);
    }
}