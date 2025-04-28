using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Item Data", menuName = "Scriptable Object/Weapon Item Data", order = int.MaxValue)]
public class WeaponItemTag : ItemTag
{
    [SerializeField] float range;
    [SerializeField] private float damage;
    [SerializeField] private float attackSpeed;
    public float Range { get { return range; } }

    public float Damage { get { return damage; } }
    public float AttackSpeed { get { return attackSpeed; } }
}