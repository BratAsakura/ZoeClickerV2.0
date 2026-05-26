using TMPro;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelUI;
    [SerializeField] private TextMeshProUGUI _healthUI;

    private Enemy enemy;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        _healthUI.SetText($"Health: {enemy.Health}");
        _levelUI.SetText($"Level: {enemy.Level}");
    }

    private void OnEnable()
    {
        enemy.HealthChanged += OnHealthView;
        enemy.LevelUpdate += OnLevelView;
    }

    private void OnDestroy()
    {
        enemy.HealthChanged -= OnHealthView;
        enemy.LevelUpdate -= OnLevelView;
    }

    private void OnLevelView(int value)
    {
        _levelUI.SetText($"Level: {value}");
    }

    private void OnHealthView(float value)
    {
        _healthUI.SetText($"Health: {value}");
    }
}
