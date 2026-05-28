using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(EnemyView))]
[DefaultExecutionOrder(-10)]
public class Enemy : MonoBehaviour, IDamageable
{
    private const float DefaultHealth = 10;

    [SerializeField] private int _level;
    [SerializeField] private float _health;

    public event Action<float> HealthChanged;
    public event Action<int> LevelUpdate;
    public event Action UnitDied;

    public int Level => _level;
    public float Health => _health;

    private void Awake()
    {
        GlobalContext.Enemy = this;
        LevelUpdate?.Invoke(_level);
    }

    private void OnEnable()
    {
        GlobalContext.ZoneSystem.ZoneLevelChanged += SetLevel;
    }

    private void OnDisable()
    {
        GlobalContext.ZoneSystem.ZoneLevelChanged -= SetLevel;
    }

    private void SetLevel(int level)
    {
        _level = level;
        LevelUpdate?.Invoke(_level);
    }

    private void OnValidate()
    {
        // Тестовая штука
        _level = Mathf.Max(1, _level);
        LevelUpdate?.Invoke(_level);
    }

    private void OnDestroy()
    {
        GlobalContext.Enemy = null;
    }

    public void TakeDamage(float damage)
    {
        _health = Mathf.Max(0, _health - damage);
        HealthChanged?.Invoke(_health);

        if (_health <= 0)
            Die();
    }

    private void Die()
    {
        UnitDied?.Invoke();
        _health = DefaultHealth * _level;
        HealthChanged?.Invoke(_health);
    }

}