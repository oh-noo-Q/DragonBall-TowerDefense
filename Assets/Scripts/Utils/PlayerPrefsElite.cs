using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsElite : MonoBehaviour
{
    public static int[] GetIntArray(string Prefs)
    {
        string[] tmp = PlayerPrefs.GetString(Prefs).Split("|"[0]);
        int[] myInt = new int[tmp.Length - 1];
        for (int i = 0; i < tmp.Length - 1; i++)
        {
            myInt[i] = int.Parse(tmp[i]);
        }
        return myInt;
    }

    public static void SetIntArray(string Prefs, int[] _Value)
    {
        string Value = "";
        for (int y = 0; y < _Value.Length; y++) { Value += _Value[y].ToString() + "|"; }
        PlayerPrefs.SetString(Prefs, Value);
    }

    public static void SetBoolean(string Prefs, bool _Value)
    {
        PlayerPrefs.SetInt(Prefs, _Value ? 1 : 0);
    }

    public static void SetBoolean(string Prefs, bool _Value, int id)
    {
        PlayerPrefs.SetInt(Prefs, _Value ? 1 : 0);
    }

    public static bool GetBoolean(string Prefs, bool defaultValue)
    {
        if (PlayerPrefs.GetInt(Prefs, defaultValue ? 1 : 0) == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
