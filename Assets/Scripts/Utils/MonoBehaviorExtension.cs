using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MonoBehaviorExtension
{
    public static Coroutine CallWithDelay(this MonoBehaviour monoBehaviour, System.Action action, float delay)
    {
        return monoBehaviour.StartCoroutine(IEDoDelayAction(action, delay));
    }

    public static IEnumerator IEDoDelayAction(System.Action action, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        action?.Invoke();
    }

    public static void CallWithEndOfFrame(this MonoBehaviour monoBehaviour, System.Action action)
    {
        monoBehaviour.StartCoroutine(IEDoWaitEndOfFrame(action));
    }

    public static IEnumerator IEDoWaitEndOfFrame(System.Action action)
    {
        yield return new WaitForEndOfFrame();
        action?.Invoke();
    }

    public static void SetLayer(this Transform transform, int layer)
    {
        transform.gameObject.layer = layer;
        if (transform.childCount == 0) return;
        for (int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).SetLayer(layer);
        }
    }

    public static void SetLayer(this Transform transform, string layerName)
    {
        int layer = LayerMask.NameToLayer(layerName);
        transform.gameObject.layer = layer;
        if (transform.childCount == 0) return;
        for (int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).SetLayer(layer);
        }
    }
}

