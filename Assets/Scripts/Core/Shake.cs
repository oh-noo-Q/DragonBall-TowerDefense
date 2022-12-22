using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    [SerializeField] float duration = 1f;
    [SerializeField] AnimationCurve curve;
    [SerializeField] bool test = false;
    private void Awake()
    {
        EventDispatcher.Instance.RegisterListener(EventID.Shaking, StartShaking);    
    }

    // Update is called once per frame
    void Update()
    {
        if (test)
        {
            test = false;
            StartCoroutine(Shaking());
        }
    }

    IEnumerator Shaking()
    {
        Vector3 starPos = transform.position;
        float elapsedTime = 0f;

        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = starPos + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = starPos;
    }

    void StartShaking(object start)
    {
        StartCoroutine(Shaking());
    }
}
