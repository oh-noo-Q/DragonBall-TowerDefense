using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Princess : IInteractableObject
{
    private void OnEnable()
    {
        
    }

    public override void InteractWithPlayer()
    {
        Debug.Log("Princess saved.");
    }

    public void Die()
    {
        Debug.Log("Bruh");
        this.PostEvent(EventID.OnLoseLevel);
    }
}
