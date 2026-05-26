using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(EnemyView))]
public class Enemy : MonoBehaviour, IDamageable
{
    private const float DeafaultHealth = 10;

    [SerializeField] private int _level;
    [SerializeField] private float _health;

    public event Action<float> HealthChanged;
    public event Action<int> LevelUpdate;

    public int Level => _level;
    public float Health => _health;

    private void Awake()
    {
        LevelUpdate?.Invoke(_level);
    }

    private void OnValidate()
    {
        // Тестовая штука
        _level = Mathf.Max(1, _level);
        LevelUpdate?.Invoke(_level);
    }

    public void TakeDamage(float damage)
    {
        _health = Mathf.Max(0, _health - damage);
        HealthChanged?.Invoke(_health);
    }
}
