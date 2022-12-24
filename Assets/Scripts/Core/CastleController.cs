using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Spine.Unity;

public class CastleController : MonoBehaviour
{
    [SerializeField] Player smallCharacter;
    [SerializeField] Player bigCharacter;
    [SerializeField] Transform spawnPos;
    public Floor waitFloor;
    public bool isActive;

    Coroutine spawnPersonCor;
    float timeToSpawnPerson = 0;

    public void JoinCastle()
    {
        isActive = true;
        spawnPersonCor = StartCoroutine(TimeSpawnPerson());
        GameManager.Instance.Player.ShowStrength(false);
    }

    IEnumerator TimeSpawnPerson()
    {
        while(timeToSpawnPerson < 20f)
        {
            timeToSpawnPerson += Time.deltaTime;
            yield return null;
        }
        timeToSpawnPerson = 0;
        CharacterDictionary characterDic = CharacterManagerDataSO.Instance.characterDic;
        int id = Random.Range(0, characterDic.Count);
        CharacterDataSO data = characterDic[(Character)id];
        Player newPerson;
        if(data.type == TypeCharacter.Small)
        {
            newPerson = Instantiate(smallCharacter, transform);
        }
        else
            newPerson = Instantiate(bigCharacter, transform);
        newPerson.ShowStrength(false);
        SetSkin(newPerson.GetComponent<SkeletonMecanim>(), data.name);
        newPerson.transform.position = spawnPos.position;
        newPerson.transform.DOMove(waitFloor.ObjectSpawnPositions[1].position, 2f).OnComplete(() =>
        {
            newPerson.transform.SetParent(waitFloor.ObjectSpawnPositions[1]);
        });
        spawnPersonCor = StartCoroutine(TimeSpawnPerson());
    }

    void SetSkin(SkeletonMecanim model, string name)
    {
        model.Skeleton.SetSkin(name);
        model.Skeleton.UpdateCache();
        model.skeleton.SetSlotsToSetupPose();
        model.skeleton.SetToSetupPose();
        model.skeleton.UpdateWorldTransform();
    }

    public void LeaveCastle()
    {
        isActive = false;
        StopCoroutine(spawnPersonCor);
        GameManager.Instance.Player.ShowStrength(false);
    }
}
