using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : BaseOperationObject
{
    Animator animator;

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public override void InteractWithPlayer()
    {
        base.InteractWithPlayer();
        this.PostEvent(EventID.OnPlayerInteract);
        Attack();
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
    }
}
