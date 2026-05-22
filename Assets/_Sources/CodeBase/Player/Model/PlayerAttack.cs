using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float _damage;

    public void Attack(IDamageable target)
    {
        target.TakeDamage(_damage);
    }
}