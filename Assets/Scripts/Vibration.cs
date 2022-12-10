using UnityEngine;
using System.Collections;

public static class Vibration
{

#if UNITY_ANDROID && !UNITY_EDITOR
    public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    public static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
#else
    public static AndroidJavaClass unityPlayer;
    public static AndroidJavaObject currentActivity;
    public static AndroidJavaObject vibrator;
#endif

    public static void Vibrate()
    {
        if (isAndroid())
            vibrator.Call("vibrate");
        else
            Handheld.Vibrate();
    }


    public static void Vibrate(long milliseconds)
    {
        //if (PlayerPrefsManager.Vibration)
        //{
        //    if (isAndroid())
        //        vibrator.Call("vibrate", milliseconds);
        //    else
        //        Handheld.Vibrate();
        //}
    }

    public static void Vibrate(long[] pattern, int repeat)
    {
        //if (PlayerPrefsManager.Vibration)
        //{
        //    if (isAndroid())
        //        vibrator.Call("vibrate", pattern, repeat);
        //    else
        //        Handheld.Vibrate();
        //}
    }

    public static void VibrateWeak()
    {
        Vibrate(100);
    }

    public static void VibrateStrong()
    {
        Vibrate(300);
    }

    public static void VibrateRhythm()
    {
        long[] pattern = { 0, 100, 100, 100 };
        Vibrate(pattern, -1);
    }
    public static bool HasVibrator()
    {
        return isAndroid();
    }

    public static void Cancel()
    {
        if (isAndroid())
            vibrator.Call("cancel");
    }

    private static bool isAndroid()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
	return true;
#else
        return false;
#endif
    }
}