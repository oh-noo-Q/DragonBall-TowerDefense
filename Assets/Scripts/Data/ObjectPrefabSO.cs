using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;
using System;

[Serializable]
public class EnemyIDDict : SerializableDictionaryBase<EnemyType, BaseOperationObject> { }

[Serializable]
public class TrapIDDict : SerializableDictionaryBase<TrapType, BaseOperationObject> { }

[Serializable]
public class ItemIDDict : SerializableDictionaryBase<ItemType, BaseOperationObject> { }

[Serializable]
public class DragonBallIDDict : SerializableDictionaryBase<DragonBallType, BaseOperationObject> { }

[Serializable]
public class WeaponIDDict : SerializableDictionaryBase<WeaponType, Weapon> { }

[Serializable]
public class KeyIDDict : SerializableDictionaryBase<KeyType, Key> { }


[CreateAssetMenu(fileName = "ObjectPrefabSO", menuName = "Data/ObjectPrefabSO")]
public class ObjectPrefabSO : SingletonScriptableObject<ObjectPrefabSO>
{
    public Tower towerPrefab;
    public Floor floorPrefab;

    public EnemyIDDict enemies = new EnemyIDDict();
    public TrapIDDict traps = new TrapIDDict();
    public ItemIDDict items = new ItemIDDict();
    public DragonBallIDDict dragonBalls = new DragonBallIDDict();
    public WeaponIDDict weapons = new WeaponIDDict();
    public List<BaseOperationObject> shields = new List<BaseOperationObject>();
    public KeyIDDict keys = new KeyIDDict();

    public IInteractableObject GetObject(ObjectType type, int idx)
    {
        IInteractableObject obj;
        switch (type)
        {
            case ObjectType.ENEMY:
                obj = enemies[(EnemyType) idx];
                break;
            case ObjectType.SHIELD:
                obj = shields[idx];
                break;
            case ObjectType.TRAP:
                obj = traps[(TrapType) idx];
                break;
            case ObjectType.ITEM:
                obj = items[(ItemType) idx];
                break;
            case ObjectType.DragonBall:
                obj = dragonBalls[(DragonBallType) idx];
                ((DragonBall)obj).indexBall = idx;
                break;
            case ObjectType.KEY:
                obj = keys[(KeyType) idx];
                break;
            default:
                obj = null;
                break;
        }
        return obj;
    }
}
