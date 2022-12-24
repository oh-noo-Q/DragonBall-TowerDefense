using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopUI : MonoBehaviour
{
    public Image iconImg;
    public Button unlockBtn, equipBtn, itemBtn, removeBtn;
    public GameObject border;
    public Text numberUnlockTxt;
    public Text pointTxt;
    [SerializeField] int numberCoinUnlock;

    int idCharacter;
    string nameSkin;
    int valuePoint;
    TypeCharacter typeCharacter;
    private void Awake()
    {
        unlockBtn.onClick.AddListener(OnClickUnlock);
        itemBtn.onClick.AddListener(OnClickChoose);
        equipBtn.onClick.AddListener(OnClickUse);
        removeBtn.onClick.AddListener(OnClickRemove);
    }

    private void OnDestroy()
    {
        unlockBtn.onClick.RemoveListener(OnClickUnlock);
        itemBtn.onClick.RemoveListener(OnClickChoose);
        equipBtn.onClick.RemoveListener(OnClickUse);
        removeBtn.onClick.RemoveListener(OnClickRemove);
    }

    public void InitItem(int id, string name, Sprite icon, bool unlocked, int numberCoin, int point, TypeCharacter type)
    {
        iconImg.sprite = icon;
        if(unlocked)
        {
            unlockBtn.gameObject.SetActive(false);
            equipBtn.gameObject.SetActive(true);
        }
        else
        {
            unlockBtn.gameObject.SetActive(true);
            equipBtn.gameObject.SetActive(false);
        }

        idCharacter = id;
        nameSkin = name;
        numberCoinUnlock = numberCoin;
        valuePoint = point;
        typeCharacter = type;

        pointTxt.text = $"{point}";
        numberUnlockTxt.text = $"{numberCoin}";
        border.SetActive(false);
        if (point == 0) pointTxt.gameObject.SetActive(false);
    }

    void OnClickChoose()
    {
        EventDispatcher.Instance.PostEvent(EventID.OnClickChangeModel, typeCharacter);
        EventDispatcher.Instance.PostEvent(EventID.OnClickSkin, nameSkin);
    }

    public void OnClickUse()
    {
        border.SetActive(true);
        UserData.CurrentCharacter = idCharacter;
        OnClickChoose();
        EventDispatcher.Instance.PostEvent(EventID.SetItemShop, this);
    }

    void OnClickUnlock()
    {
        if(UserData.CurrentCoin >= numberCoinUnlock)
        {
            UserData.CurrentCoin -= numberCoinUnlock;
            UserData.AddValueCharacter(idCharacter, valuePoint);
            OnClickUse();
        }
    }

    void OnClickRemove()
    {

    }

    public void OnClickBuyDragonBall(int index)
    {
        if(UserData.CurrentCoin > Constant.BALL_SHOP_COST)
        {
            UserData.CurrentCoin -= Constant.BALL_SHOP_COST;
            UserData.AddDragonBall(index);
        }
        else
        {

        }
    }

    public void OnClickBuyItem(int id)
    {
        if (UserData.CurrentCoin > Constant.ITEM_SHOP_COST)
        {
            UserData.CurrentCoin -= Constant.ITEM_SHOP_COST;
            switch(id)
            {
                case 1:
                    UserData.NumberPeanut++;
                    break;
                case 2:
                    UserData.NumberRada++;
                    break;
                case 3:
                    break;
            }
        }
        else
        {

        }
    }

    public void ChooseItemSupport()
    {
        border.SetActive(true);
    }

    public void Unuse()
    {
        equipBtn.gameObject.SetActive(true);
        border.SetActive(false);
    }
} 