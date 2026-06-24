using System;
using TMPro;
using UnityEngine;

public class ZoneView : MonoBehaviour
{
    //Это должно быть UI HUD
    [SerializeField] private TextMeshProUGUI _levelView; // Это надо заменить на sprite
    [SerializeField] private TextMeshProUGUI _progressKill;
    [SerializeField] private Zone _zone;

    private void OnEnable()
    {
        GlobalContext.ZoneSystem.ZoneLevelChanged += SetLevel;
        GlobalContext.ZoneSystem.ZoneProgressRestored += OnProgressRestored;
        _zone.KillCount += OnKills;
    }

    private void OnKills(int value)
    {
        _progressKill.SetText($"Kill: {value}/10");
    }

    private void OnDisable()
    {
        GlobalContext.ZoneSystem.ZoneLevelChanged -= SetLevel;
        GlobalContext.ZoneSystem.ZoneProgressRestored -= OnProgressRestored;
        _zone.KillCount -= OnKills;

    }

    private void SetLevel(int value)
    {
        _levelView.SetText($"Level: {value}");
    }

    private void OnProgressRestored(int level, int countKills, bool isComplete)
    {
        _progressKill.SetText($"Kill: {countKills}/10");
    }
}
