using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girl : BaseOperationObject
{
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public override void Init(Operation operation, int value)
    {
        
    }

    public override void InteractWithPlayer()
    {
        base.InteractWithPlayer();
        UserData.CurrentCoin += 10;
        GameManager.Instance.Player.CollectCoin();
        GameManager.Instance.Player.Slap();
        Dance();
        Destroy(gameObject, 1f);
    }

    void Dance()
    {
        int danceRan = Random.Range(0, 3) + 1;
        animator.SetInteger("Dance", danceRan);
    }
}
