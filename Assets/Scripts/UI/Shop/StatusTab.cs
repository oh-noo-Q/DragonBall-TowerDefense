using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusTab : MonoBehaviour
{
    [SerializeField] Sprite activeImg;
    [SerializeField] Sprite deactiveImg;
    [SerializeField] Image tabBtn;
    
    public void SelectedTab()
    {
        tabBtn.sprite = activeImg;
    }

    public void UnselectTab()
    {
        transform.SetSiblingIndex(0);
        tabBtn.sprite = deactiveImg;
    }
}
