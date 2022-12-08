using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;

[System.Serializable]
public class GiantBossDictionary : SerializableDictionaryBase<BossType, Enemy>
{

}

[CreateAssetMenu(fileName = "GiantBossSO", menuName = "Data/GiantBossSO")]
public class GiantBossSO : SingletonScriptableObject<GiantBossSO>
{
    public GiantBossDictionary giantBosses = new GiantBossDictionary();
}
