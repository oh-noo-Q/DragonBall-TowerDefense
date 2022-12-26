using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public enum EnemyType
{
    DEFAULT,
    Creep,
    Creep1,
    Creep2,
    Drum,
    Tambourine,
    Radit,
    Nappu,
    St06,
    St05,
    St04,
    St03,
    St02,
    St01,
    Shisami,
    N16,
    N17,
    N18,
    N19,
    Girl,
    DrGero,
    Dabra,
    Puipui,
    Yakon,
    Babidi
}
public class Enemy : BaseOperationObject
{
    [SerializeField] public GameObject model;
    protected Animator animator;
    [SerializeField] protected int AmountAttackType = 1;

    int currentAttack;
    bool isWin;
    bool attacked;
    protected bool attackable;
    protected Player player;

    public bool IsWin => isWin;

    protected virtual void Awake()
    {
        animator = model.GetComponent<Animator>();
    }

    public override void Init(Operation operation, int value)
    {
        float scaleRan = Random.Range(0.95f, 1.1f);
        model.transform.localScale = model.transform.localScale * scaleRan;
        this.operation = Operation.ADD;
        this.value = value;
        UpdateValueText();
    }

    protected override void UpdateValueText()
    {
        valueText.text = value.ToString();
    }

    public override void InteractWithPlayer()
    {
        player = GameManager.Instance.Player;
        if (value >= GameManager.Instance.Player.Strength)
        {
            Attack();
            player.GetHit();
            isWin = true;
            return;
        }
        else
        {
            isWin = false;
            RandomStateReact();
            if(attackable) 
            {
                Attack();
                player.Block();
            }
            else
            {
                player.Attack();
            }
        }
        base.InteractWithPlayer();
    }

    protected virtual void Attack()
    {
        currentAttack = Random.Range(0, AmountAttackType) + 1;
        animator.SetInteger("Attack", currentAttack);
    }

    protected virtual void Die()
    {
        this.PostEvent(EventID.OnPlayerInteract);
        Destroy(gameObject);
    }

    public virtual void GetHitAnim(int index)
    {
        animator.SetInteger("GetHit", index);
    }

    public virtual void GetOnceHit()
    {

    }

    public virtual void GetDieAnim(int index)
    {

    }

    public void EndAnim()
    {
        Die();
    }

    public void EndAttack()
    {
        animator.SetInteger("Attack", 0);
        if (isWin)
        {
            player.EndGetHit();
            player.Die();
        }
        else if (!attacked)
        {
            attacked = true;
            GameManager.Instance.Player.Attack();
        }
    }

    protected virtual void RandomStateReact()
    {
        float ranNum = Random.Range(0, 2f);
        attackable = (ranNum > 1) ? true : false;
    }

    public void ResetAttack()
    {
        animator.SetInteger("Attack", 0);
    }

    //sound
    public void Punch01()
    {
        SoundManager.instance.PlaySingle(SoundType.Punch);
    }
    public void Punch02()
    {
        SoundManager.instance.PlaySingle(SoundType.Punch02);
    }

    public void Punch03()
    {
        SoundManager.instance.PlaySingle(SoundType.Punch03);
    }

    public void Punch04()
    {
        SoundManager.instance.PlaySingle(SoundType.Punch04);
    }

    public void CritPunch()
    {
        SoundManager.instance.PlaySingle(SoundType.CritPunch);
    }

    public void CritDap()
    {
        SoundManager.instance.PlaySingle(SoundType.CritDap);
    }
}
