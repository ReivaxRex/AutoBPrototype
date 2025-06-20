using UnityEngine;

public interface IDamageable<T>
{
    void Damage(T damageAmount);
    bool IsAlive();
    void Die();

}
