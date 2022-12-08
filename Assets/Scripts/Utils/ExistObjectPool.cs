using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExistObjectPool : BaseBehaviour
{
    private Queue<PooledExistObject> availableObjects = new Queue<PooledExistObject>();

    public List<PooledExistObject> activeObjects = new List<PooledExistObject>();

    //public void InitPool<T>(T poolSamle,int count) where T: PooledExistObject
    //{
    //    for (int i = 0; i < count; i++)
    //    {
    //        PooledExistObject obj = Instantiate<PooledExistObject>(poolSamle);
    //        obj.transform.SetParent(transform, false);
    //        obj.pool = this;
    //        AddObject(obj);
    //    }
    //}

    public void ResetPool()
    {
        for (int i = 0; i < activeObjects.Count; i++)
        {
            activeObjects[i].ReturnToPoolAndRemoveFromActiveObject();
            i--;
        }
        activeObjects.Clear();
        
    }

    public void AddObject<T>(T obj) where T: PooledExistObject
    {
        obj.gameObject.SetActive(false);
        availableObjects.Enqueue(obj);
        activeObjects.Remove(obj);
    }
    public T GetObject<T>(T poolSamle) where T:PooledExistObject
    {
        PooledExistObject obj;
        if (availableObjects.Count > 0)
        {
            obj = availableObjects.Dequeue();
            if (obj.isActiveAndEnabled) obj = Instantiate(poolSamle, transform);
            obj.gameObject.SetActive(true);
        }
        else 
        { 
            obj = Instantiate(poolSamle, transform);
        }
        obj.pool = this;

        activeObjects.Add(obj);

        return (T)obj;
    }

}
