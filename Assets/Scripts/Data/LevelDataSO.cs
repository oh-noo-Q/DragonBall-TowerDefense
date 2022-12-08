using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName ="Data/LevelData")]
public class LevelDataSO : ScriptableObject
{
    public int playerStrength;
    public List<TowerData> Towers = new List<TowerData>();
    public int giantBossID = -1;
    public int giantBossStrength = 100;

    public void OnValidate()
    {
        if (Towers.Count < 1)
        {
            Towers.Add(new TowerData());
        }
        for (int towerIdx = 0; towerIdx < Towers.Count; towerIdx++)
        {
            TowerData tower = Towers[towerIdx];
            tower.name = "Tower " + (towerIdx + 1).ToString(); 
            if (tower.floors.Count < 1)
            {
                tower.floors.Add(new FloorData());
            }
            List<FloorData> floors = tower.floors;
            for (int floorIdx = 0; floorIdx < floors.Count; floorIdx++)
            {
                FloorData floor = floors[floorIdx];
                floor.name = "Floor " + (floorIdx + 1).ToString();
                if (floor.interactableObjects.Count > 3)
                {
                    floor.interactableObjects.RemoveAt(floor.interactableObjects.Count - 1);
                }
                foreach (InteractableObjData obj in floor.interactableObjects)
                {
                    obj.typeStr = Enum.GetName(typeof(ObjectType), obj.type);
                }
            }
        }
    }
}

[Serializable]
public class TowerData
{
    [HideInInspector] public string name;
    public List<FloorData> floors = new List<FloorData>();
}

[Serializable]
public class FloorData
{
    [HideInInspector] public string name;
    public KeyType gate = KeyType.NULL;
    public List<InteractableObjData> interactableObjects = new List<InteractableObjData>();
}

[Serializable]
public class InteractableObjData
{
    [HideInInspector] public string typeStr;
    public ObjectType type;
    public int ID;
    public Operation operation;
    public int value;
    public bool hideStrength;
}
