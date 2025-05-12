using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class UsingWeapon : MonoBehaviour
{
    private Animator animator;
    private Mob owner;
    [SerializeField] private float timer = 0.0f;
    [SerializeField] private bool attack;
    [SerializeField] private Transform hand;
    void Start()
    {
        animator = GetComponent<Animator>();
        owner = GetComponent<Mob>();
    }

    IEnumerator CoolDown(WeaponItemTag _tag)
    {
        if (!attack)
        {
            Debug.Log("Attack!");
            attack = true;

            animator.SetTrigger("Attack");
            animator.SetFloat("AttackSpeed", _tag.AttackSpeed);

            yield return new WaitForSeconds(2.0f / _tag.AttackSpeed);
            attack = false;
            Debug.Log("attack ended!");
        }
    }
    
    public void Attack(WeaponItemTag _tag)
    {
        if (!attack) StartCoroutine(CoolDown(_tag));
    }

    public bool TryAttack()
    {
        if(!attack)
        {
            var item = owner.GetHoldingItem();
            int mask = owner.GetEnemyMask();
            if (item.GetItemTag() is WeaponItemTag)
            {
                var tag = (WeaponItemTag)item.GetItemTag();
                var obj = Physics2D.OverlapBox(hand.position, new Vector2(10.0f * tag.Range, 1.0f), 0.0f, mask);
                if (obj)
                {
                    Attack(tag);
                    return true;
                }
            }
        }
        return false;
    }

    public bool IsAttack()
    {
        return attack;
    }
}
