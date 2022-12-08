using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peanut : Item
{
    public override void InteractWithPlayer()
    {
        base.InteractWithPlayer();
        UserData.NumberPeanut++;
        Debug.Log($"Peanut: {UserData.NumberPeanut}");
        EventDispatcher.Instance.PostEvent(EventID.OnUpdatePeanut);
    }
}
