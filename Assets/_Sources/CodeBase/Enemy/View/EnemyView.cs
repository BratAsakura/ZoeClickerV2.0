using TMPro;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelUI;
    [SerializeField] private TextMeshProUGUI _healthUI;

    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _healthUI.SetText($"Health: {_enemy.Health}");
        _levelUI.SetText($"Level: {_enemy.Level}");
    }

    private void OnEnable()
    {
        _enemy.HealthChanged += OnHealthView;
        _enemy.LevelUpdate += OnLevelView;
    }

    private void OnDisable()
    {
        _enemy.HealthChanged -= OnHealthView;
        _enemy.LevelUpdate -= OnLevelView;
    }

    private void OnLevelView(int value)
    {
        _levelUI.SetText($"Level: {value}");
    }

    private void OnHealthView(float value)
    {
        _healthUI.SetText("Health: " + NumberFormatter.Format(value));
    }
}
