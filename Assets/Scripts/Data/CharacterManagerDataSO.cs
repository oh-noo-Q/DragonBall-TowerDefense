using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterManager", menuName = "Data/CharacterManager")]

public class CharacterManagerDataSO : SingletonScriptableObject<CharacterManagerDataSO>
{
    public CharacterDictionary characterDic = new CharacterDictionary();
}
