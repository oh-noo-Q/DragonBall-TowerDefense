using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelController : MonoBehaviour
{
    [SerializeField] public Enemy enemy;

    public  void GetOnceHit()
    {
        enemy.GetOnceHit();
    }

    public  void GetDieAnim(int index)
    {
        enemy.GetDieAnim(index);
    }

    public void EndAnim()
    {
        enemy.EndAnim();
    }

    public void EndAttack()
    {
        enemy.EndAttack();
    }

    public void ResetAttack()
    {
        enemy.ResetAttack();
    }
}
