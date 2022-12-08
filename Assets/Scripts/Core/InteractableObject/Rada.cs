using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rada : Item
{
    public override void InteractWithPlayer()
    {
        base.InteractWithPlayer();
        UserData.NumberRada++;
        EventDispatcher.Instance.PostEvent(EventID.OnUpdateRada);
    }
}
