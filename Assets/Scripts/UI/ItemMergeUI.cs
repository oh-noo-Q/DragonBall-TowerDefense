using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemMergeUI : MonoBehaviour
{
    [SerializeField] Button mergeBtn;
    [SerializeField] Text pointTxt;
    [SerializeField] Image iconImg;

    int valuePoint;

    public void InitItem(Sprite icon, int point)
    {
        iconImg.sprite = icon;
        valuePoint = point;
        pointTxt.text = $"x2 + {point}";
    }

    public void AddEventButton(UnityAction action)
    {
        mergeBtn.onClick.AddListener(action);
    }

}
