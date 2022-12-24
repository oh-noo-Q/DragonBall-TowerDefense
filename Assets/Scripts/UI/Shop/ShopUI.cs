using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] Button backBtn;
    [SerializeField] List<StatusTab> tabBtn;
    [SerializeField] List<GameObject> panelItemShop;
    [SerializeField] Transform parentItem;
    [SerializeField] ItemShopUI itemPrefab;
    [SerializeField] SkeletonMecanim bigModel;
    [SerializeField] SkeletonMecanim smallModel;

    List<ItemShopUI> items = new List<ItemShopUI>();
    ItemShopUI currentItem;

    private void Awake()
    {
        backBtn.onClick.AddListener(OnMainMenuClicked);
        //EventDispatcher.Instance.RegisterListener(EventID.OnClickSkin, SetSkinModel);
        //EventDispatcher.Instance.RegisterListener(EventID.OnClickChangeModel, ChangeModel);
        EventDispatcher.Instance.RegisterListener(EventID.SetItemShop, SetItemShop);
        EventDispatcher.Instance.RegisterListener(EventID.SetDefaultItem, SetDefaultItem);
    }

    private void OnDestroy()
    {
        //EventDispatcher.Instance.RemoveListener(EventID.OnClickSkin, SetSkinModel);
        //EventDispatcher.Instance.RemoveListener(EventID.OnClickChangeModel, ChangeModel);
        EventDispatcher.Instance.RemoveListener(EventID.SetItemShop, SetItemShop);
        EventDispatcher.Instance.RemoveListener(EventID.SetDefaultItem, SetDefaultItem);
    }

    private void OnEnable()
    {
        CharacterDictionary characterDic = CharacterManagerDataSO.Instance.characterDic;
        if(items.Count <= 0)
        {
            for (int i = 0; i < characterDic.Count; i++)
            {
                CharacterDataSO data = characterDic[(Character)i];
                ItemShopUI item = Instantiate(itemPrefab, parentItem);
                items.Add(item);
                bool unlocked = (UserData.CharacterMerge[i] > 0) ? true : false;
                int numberCoin = 0; 
                int point = 0;
                switch(data.rank)
                {
                    case RankCharacter.Silver:
                        numberCoin = 500;
                        point = 5;
                        break;
                    case RankCharacter.Golden:
                        numberCoin = 800;
                        point = 10;
                        break;
                    case RankCharacter.Super:
                        numberCoin = 1000;
                        point = 20;
                        break;
                }
                if (i == 0)
                {
                    item.InitItem(i, data.name, data.icon, true, 0, 0, data.type);
                }
                else
                    item.InitItem(i, data.name, data.icon, unlocked, numberCoin, point, data.type);
            }
        }

        OnClickSwitchTab(0);
        currentItem = items[UserData.CurrentCharacter];
        currentItem.ActiveItem();
    }

    public void OnClickSwitchTab(int i)
    {
        ClearShop(i);
        tabBtn[i].SelectedTab();
        panelItemShop[i].SetActive(true);
    }

    void ClearShop(int except)
    {
        for (int i = 0; i < tabBtn.Count; i++)
        {
            if (i != except)
            {
                tabBtn[i].UnselectTab();
                panelItemShop[i].SetActive(false);
            }
        }
    }
    private void OnMainMenuClicked()
    {
        UIManager.Instance.Loading(() =>
        {
            Hide();
            GameManager.Instance.ReturnMainMenu();
            UIManager.Instance.ShowMainMenuUI();
        });
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    void SetItemShop(object obj)
    {
        currentItem.Unuse();
        currentItem = (ItemShopUI)obj;
    }

    void SetDefaultItem(object obj)
    {
        currentItem.Unuse();
        currentItem = items[0];
        items[0].ActiveItem();
    }
}
