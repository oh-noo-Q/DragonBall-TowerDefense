using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MergePopupController : MonoBehaviour
{
    [SerializeField] GameplayUI gameplayUI;
    [SerializeField] Transform parentItem;
    [SerializeField] ItemMergeUI itemPrf;


    private void OnEnable()
    {
        for(int i = 0; i < UserData.CharacterMerge.Count; i++)
        {
            int point = UserData.CharacterMerge[i];
            if (point > 0)
            {
                ItemMergeUI item = Instantiate(itemPrf, parentItem);
                item.InitItem(CharacterManagerDataSO.Instance.characterDic[(Character)i].icon, point);
                item.AddEventButton(() => { OnClickMerge(point); });
            }
        }
    }

    void OnClickMerge(int point)
    {
        gameplayUI.OnClickMerge(point);
    }
}
