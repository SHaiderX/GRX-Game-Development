using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Vibration
{
#if UNITY_ANDROID && !UNITY_EDITOR
    public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    public static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
    public static AndroidJavaClass vibrationEffectClass = new AndroidJavaClass("android.os.VibrationEffect");
    public static int defaultAmplitude = vibrationEffectClass.GetStatic<int>("DEFAULT_AMPLITUDE");
    public static AndroidJavaClass androidVersion = new AndroidJavaClass("android.os.Build$VERSION");
    public static int apiLevel = androidVersion.GetStatic<int>("SDK_INT");
#else
    public static AndroidJavaClass unityPlayer;
    public static AndroidJavaObject vibrator;
    public static AndroidJavaObject currentActivity;
    public static AndroidJavaClass vibrationEffectClass;
    public static int defaultAmplitude;

#endif

    public static void Vibrate(long milliseconds)
    {
        if (PlayerPrefs.GetInt("Vibrate") == 0)
            CreateOneShot(milliseconds, defaultAmplitude);
    }

    public static void CreateOneShot(long milliseconds, int amplitude)
    {
        if (PlayerPrefs.GetInt("Vibrate") == 0)
            CreateVibrationEffect("createOneShot", new object[] { milliseconds, amplitude });
    }

    public static void CreateWaveform(long[] timings, int repeat)
    {
        if (PlayerPrefs.GetInt("Vibrate") == 0)
            CreateVibrationEffect("createWaveform", new object[] { timings, repeat });
    }

    public static void CreateWaveform(long[] timings, int[] amplitudes, int repeat)
    {
        if (PlayerPrefs.GetInt("Vibrate") == 0)
            CreateVibrationEffect("createWaveform", new object[] { timings, amplitudes, repeat });
    }

    public static void CreateVibrationEffect(string function, params object[] args)
    {
        if (isAndroid() && HasAmplituideControl())
        {
            AndroidJavaObject vibrationEffect = vibrationEffectClass.CallStatic<AndroidJavaObject>(function, args);
            vibrator.Call("vibrate", vibrationEffect);
        }
        else
            Handheld.Vibrate();
    }

    //public static void VibratePattern(long milliseconds, int amplitude, float time, int repeats) {
    //    StartCoroutine(VibrateCr(milliseconds, amplitude, time, repeats));
    //}

    public static IEnumerator VibratePattern(long milliseconds, int amplitude, float time, int repeats)
    {
        if (PlayerPrefs.GetInt("Vibrate") == 0) {
            int i = 0;
            while (true)
            {
                CreateOneShot(milliseconds, amplitude);

                if (i == repeats)
                    yield break;

                i++;
                yield return new WaitForSeconds(time / 100);
            }
        }
        
    }

    public static bool HasVibrator()
    {
        return vibrator.Call<bool>("hasVibrator");
    }

    public static bool HasAmplituideControl()
    {
        //if (apiLevel >= 26)
        return vibrator.Call<bool>("hasAmplitudeControl"); // API 26+ specific
        //else
        //    return false; // no amplitude control below API level 26
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