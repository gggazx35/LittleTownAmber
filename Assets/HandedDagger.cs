using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandedDagger : HandedItem
{
    private Animator animator;
    private float timer = 0.0f;
    
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
        if (hit)
        {
            Mob other = hit.transform.gameObject.GetComponent<Mob>();
            if (other)
            {
                other.TakeDamage(_mob, weaponTag.GetDamage());
                AttackAnimation();
                Debug.Log("Att");
            }
        }
    }

    public override void Use(Mob _mob, int _slot)
    {
        if(timer <= 0.0f)
        {
            WeaponItemTag weaponTag = item.GetItemTag() as WeaponItemTag;
            timer = weaponTag.attackSpeed;
            
            base.Use(_mob, _slot);
        }
    }

    public override Mob InRange(Mob _mob, int _slot)
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
        animator.SetTrigger("Attack");
    }

    IEnumerator Attack(float _sec)
    {
        yield return new WaitForSeconds(_sec);
    }
}
