using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UserData
{
    private const string Current_Level = "user_current_level";
    private const string COLLECT_ITEM_IN_LEVEL = "collect_item_in_level";
    private const string Music_Setting = "user_music_setting";
    private const string SFX_Setting = "user_sfx_setting";
    private const string Vibration_Setting = "user_vibration_setting";
    private const string Current_Coin = "user_current_coin";
    private const string NUMBER_RADA_PREFS = "number_rada";
    private const string NUMBER_PEANUT_PREFS = "number_peanut";
    private const string DRAGON_BALL_PREFS = "dragon_ball";
    private const string CHARACTER_MERGE_PREFS = "character_merge";
    private const string CURRENT_CHARACTER = "current_character";

    public static int CurrentLevel
    {
        get => PlayerPrefs.GetInt(Current_Level, 1);
        set
        {
            PlayerPrefs.SetInt(Current_Level, value);
        }
    }

    public static bool CollectItemInLevel
    {
        get => PlayerPrefs.GetInt(COLLECT_ITEM_IN_LEVEL, 1) == 1;
        set => PlayerPrefs.SetFloat(COLLECT_ITEM_IN_LEVEL, value ? 1 : 0);
    }

    public static int CurrentCoin
    {
        get => PlayerPrefs.GetInt(Current_Coin);
        set {
            PlayerPrefs.SetInt(Current_Coin, value);
            EventDispatcher.Instance.PostEvent(EventID.OnUpdateCoin);
        }
    }

    public static bool MusicSetting
    {
        get => PlayerPrefs.GetInt(Music_Setting, 1) == 1;
        set => PlayerPrefs.SetInt(Music_Setting, value ? 1 : 0);
    }

    public static bool SFXSetting
    {
        get => PlayerPrefs.GetInt(Music_Setting, 1) == 1;
        set => PlayerPrefs.SetInt(Music_Setting, value ? 1 : 0);
    }

    public static bool VibrationSetting
    {
        get => PlayerPrefs.GetInt(Vibration_Setting, 1) == 1;
        set => PlayerPrefs.SetInt(Vibration_Setting, value ? 1 : 0);
    }

    public static int NumberRada
    {
        get => PlayerPrefs.GetInt(NUMBER_RADA_PREFS, 0);
        set => PlayerPrefs.SetInt(NUMBER_RADA_PREFS, value);
    }

    public static int NumberPeanut
    {
        get => PlayerPrefs.GetInt(NUMBER_PEANUT_PREFS, 0);
        set => PlayerPrefs.SetInt(NUMBER_PEANUT_PREFS, value);
    }

    private static List<int> _dragonBall;

    public static List<int> DragonBall
    {
        get
        {
            if (_dragonBall == null)
            {
                try
                {
                    _dragonBall = new List<int>();
                    int[] temp = PlayerPrefsElite.GetIntArray(DRAGON_BALL_PREFS);
                    _dragonBall.AddRange(temp);

                }
                catch (System.Exception e)
                {
                    Debug.LogError(e);
                    return _dragonBall;
                }
            }
            return _dragonBall;
        }
        set
        {
            _dragonBall = value;
            PlayerPrefsElite.SetIntArray(DRAGON_BALL_PREFS, value.ToArray());
        }
    }

    public static void AddDragonBall(int value)
    {
        List<int> dragonBall = DragonBall;
        dragonBall[value] = dragonBall[value] + 1;
        DragonBall = dragonBall;
    }

    public static void NewDragonBall()
    {
        List<int> dragonBall = new List<int>();
        for (int i = 0; i < 7; i++)
            dragonBall.Add(0);
        DragonBall = dragonBall;
    }

    private static List<int> _characterMerge;

    public static List<int> CharacterMerge
    {
        get
        {
            if (_characterMerge == null)
            {
                try
                {
                    _characterMerge = new List<int>();
                    int[] temp = PlayerPrefsElite.GetIntArray(CHARACTER_MERGE_PREFS);
                    _characterMerge.AddRange(temp);

                }
                catch (System.Exception e)
                {
                    Debug.LogError(e);
                    return _characterMerge;
                }
            }
            return _characterMerge;
        }
        set
        {
            _characterMerge = value;
            PlayerPrefsElite.SetIntArray(CHARACTER_MERGE_PREFS, value.ToArray());
        }
    }

    public static void NewCharacterMerge(int size)
    {
        List<int> character = new List<int>();
        for (int i = 0; i < size; i++)
            character.Add(0);
        CharacterMerge = character;
    }

    public static void AddValueCharacter(int index, int value)
    {
        List<int> character = CharacterMerge;
        character[index] = value;
        CharacterMerge = character;
    }

    public static bool CheckHaveCharacterMerge()
    {
        foreach(int value in CharacterMerge)
        {
            if (value > 0) return true;
        }
        return false;
    }

    public static int CurrentCharacter
    {
        get => PlayerPrefs.GetInt(CURRENT_CHARACTER, 0);
        set => PlayerPrefs.SetInt(CURRENT_CHARACTER, value);
    }
}
