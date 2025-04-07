using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandedDagger : HandedItem, IDetectRange
{
    private Animator animator;
    [SerializeField] private float timer = 0.0f;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (timer > 0.0f)
        {
            timer -= Time.deltaTime;
        }
    }

    protected override void Perform(Mob _mob)
    {
        WeaponItemTag weaponTag = item.GetItemTag() as WeaponItemTag;
        RaycastHit2D hit = _mob.RaycastAt(weaponTag.GetRange(), _mob.GetEnemyMask());
        AttackAnimation();
        if (hit)
        {
            Mob other = hit.transform.gameObject.GetComponent<Mob>();
            if (other)
            {
                other.TakeDamage(_mob, weaponTag.GetDamage());
                
                Debug.Log("Att");
            }
        }
    }

    public override void Use(Mob _mob, int _slot)
    {
        if(timer <= 0.0f)
        {
            WeaponItemTag weaponTag = item.GetItemTag() as WeaponItemTag;
            timer = 1.0f / weaponTag.attackSpeed;
            
            base.Use(_mob, _slot);
        }
    }

    public Mob InRange(Mob _mob, int _slot)
    {
        WeaponItemTag weaponTag = item.GetItemTag() as WeaponItemTag;
        RaycastHit2D hit = _mob.RaycastAt(weaponTag.GetRange(), _mob.GetEnemyMask());
        if (hit)
        {
            return hit.transform.GetComponent<Mob>();
        }
        return null;
    }


    private void AttackAnimation()
    {
        if (animator == null) return;
        WeaponItemTag weaponTag = item.GetItemTag() as WeaponItemTag;
        animator.SetTrigger("Attack");
        animator.SetFloat("AttackSpeed", weaponTag.attackSpeed);
    }
}
