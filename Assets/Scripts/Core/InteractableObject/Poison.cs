using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : BaseOperationObject
{
    public override void InteractWithPlayer()
    {
        base.InteractWithPlayer();
        GameManager.Instance.Player.Poison = this;
        GameManager.Instance.Player.ShowEffectText(value);
    }
}
