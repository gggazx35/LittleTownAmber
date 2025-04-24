using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandedDagger : HandedItem, IDetectRange
{
    private Animator animator;
    private Player p;
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
        else
        {
        }
    }

    protected override void Perform(Mob _mob)
    {
        Item item = _mob.GetHoldingItem();
        WeaponItemTag weaponTag = item.GetItemTag() as WeaponItemTag;
        RaycastHit2D hit = _mob.RaycastAt(weaponTag.Range, _mob.GetEnemyMask());
        AttackAnimation(_mob);
        if (hit)
        {
            Mob other = hit.transform.gameObject.GetComponent<Mob>();
            if (other)
            {
                //EventBus.get().Publish(_mob.gameObject, new MobUseDaggerEvent(item, _mob,));
                other.TakeDamage(_mob, weaponTag.Damage);
                
                Debug.Log("Att");
            }
        }
    }

    public override void Use(Mob _mob)
    {
        if(timer <= 0.0f)
        {
            Player p = _mob.GetComponent<Player>();

            WeaponItemTag weaponTag = _mob.GetHoldingItem().GetItemTag() as WeaponItemTag;
            timer = 1.0f / weaponTag.AttackSpeed;
            
            base.Use(_mob);
        }
    }

    public Mob InRange(Mob _mob)
    {
        WeaponItemTag weaponTag = _mob.GetHoldingItem().GetItemTag() as WeaponItemTag;
        RaycastHit2D hit = _mob.RaycastAt(weaponTag.Range, _mob.GetEnemyMask());
        if (hit)
        {
            return hit.transform.GetComponent<Mob>();
        }
        return null;
    }


    private void AttackAnimation(Mob _mob)
    {
        WeaponItemTag weaponTag = _mob.GetHoldingItem().GetItemTag() as WeaponItemTag;
        if (animator == null)
        {
            p = _mob.GetComponent<Player>();
            if (p != null)
            {
                p.AttackAnim(weaponTag.AttackSpeed);
            }
            return;
        }
        animator.SetTrigger("Attack");
        animator.SetFloat("AttackSpeed", weaponTag.AttackSpeed);
    }
}
