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
        Item item = _mob.GetHoldingItem();
        WeaponItemTag weaponTag = item.GetItemTag() as WeaponItemTag;
        RaycastHit2D hit = _mob.RaycastAt(weaponTag.GetRange(), _mob.GetEnemyMask());
        AttackAnimation(_mob);
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

    public override void Use(Mob _mob)
    {
        if(timer <= 0.0f)
        {
            WeaponItemTag weaponTag = _mob.GetHoldingItem().GetItemTag() as WeaponItemTag;
            timer = 1.0f / weaponTag.attackSpeed;
            
            base.Use(_mob);
        }
    }

    public Mob InRange(Mob _mob)
    {
        WeaponItemTag weaponTag = _mob.GetHoldingItem().GetItemTag() as WeaponItemTag;
        RaycastHit2D hit = _mob.RaycastAt(weaponTag.GetRange(), _mob.GetEnemyMask());
        if (hit)
        {
            return hit.transform.GetComponent<Mob>();
        }
        return null;
    }


    private void AttackAnimation(Mob _mob)
    {
        if (animator == null) return;
        WeaponItemTag weaponTag = _mob.GetHoldingItem().GetItemTag() as WeaponItemTag;
        animator.SetTrigger("Attack");
        animator.SetFloat("AttackSpeed", weaponTag.attackSpeed);
    }
}
