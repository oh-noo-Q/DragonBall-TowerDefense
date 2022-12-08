using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledExistObject : BaseBehaviour
{
    public ExistObjectPool pool;
    public T GetPooledInstance<T>() where T:PooledExistObject
    {
        return (T)pool.GetObject(this);
    }

    public void ReturnToPoolAndRemoveFromActiveObject()
    {
        if (pool) pool.AddObject(this);
    }
}
