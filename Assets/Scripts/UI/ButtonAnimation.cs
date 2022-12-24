using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        transform.DOScale(0.8f, 0.2f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.DOScale(1.0f, 0.2f);
    }
}