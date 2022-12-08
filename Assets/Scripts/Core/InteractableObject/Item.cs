using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : BaseOperationObject
{
    public override void Init(Operation operation, int value)
    {
        base.Init(operation, value);
        valueText.color = Color.green;
    }

    public override void InteractWithPlayer()
    {
        base.InteractWithPlayer();
        this.PostEvent(EventID.OnPlayerInteract);
        Destroy(gameObject, 0.5f);
    }
}
