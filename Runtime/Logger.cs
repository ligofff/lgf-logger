using UnityEngine;

public static class Logger
{
    public static void Log(this object context, string message)
    {
        var type = context.GetType();
        var color = ColorUtility.ToHtmlStringRGB(GetColorFromObject(type));
        Debug.Log($"<color=#{color}>{$"[{type.Name}]",-35}</color> {message}");
    }

    public static void Log(this UnityEngine.Object context, string message)
    {
        var type = context.GetType();
        var color = ColorUtility.ToHtmlStringRGB(GetColorFromObject(type));
        Debug.Log($"<color=#{color}>{$"[{type.Name}]",-35}</color> {message}", context);
    }
    
    private static Color GetColorFromObject(object obj)
    {
        var hue = (float) Mathf.Abs(obj.GetHashCode()) / int.MaxValue;
            
        var color = Color.HSVToRGB(hue, 0.7f, 1f);
        color.a = 0.15f;

        return color;
    }
}