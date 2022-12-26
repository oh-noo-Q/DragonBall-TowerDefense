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

    public void Punch01()
    {
        enemy.Punch01();
    }
    public void Punch02()
    {
        enemy.Punch02();
    }

    public void Punch03()
    {
        enemy.Punch03();
    }

    public void Punch04()
    {
        enemy.Punch04();
    }

    public void CritPunch()
    {
        enemy.CritPunch();
    }

    public void CritDap()
    {
        enemy.CritDap();
    }
}
