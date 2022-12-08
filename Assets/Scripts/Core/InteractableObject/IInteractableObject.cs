using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IInteractableObject : MonoBehaviour
{
    public abstract void InteractWithPlayer();

    public virtual void Init(Operation operation, int value)
    {

    }
}
