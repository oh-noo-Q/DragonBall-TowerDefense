using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : BaseOperationObject
{
    public override void InteractWithPlayer()
    {
        base.InteractWithPlayer();
        GameManager.Instance.Player.Weapon = this;
    }
}
