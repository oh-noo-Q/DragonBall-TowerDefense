using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earring : Item
{
    public override void InteractWithPlayer()
    {
        base.InteractWithPlayer();
        if(UserData.CheckHaveCharacterMerge())
            EventDispatcher.Instance.PostEvent(EventID.OnShowMerge);
        Debug.Log("Earring");
    }

    public override void Init(Operation operation, int value)
    {
        base.Init(operation, value);

    }
}
