using UnityEngine;

public static class Easing
{
    public static float EaseIn(float t)
    {
        return Mathf.Pow(t, 3);
    }

    public static float EaseOut(float t)
    {
        return 1f - Mathf.Pow(1f - t, 3);
    }

    public static float EaseInOut(float t)
    {
        if (t < 0.5f)
        {
            return 4f * Mathf.Pow(t, 3);
        }
        else
        {
            return 1f - Mathf.Pow(-2f * t + 2f, 3) / 2f;
        }
    }
}
