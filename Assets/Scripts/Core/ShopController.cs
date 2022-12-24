using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class ShopController : MonoBehaviour
{
    [SerializeField] Transform modelPosition;
    [SerializeField] Player bigModel;
    [SerializeField] Player smallModel;

    Player mainPlayer;
    private void Awake()
    {
        CharacterDictionary characterDic = CharacterManagerDataSO.Instance.characterDic;
        CharacterDataSO data = characterDic[(Character)UserData.CurrentCharacter];
        ChangeModel(data.type);
        SetSkinModel(data.name);

        EventDispatcher.Instance.RegisterListener(EventID.OnClickSkin, SetSkinModel);
        EventDispatcher.Instance.RegisterListener(EventID.OnClickChangeModel, ChangeModel);
    }

    private void OnDestroy()
    {
        EventDispatcher.Instance.RemoveListener(EventID.OnClickSkin, SetSkinModel);
        EventDispatcher.Instance.RemoveListener(EventID.OnClickChangeModel, ChangeModel);
    }
    public void JoinShop()
    {
        bigModel.ShowStrength(false);
        smallModel.ShowStrength(false);
    }

    void SelectMainSkin()
    {
        Destroy(GameManager.Instance.Player.gameObject);
        Player newPlayer = Instantiate(mainPlayer);
        GameManager.Instance.SetMainPlayer(newPlayer);
    }

    void SetMainSkin(object obj)
    {
        mainPlayer = (Player)obj;
    }

    public void ChangeModel(object type)
    {
        if ((TypeCharacter)type == TypeCharacter.Small)
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
            SetSkin(bigModel.GetComponent<SkeletonMecanim>(), (string)name);

        else SetSkin(smallModel.GetComponent<SkeletonMecanim>(), (string)name);
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
