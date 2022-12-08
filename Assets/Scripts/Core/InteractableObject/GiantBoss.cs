using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossType
{
    Default,
    Dragon,
    Picolo,
    Cadic,
    KingKong,
    Fize1,
}
public class GiantBoss : Enemy
{
    public Vector3 playerPos;
    public override void InteractWithPlayer()
    {
        base.InteractWithPlayer();
        valueText.gameObject.SetActive(false);
        GameManager.Instance.Player.StrengthText.gameObject.SetActive(false);
    }
    protected override void Die()
    {
        animator.SetBool("Die", true);
        this.PostEvent(EventID.OnWinLevel);
    }

    public override void GetHitAnim(int index)
    {
        Die();
    }

    protected override void Attack()
    {
        base.Attack();
    }

    protected override void RandomStateReact()
    {
        attackable = true;
    }
}
