using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tower : MonoBehaviour
{
    [SerializeField] private List<Floor> floorList = new List<Floor>();
    [SerializeField] private Transform roof;

    [SerializeField] public bool isOccupied = false;
    [SerializeField] private Floor floorPrefab;

    public List<Floor> FloorList
    {
        get => floorList;
        set => floorList = value;
    }

    private void Awake()
    {
        //InitFloor();
    }

    private void OnEnable()
    {
        //SetInteractable(false);
        this.RegisterListener(EventID.OnPlayerDrop, UpdateAvailableFloor);
        this.RegisterListener(EventID.OnUpdateTower, AddFloor);
    }

    private void OnDisable()
    {
        this.RemoveListener(EventID.OnPlayerDrop, UpdateAvailableFloor);
        this.RemoveListener(EventID.OnUpdateTower, AddFloor);
    }

    private void UpdateAvailableFloor(object param)
    {
        Debug.Log("Check");
        Floor floor = (Floor)param;
        if (floor == null || isOccupied || !floor.IsEmpty() || floorList.Count <= 1) return;
        if (floorList.Contains(floor))
        {
            int floorIndex = floorList.IndexOf(floor);
            floorList.Remove(floor);
            //floor.ReturnToPool();
            Destroy(floor.gameObject);
            if (floorIndex < floorList.Count)
            {
                for (int i = floorIndex; i < floorList.Count; i++)
                {
                    Floor availableFloor = floorList[i];
                    availableFloor.transform.DOMoveY(availableFloor.transform.position.y - availableFloor.GetSpriteHeight(), 0.5f);
                }
            }
            roof.DOMoveY(roof.transform.position.y - floorList[0].GetSpriteHeight(), 0.5f);
            this.PostEvent(EventID.OnUpdateTower);

        }
    }

    public void SetInteractable(bool value)
    {
        foreach(Floor floor in floorList)
        {
            floor.SetDroppable(value);
        }
    }

    public void CheckClear()
    {
        foreach(Floor floor in floorList)
        {
            if (!floor.IsAllObjectInteracted()) return;
        }
        SetCurrentMainTower();
    }

    public void CheckEnemyClear()
    {
        foreach (Floor floor in floorList)
        {
            if (floor.IsRemainEnemy() || floor.IsRemainGirl() || floor.IsRemainItem()) return;
        }
        SetCurrentMainTower();
    }

    private void SetCurrentMainTower()
    {
        isOccupied = true;
        GameManager.Instance.CurrentMainTower = this;
    }

    private bool IsMainTower()
    {
        return this == GameManager.Instance.CurrentMainTower;
    }

    private void AddFloor(object param = null)
    {
        if (!isOccupied || !IsMainTower()) return;
        foreach (Floor floor in floorList)
        {
            floor.transform.DOMoveY(floor.transform.position.y + floor.GetSpriteHeight(), 0.5f);
        }
        roof.DOMoveY(roof.transform.position.y + floorList[0].GetSpriteHeight(), 0.5f);
        Floor floorObj = Instantiate(floorPrefab, transform);
        //Floor floorObj = floorPrefab.GetPooledInstance<Floor>();
        floorList.Add(floorObj);
        //floorObj.transform.SetParent(transform);
        floorObj.transform.localPosition = new Vector3(0f, -floorObj.GetSpriteHeight(), 0f);
        floorObj.transform.DOMoveY(floorObj.transform.position.y + floorObj.GetSpriteHeight(), 0.5f);
    }

    public void Setup(TowerData data)
    {
        SetInteractable(false);

        floorList.Clear();

        foreach(FloorData floorData in data.floors)
        {
            //Floor floor = floorPrefab.GetPooledInstance<Floor>();
            Floor floor = Instantiate(floorPrefab, transform);
            floor.transform.SetParent(transform);
            floor.Setup(floorData);
            floorList.Add(floor);
        }

        int floorNum = floorList.Count;
        for (int i = 0; i < floorNum; i++)
        {
            floorList[i].transform.localPosition = new Vector3(0f, (i) * floorList[i].GetSpriteHeight(), 0f);
            floorList[i].IsTopFloor = (i == floorNum - 1);
        }
        roof.localPosition = new Vector3(0f, floorNum * floorList[0].GetSpriteHeight(), 0f);
    }

    //[ContextMenu("Return To Pool")]
    //public override void ReturnToPool()
    //{
    //    foreach (Floor floor in floorList)
    //    {
    //        floor.ReturnToPool();
    //    }
    //    floorList.Clear();
    //    isOccupied = false;
    //    base.ReturnToPool();
    //}
}
