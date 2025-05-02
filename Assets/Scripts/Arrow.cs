using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] float damage = 5.0f;
    WeaponItemTag weapon;

    public void Shoot(WeaponItemTag _weapon)
    {
        weapon = _weapon;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    { 
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        collision.gameObject.GetComponent<Health>().TakeDamage(damage + weapon.Damage);
    }
}
