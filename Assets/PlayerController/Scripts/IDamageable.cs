using UnityEngine;

public interface IDamageable
{
    void ApplyDamage(float damage, MonoBehaviour causer);
}
