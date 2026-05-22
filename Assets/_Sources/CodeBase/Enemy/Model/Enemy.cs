using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int _level;
    [SerializeField] private float _health;

    public int Level => _level;
    public float Health => _health;

    public void TakeDamage(float damage)
    {
        _health = Mathf.Max(0, _health - damage);
    }
}
