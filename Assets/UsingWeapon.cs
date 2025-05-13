using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;


public class AttackEvent : IEvent
{
    WeaponItemTag weapon;
    public WeaponItemTag Weapon => weapon;
}

public class UsingWeapon : MonoBehaviour
{
    private Mob owner;
    [SerializeField] private float timer = 0.0f;
    //[SerializeField] private bool attack;
    [SerializeField] private Transform hand;
    [SerializeField] private BlackboardBoolProperty attack;
    void Start()
    {
        owner = GetComponent<Mob>();
        attack = owner.brain.Memory.GetProperty<BlackboardBoolProperty>(new TypedID("isAttack", typeof(BlackboardBoolProperty)));
        //GetComponent<EventBus>().Subscribe<AttackEvent>(Attack);
    }

    IEnumerator CoolDown(WeaponItemTag _tag)
    {
        if (!attack.Get())
        {
            Debug.Log("Attack!");
            attack.Set(true);
            //animator.SetFloat("AttackSpeed", _tag.AttackSpeed);

            yield return new WaitForSeconds(2.0f / _tag.AttackSpeed);
            attack.Set(false);
            Debug.Log("attack ended!");
        }
    }
    
    public bool Attack()
    {
        var item = owner.GetHoldingItem();
        if (!(item.GetItemTag() is WeaponItemTag)) return false;

        if (!attack.Get()) StartCoroutine(CoolDown((WeaponItemTag)item.GetItemTag()));
        return true;
    }

    public bool TryAttack()
    {
        if (!attack.Get())
        {
            var item = owner.GetHoldingItem();
            int mask = owner.GetEnemyMask();
            if (item.GetItemTag() is WeaponItemTag)
            {
                var tag = (WeaponItemTag)item.GetItemTag();
                var obj = Physics2D.OverlapBox(hand.position, new Vector2(10.0f * tag.Range, 1.0f), 0.0f, mask);
                if (obj)
                {
//Attack(tag);
                    return true;
                }
            }
        }
        return false;
    }

    public bool IsAttack()
    {
        return attack.Get();
    }
}
