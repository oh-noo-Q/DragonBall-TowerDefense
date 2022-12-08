using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBall : Item
{
    public int indexBall;
    public override void InteractWithPlayer()
    {
        base.InteractWithPlayer();
        UserData.AddDragonBall(indexBall);
        
    }
}
