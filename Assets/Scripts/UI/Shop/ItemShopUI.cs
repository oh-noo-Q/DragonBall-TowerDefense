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

    public string NameSkin => nameSkin;
    public TypeCharacter TypeCharacter => typeCharacter;

    private void Awake()
    {
        if(unlockBtn != null) InitButton(unlockBtn, OnClickUnlock);
        if (itemBtn != null) itemBtn.onClick.AddListener(OnClickChoose);
        if (equipBtn != null) InitButton(equipBtn, OnClickUse);
        if (removeBtn != null) InitButton(removeBtn, OnClickRemove);
    }

    private void OnDestroy()
    {
        if (unlockBtn != null) unlockBtn.onClick.RemoveListener(OnClickUnlock);
        if (itemBtn != null) itemBtn.onClick.RemoveListener(OnClickChoose);
        if (equipBtn != null) equipBtn.onClick.RemoveListener(OnClickUse);
        if (removeBtn != null) removeBtn.onClick.RemoveListener(OnClickRemove);
    }

    void InitButton(Button btn, UnityEngine.Events.UnityAction action)
    {
        btn.onClick.AddListener(action);
        btn.gameObject.AddComponent<ButtonAnimation>();
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

    void OnClickUse()
    {
        EventDispatcher.Instance.PostEvent(EventID.SetItemShop, this);
        ActiveItem();
    }

    public void ActiveItem()
    {
        border.SetActive(true);
        UserData.CurrentCharacter = idCharacter;
        OnClickChoose();
        removeBtn.gameObject.SetActive(true);
        equipBtn.gameObject.SetActive(false);
    }

    void OnClickUnlock()
    {
        if(UserData.CurrentCoin >= numberCoinUnlock)
        {
            unlockBtn.gameObject.SetActive(false);
            UserData.CurrentCoin -= numberCoinUnlock;
            UserData.AddValueCharacter(idCharacter, valuePoint);
            OnClickUse();
        }
        else
        {

        }
    }

    void OnClickRemove()
    {
        if (idCharacter != 0)
        {
            Unuse();
            EventDispatcher.Instance.PostEvent(EventID.SetDefaultItem);
        }
    }

    public void OnClickBuyDragonBall(int index)
    {
        if(UserData.CurrentCoin >= Constant.BALL_SHOP_COST)
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
        if (UserData.CurrentCoin >= Constant.ITEM_SHOP_COST)
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
        removeBtn.gameObject.SetActive(false);
        equipBtn.gameObject.SetActive(true);
        border.SetActive(false);
    }
} 
