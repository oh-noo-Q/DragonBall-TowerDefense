using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CastleController : MonoBehaviour
{
    [SerializeField] Player person;
    [SerializeField] Transform spawnPos;
    public Floor waitFloor;
    public bool isActive;

    Coroutine spawnPersonCor;
    float timeToSpawnPerson = 0;

    public void JoinCastle()
    {
        isActive = true;
        spawnPersonCor = StartCoroutine(TimeSpawnPerson());
    }

    IEnumerator TimeSpawnPerson()
    {
        while(timeToSpawnPerson < 10f)
        {
            timeToSpawnPerson += Time.deltaTime;
            yield return null;
        }
        timeToSpawnPerson = 0;
        Player newPerson = Instantiate(person, transform);
        newPerson.transform.position = spawnPos.position;
        newPerson.transform.DOMove(waitFloor.ObjectSpawnPositions[1].position, 2f).OnComplete(() =>
        {
            newPerson.transform.SetParent(waitFloor.ObjectSpawnPositions[1]);
        });
        spawnPersonCor = StartCoroutine(TimeSpawnPerson());
    }

    public void LeaveCastle()
    {
        isActive = false;
        StopCoroutine(spawnPersonCor);
    }
}
