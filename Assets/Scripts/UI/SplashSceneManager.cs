using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashSceneManager : MonoBehaviour
{
    [HideInInspector]
    public AsyncOperation asyncOperation;
    float delayLoadScene = 3f;
    DateTime startDate;

    public void LoadScene(string name)
    {
        asyncOperation = SceneManager.LoadSceneAsync(name);
    }

    private void Start()
    {
#if ENV_PROD
        
#endif
        Application.targetFrameRate = 60;

        startDate = DateTime.Now;
        StartCoroutine(IEDelayLoadScene());
    }

    IEnumerator IEDelayLoadScene()
    {
        double elaspedSecond = (DateTime.Now - startDate).TotalSeconds;

        if (elaspedSecond < delayLoadScene)
        {
            yield return new WaitForSeconds((float)(delayLoadScene - elaspedSecond));
        }

        LoadScene("GameScene");
    }
}
