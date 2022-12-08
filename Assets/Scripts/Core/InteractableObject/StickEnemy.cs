using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StickType
{
    Normal = 0,
    Gun = 1,
    Hammer = 2
}
public class StickEnemy : Enemy
{
    public StickType type;

    protected override void Awake()
    {
        base.Awake();
        animator.SetLayerWeight((int)type, 1);
    }

    public override void GetHitAnim(int index)
    {
    }

    public override void GetOnceHit()
    {
        base.GetOnceHit();
        animator.SetTrigger("GetHit");
    }

    public override void GetDieAnim(int index)
    {
        base.GetDieAnim(index);
        animator.SetInteger("Die", index);
    }

}
