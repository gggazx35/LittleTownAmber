using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] protected float health;
    public float maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    public void TakeDamage(float _amount)
    {
        health -= _amount;
    }

    /*public void TakeDamage(Mob _cause, float _amount)
    {
        health = Mathf.Clamp(health - _cause.CalcuateDamage(_amount), 0.0f, maxHealth);
        m_recentDamageCause = _cause;
        if (m_health <= 0.0f)
        {
            EventBus.get().Publish(new MobDeathEvent(this, m_recentDamageCause, DamageReason.None));
        }
    }

    public void TakeDamage(DamageReason _reason, float _amount)
    {
        health = Mathf.Clamp(health - _amount, 0.0f, maxHealth);
        if (health <= 0.0f)
        {
            EventBus.get().Publish(new MobDeathEvent(this, m_recentDamageCause, _reason));
        }
        m_recentDamageCause = null;
    }*/
}
