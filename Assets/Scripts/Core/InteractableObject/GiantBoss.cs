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
    Fize2,
    Cell1,
    Cell2,
    MabuSlim,
    MabuFat,
}
public class GiantBoss : Enemy
{
    public Vector3 playerPos;
    [SerializeField] bool special;
    public override void InteractWithPlayer()
    {
        base.InteractWithPlayer();
        valueText.gameObject.SetActive(false);
        GameManager.Instance.Player.StrengthText.gameObject.SetActive(false);
    }
    protected override void Die()
    {
        Debug.Log("sound!!");
        animator.SetBool("Die", true);
        this.PostEvent(EventID.OnWinLevel);
    }

    public override void GetHitAnim(int index)
    {
        if(special)
        {
            Die();
        }
        else 
            base.GetHitAnim(index);
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
