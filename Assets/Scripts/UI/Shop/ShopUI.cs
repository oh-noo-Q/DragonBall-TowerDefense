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

    private void Awake()
    {
        backBtn.onClick.AddListener(OnMainMenuClicked);
        EventDispatcher.Instance.RegisterListener(EventID.OnClickSkin, SetSkinModel);
        EventDispatcher.Instance.RegisterListener(EventID.OnClickChangeModel, ChangeModel);
    }

    private void OnDestroy()
    {
        EventDispatcher.Instance.RemoveListener(EventID.OnClickSkin, SetSkinModel);
        EventDispatcher.Instance.RemoveListener(EventID.OnClickChangeModel, ChangeModel);
    }

    private void OnEnable()
    {
        if(items.Count <= 0)
        {
            CharacterDictionary characterDic = CharacterManagerDataSO.Instance.characterDic;
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
                    SetSkinModel(data.name);
                }
                else
                    item.InitItem(i, data.name, data.icon, unlocked, numberCoin, point, data.type);
            }
        }

        OnClickSwitchTab(0);
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

    public void ChangeModel(object type)
    {
        if((TypeCharacter)type == TypeCharacter.Small)
        {
            smallModel.gameObject.SetActive(true);
            bigModel.gameObject.SetActive(false);
        }
        else
        {
            smallModel.gameObject.SetActive(false);
            bigModel.gameObject.SetActive(true);
        }
    }

    public void SetSkinModel(object name)
    {
        if (bigModel.gameObject.activeInHierarchy)
            SetSkin(bigModel, (string)name);

        else SetSkin(smallModel, (string)name);
    }

    void SetSkin(SkeletonMecanim model, string name)
    {
        model.Skeleton.SetSkin(name);
        model.Skeleton.UpdateCache();
        model.skeleton.SetSlotsToSetupPose();
        model.skeleton.SetToSetupPose();
        model.skeleton.UpdateWorldTransform();
    }

}
