using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : BaseBehaviour
{

    [HideInInspector]
    public ObjectPool pool;

    [System.NonSerialized]
    ObjectPool poolInstanceForPrefab;

    public void InitPool<T>(int count) where T : PooledObject
    {
        poolInstanceForPrefab = ObjectPool.InitPool(this, count);
    }

    public T GetPooledInstance<T>() where T : PooledObject
    {
        if (!poolInstanceForPrefab)
            poolInstanceForPrefab = ObjectPool.GetPool(this);
        return (T)poolInstanceForPrefab.GetObject();
    }

    public T GetPooledInstance<T>(Vector3 position)
      where T : PooledObject
    {
        if (!poolInstanceForPrefab)
            poolInstanceForPrefab = ObjectPool.GetPool(this);
        var obj = poolInstanceForPrefab.GetObject();
        obj.transform.position = position;
        return (T)obj;
    }

    public T GetPooledInstance<T>(Vector3 position,
      Quaternion rotation,
      bool enable = true) where T : PooledObject
    {
        if (!poolInstanceForPrefab)
            poolInstanceForPrefab = ObjectPool.GetPool(this);
        var obj = poolInstanceForPrefab.GetObject();
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.gameObject.SetActive(enable);
        return (T)obj;
    }

    public virtual void ReturnToPool() { if (pool) pool.AddObject(this); }

    
}
