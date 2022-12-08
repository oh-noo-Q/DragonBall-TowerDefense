using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : IInteractableObject
{
    [SerializeField] KeyType keyType;

    public KeyType KeyType => keyType;

    private void OnEnable()
    {

    }

    public override void InteractWithPlayer()
    {
        
    }
}
