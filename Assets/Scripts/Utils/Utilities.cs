using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public static class Extension
{

    #region Datetime

    public static double DeltaSecond(this double newTimestamp,
                                     double oldTimestamp)
    {
        DateTime oldDate = oldTimestamp.JavaTimeStampToDateTime()/*.ToLocalTime()*/;
        DateTime newDate = newTimestamp.JavaTimeStampToDateTime();

        return newDate.Subtract(oldDate).TotalSeconds;
    }

    public static double DeltaMinute(this double newTimestamp,
                                     double oldTimestamp)
    {
        DateTime oldDate = oldTimestamp.JavaTimeStampToDateTime();
        DateTime newDate = newTimestamp.JavaTimeStampToDateTime();

        return newDate.Subtract(oldDate).TotalMinutes;
    }

    public static double DeltaHour(this double newTimestamp,
                                   double oldTimestamp)
    {
        DateTime oldDate = oldTimestamp.JavaTimeStampToDateTime();
        DateTime newDate = newTimestamp.JavaTimeStampToDateTime();

        return newDate.Subtract(oldDate).TotalHours;
    }

    public static double DeltaDay(this double newTimestamp, double oldTimestamp)
    {
        DateTime oldDate = oldTimestamp.JavaTimeStampToDateTime();
        DateTime newDate = newTimestamp.JavaTimeStampToDateTime();

        DateTime oldActualDate = new DateTime(oldDate.Year,
                                     oldDate.Month,
                                     oldDate.Day);
        DateTime newActualDate = new DateTime(newDate.Year,
                                     newDate.Month,
                                     newDate.Day);

        return newActualDate.Subtract(oldActualDate).TotalDays;
    }

    public static double DeltaSecondToNextDay(this double timestamp)
    {
        DateTime oldDate = timestamp.JavaTimeStampToDateTime();
        DateTime newDate = oldDate.AddDays(1);
        DateTime newActualDate = new DateTime(newDate.Year,
                                     newDate.Month,
                                     newDate.Day);
        return newActualDate.Subtract(oldDate).TotalSeconds;
    }

    public static double DeltaSecondToNextDayinTime(this double timestamp, int hours, int minutes)
    {
        DateTime oldDate = timestamp.JavaTimeStampToDateTime();
        DateTime newDate = oldDate.AddDays(1);
        DateTime newActualDate = new DateTime(newDate.Year,
                                     newDate.Month,
                                     newDate.Day,
                                     hours,
                                     minutes, 
                                     0);
        return newActualDate.Subtract(oldDate).TotalSeconds;
    }

    /// <summary>
    /// Javas timestamp to date time. Java timestamp is milliseconds past epoch.
    /// </summary>
    /// <returns>Java timestamp is milliseconds past epoch</returns>
    /// <param name="javaTimeStamp">Java timestamp.</param>
    // 
    public static DateTime JavaTimeStampToDateTime(this double javaTimeStamp)
    {
        System.DateTime dtDateTime = new DateTime(1970,
                                         1,
                                         1,
                                         0,
                                         0,
                                         0,
                                         0,
                                         System.DateTimeKind.Utc);
        dtDateTime = dtDateTime.AddMilliseconds(javaTimeStamp).ToUniversalTime();
        return dtDateTime;
    }

    public static double ToJavaTimeStamp(this DateTime date)
    {
        System.DateTime dtDateTime = new DateTime(1970,
                                         1,
                                         1,
                                         0,
                                         0,
                                         0,
                                         0,
                                         System.DateTimeKind.Utc);
        return date.Subtract(dtDateTime).TotalMilliseconds;
    }

    /// <summary>
    /// Timestamp to date time.
    /// </summary>
    /// <returns>Unix timestamp from seconds</returns>
    /// <param name="timeStamp">Timestamp.</param>
    public static DateTime TimeStampToDateTime(this long timeStamp)
    {
        System.DateTime dtDateTime = new DateTime(1970,
                                         1,
                                         1,
                                         0,
                                         0,
                                         0,
                                         0,
                                         System.DateTimeKind.Utc);
        dtDateTime = dtDateTime.AddSeconds(timeStamp).ToUniversalTime();
        return dtDateTime;
    }

    public static double AddMinutes(this double javaTimeStamp, double minutes)
    {
        DateTime newDate = javaTimeStamp.JavaTimeStampToDateTime();
        return newDate.AddMinutes(minutes).ToJavaTimeStamp();
    }

    public static int ToDayOfWeek(this double javaTimeStamp)
    {
        DateTime dateTime = javaTimeStamp.JavaTimeStampToDateTime();
        return (int)dateTime.DayOfWeek + 1;
    }

    #endregion

    #region Format Number

    const string NUMBER_FORMAT = "N0";
    const string NUMBER_FORMAT_THOUSAND = "0K";
    const string NUMBER_FORMAT_MILLION = "0M";
    const string TIME_FORMAT_MM_SS = "{0:D2}:{1:D2}";
    const string TIME_FORMAT_HH_MM_SS = "{0:D}:{1:D2}:{2:D2}";
    const string TIME_FORMAT_DD_HH_MM = "{0:D}:{1:D2}:{2:D2}";
    const string TIME_FORMAT_DD_HH_MM_SS = "{0:D}days {1:D2}:{2:D2}:{3:D2}";
    const string TIME_FORMAT_DD_HH_MM_SS_SHORT = "{0:D}d {1:D2}:{2:D2}:{3:D2}";
    const string TIME_FORMAT_DD_HH_MM_SHORT = "{0:D}d {1:D2}:{2:D2}";
    const string TIME_FORMAT_DD_HH_SHORT = "{0:D}d {1:D2}h";

    #region number string

    public static string ToFullNumberString(this float num)
    {
        return num.ToString(NUMBER_FORMAT);
    }

    public static string ToFullNumberString(this double num)
    {
        return num.ToString(NUMBER_FORMAT);
    }

    public static string ToFullNumberString(this int num)
    {
        return num.ToString(NUMBER_FORMAT);
    }
    
    public static string ToFullNumberString(this long num)
    {
        return num.ToString(NUMBER_FORMAT);
    }

    public static string ToShortNumberString(this float num)
    {
        if (num >= 1000000)
        {
            return (num / 1000).ToString(NUMBER_FORMAT_THOUSAND);
        }
        if (num >= 100000)
        {
            return (num / 1000).ToString(NUMBER_FORMAT_THOUSAND);
        }
        return num.ToString(NUMBER_FORMAT);
    }

    public static string ToShortNumberString(this double num)
    {
        if (num >= 1000000)
        {
            return (num / 1000).ToString(NUMBER_FORMAT_THOUSAND);
        }
        if (num >= 100000)
        {
            return (num / 1000).ToString(NUMBER_FORMAT_THOUSAND);
        }
        return num.ToString(NUMBER_FORMAT);
    }

    public static string ToShortNumberString(this int num)
    {
        if (num >= 1000000)
        {
            return (num / 1000).ToString(NUMBER_FORMAT_THOUSAND);
        }
        if (num >= 100000)
        {
            return (num / 1000).ToString(NUMBER_FORMAT_THOUSAND);
        }
        return num.ToString(NUMBER_FORMAT);
    }

    #endregion

    #region Time
    public static string ToShortTimeString(this double num)
    {
        System.TimeSpan t = System.TimeSpan.FromSeconds(num);
        return string.Format(TIME_FORMAT_MM_SS,
            t.Minutes,
            t.Seconds);
    }

    public static string ToFullTimeString(this double num)
    {
        System.TimeSpan t = System.TimeSpan.FromSeconds(num);
        return string.Format(TIME_FORMAT_HH_MM_SS,
            t.Hours,
            t.Minutes,
            t.Seconds);
    }
    public static string ToFullTimeStringWithDay(this double num)
    {
        System.TimeSpan t = System.TimeSpan.FromSeconds(num);
        return string.Format(TIME_FORMAT_DD_HH_MM,
            t.Days,
            t.Hours,
            t.Minutes);
    }
    public static string ToFullTimeStringWithDaySecond(this double num)
    {
        System.TimeSpan t = System.TimeSpan.FromSeconds(num);
        return string.Format(TIME_FORMAT_DD_HH_MM_SS_SHORT,
            t.Days,
            t.Hours,
            t.Minutes,
            t.Seconds);
    }

    public static string ToFullAdaptiveTimeString(this double totalSec)
    {
        System.TimeSpan t = System.TimeSpan.FromSeconds(totalSec);
        if(t.Days >= 1)
        {
            return string.Format(TIME_FORMAT_DD_HH_MM_SS_SHORT,
            t.Days,
            t.Hours,
            t.Minutes,
            t.Seconds);
        } 
        else
        {
            return string.Format(TIME_FORMAT_HH_MM_SS,
                t.Hours,
                t.Minutes,
                t.Seconds);
        }
        
    }

    public static string ToShortAdaptiveTimeString(this double totalSec)
    {
        System.TimeSpan t = System.TimeSpan.FromSeconds(totalSec);
        if(t.Days >= 1)
        {
            return string.Format(TIME_FORMAT_DD_HH_SHORT,
                t.Days,
                t.Hours);
        } else
        {
            return string.Format(TIME_FORMAT_HH_MM_SS,
                t.Hours,
                t.Minutes,
                t.Seconds);
        }
    }

    public static string ToFullDateTimeString(this double num)
    {
        DateTime date = num.JavaTimeStampToDateTime();
        return date.ToLongDateString() + " " + date.ToLongTimeString();
    }

    public static string ToAllHourTimeString(this double totalSec) {
        System.TimeSpan t = System.TimeSpan.FromSeconds(totalSec);
        return string.Format(TIME_FORMAT_HH_MM_SS,
            ((int) t.TotalHours).To2DigitNumber(),
            t.Minutes.To2DigitNumber(),
            t.Seconds.To2DigitNumber());
    }

    public static string To2DigitNumber(this int num) {
        if (num < 10) return String.Format("0{0}", num);
        else return num.ToString();
    }

    #endregion
    #endregion

    public static T[] SubArray<T>(this T[] data, int index, int length)
    {
        T[] result = new T[length];
        Array.Copy(data, index, result, 0, length);
        return result;
    }

    public static bool isVisible(this Vector3 position)
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(position);
        return (screenPoint.x > 0) && (screenPoint.x < 1)
        && (screenPoint.y > 0) && (screenPoint.y < 1)
        && (screenPoint.z > 0);
    }

    public static bool IsNull(this UnityEngine.Object go)
    {
#if UNITY_EDITOR
        return go == null;
#else
		return (object) go == null;
#endif
    }

    public static int ToInt(this Color color)
    {
        return (Mathf.RoundToInt(color.a * 255) << 24) +
        (Mathf.RoundToInt(color.r * 255) << 16) +
        (Mathf.RoundToInt(color.g * 255) << 8) +
        Mathf.RoundToInt(color.b * 255);
    }

    public static Color ToColor(this int value)
    {
        var a = (float)(value >> 24 & 0xFF) / 255f;
        var r = (float)(value >> 16 & 0xFF) / 255f;
        var g = (float)(value >> 8 & 0xFF) / 255f;
        var b = (float)(value & 0xFF) / 255f;
        return new Color(r, g, b, a);
    }

    private static int _playServiceState = -1;

    public static bool IsPlayServicesAvailable()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (_playServiceState == -1)
            {
                const string GoogleApiAvailability_Classname = "com.google.android.gms.common.GoogleApiAvailability";
                AndroidJavaClass clazz = new AndroidJavaClass(GoogleApiAvailability_Classname);
                AndroidJavaObject obj = clazz.CallStatic<AndroidJavaObject>("getInstance");

                var androidJC = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                var activity = androidJC.GetStatic<AndroidJavaObject>("currentActivity");

                _playServiceState = obj.Call<int>("isGooglePlayServicesAvailable",
                    activity);

            }
            // 0 == success
            // 1 == service_missing
            // 2 == update service required
            // 3 == service disabled
            // 18 == service updating
            // 9 == service invalid
            return _playServiceState == 0;
        }
        else
        {
            return true;
        }
    }

    #region Encrypt

    public static string sha256(this string src)
    {
        var crypt = new System.Security.Cryptography.SHA256Managed();
        var hash = new System.Text.StringBuilder();
        byte[] crypto = crypt.ComputeHash(System.Text.Encoding.UTF8.GetBytes(src));
        foreach (byte theByte in crypto)
        {
            hash.Append(theByte.ToString("x2"));
        }
        return hash.ToString();
    }

    public static string base64(this string src)
    {
        byte[] bytesToEncode = System.Text.Encoding.UTF8.GetBytes(src);
        return Convert.ToBase64String(bytesToEncode);
    }

    #endregion

    #region List

    public static string ListIntToString(this List<int> list)
    {
        return string.Join(",", list.Select(x => x.ToString()).ToArray());
    }

    #endregion
}

public struct HashCode
{
    private readonly int value;

    private HashCode(int value)
    {
        this.value = value;
    }

    public static implicit operator int(HashCode hashCode)
    {
        return hashCode.value;
    }

    public static HashCode Of<T>(T item)
    {
        return new HashCode(GetHashCode<T>(item));
    }

    public HashCode And<T>(T item)
    {
        return new HashCode(CombineHashCodes(this.value, GetHashCode<T>(item)));
    }

    //	public HashCode AndEach<T>(IEnumerable items)
    //	{
    //		if (items == null)
    //		{
    //			return this.value;
    //		}
    //
    //		var hashCode = items.Any() ? items.Select(GetHashCode).Aggregate(CombineHashCodes) : 0;
    //		return new HashCode(CombineHashCodes(this.value, hashCode));
    //	}

    private static int CombineHashCodes(int h1, int h2)
    {
        unchecked
        {
            // Code copied from System.Tuple so it must be the best way to combine hash codes or at least a good one.
            return ((h1 << 5) + h1) ^ h2;
        }
    }

    private static int GetHashCode<T>(T item)
    {
        return item == null ? 0 : item.GetHashCode();
    }

}