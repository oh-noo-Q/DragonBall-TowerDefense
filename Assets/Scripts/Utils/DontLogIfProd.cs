using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontLogIfProd : MonoBehaviour
{

    void Awake()
    {
#if ENV_LOG || !ENV_PROD || UNITY_EDITOR
        Debug.unityLogger.logEnabled = true;
#else
        Debug.unityLogger.logEnabled = false;
#endif
    }

}
