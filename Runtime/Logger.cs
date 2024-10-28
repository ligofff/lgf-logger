using System;
using System.Threading;
using UnityEngine;

public static class Logger
{
    public static event Action<object, string> OnLog; 
    public static event Action<object, string> OnLogWarning;
    public static event Action<object, string> OnLogError;

    private static Thread MainThread;
    
    [RuntimeInitializeOnLoadMethod]
    public static void Init()
    {
        MainThread = Thread.CurrentThread;
    }

    public static void Log(this object context, string message)
    {
        Debug.Log(GetMessage(context, message));
        OnLog?.Invoke(context, message);
    }

    public static void Log(this UnityEngine.Object context, string message)
    {
        Debug.Log(GetMessage(context, message), context);
        OnLog?.Invoke(context, message);
    }
    
    public static void LogWarning(this object context, string message)
    {
        Debug.LogWarning(GetMessage(context, message));
        OnLogWarning?.Invoke(context, message);
    }

    public static void LogWarning(this UnityEngine.Object context, string message)
    {
        Debug.LogWarning(GetMessage(context, message), context);
        OnLogWarning?.Invoke(context, message);
    }
    
    public static void LogError(this object context, string message)
    {
        Debug.LogError(GetMessage(context, message));
        OnLogError?.Invoke(context, message);
    }

    public static void LogError(this UnityEngine.Object context, string message)
    {
        Debug.LogError(GetMessage(context, message), context);
        OnLogError?.Invoke(context, message);
    }

    private static string GetMessage(this object context, string message)
    {
        var type = context.GetType();
        var color = ColorUtility.ToHtmlStringRGB(GetColorFromObject(type));
        
        var frameCount = MainThread == Thread.CurrentThread ? Time.frameCount.ToString() : $"{Thread.CurrentThread.Name}";
        return $"[{frameCount}] <color=#{color}>[{type.Name}]</color> {message}";
    }
    
    private static Color GetColorFromObject(Type obj)
    {
        var hue = (float) Mathf.Abs(obj.FullName.GetHashCode()) / int.MaxValue;
            
        var color = Color.HSVToRGB(hue, 0.5f, 1f);
        color.a = 0.15f;

        return color;
    }
}