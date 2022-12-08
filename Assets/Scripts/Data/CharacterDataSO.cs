using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;
using UnityEngine.UI;

public enum Character
{
    Default,
    Radit,
    Nappu,
    Drum,
    Tambourie,
    St6,
    St5,
    St4,
    St3,
    St2,
    St1,
    Shisami,
    Puipui,
    N19,
    DrGero,
    Babidi,
    Kirilin,
    N16,
    N17,
    N18,
    Bulma,
    Trunk,
    Goten,
    Cadic,
    Picolo,
    SuperPicolo,
    SuperGoku,
}

public enum TypeCharacter
{
    Small,
    Big
}

public enum RankCharacter
{
    Silver,
    Golden,
    Super,
}

[CreateAssetMenu(fileName = "Character", menuName = "Data/Character")]
[System.Serializable]

public class CharacterDataSO : ScriptableObject
{
    public string name;
    public Sprite icon;
    public RankCharacter rank;
    public TypeCharacter type;
}

[System.Serializable]
public class CharacterDictionary : SerializableDictionaryBase<Character, CharacterDataSO> { }
