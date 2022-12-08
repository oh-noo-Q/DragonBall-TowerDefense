using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBehaviour : MonoBehaviour
{
    [HideInInspector]
    public Vector2 _cacheVector2 = new Vector3();
    public Vector2 getV2(float x, float y)
    {
        _cacheVector2.x = x;
        _cacheVector2.y = y;
        return _cacheVector2;
    }
    public Vector2 getV2xy(Vector3 v3)
    {
        _cacheVector2.x = v3.x;
        _cacheVector2.y = v3.y;
        return _cacheVector2;
    }
    public Vector3 getV2(Vector2 v2)
    {

        return getV2(v2.x, v2.y);
    }
    [HideInInspector]
    public Vector3 _cacheVector3 = new Vector3();
    public Vector3 getV3(float x, float y, float z)
    {
        _cacheVector3.x = x;
        _cacheVector3.y = y;
        _cacheVector3.z = z;
        return _cacheVector3;
    }
    public Vector3 getV3fromV2(Vector2 v2)
    {

        return getV3(v2.x, v2.y, transform.position.z);
    }

    // public float GetUIFitWitdhScreen(float originalWitdh, float minWidth)
    // {
    //     if (PlayerProfile.SCALE_SCREEN_RATIO == 1) return originalWitdh;
    //     float deltaWitdh = 1080 - originalWitdh; // 1080/1920
    //     float newScreenWidth = 1920 * PlayerProfile.SCALE_SCREEN_RATIO;
    //     float res = newScreenWidth - deltaWitdh;
    //     if (res < minWidth) res = minWidth;
    //     if (res > originalWitdh) res = originalWitdh;
    //     return res;
    // }
}
