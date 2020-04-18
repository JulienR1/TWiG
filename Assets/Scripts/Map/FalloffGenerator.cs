using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FalloffGenerator
{
    public static float[,] GenerateFalloffMap(int size, float falloffStartFactor)
    {
        float[,] map = new float[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Vector2 centre = Vector2.one * size / 2f;
                Vector2 point = new Vector2(i, j);
                float distance = Mathf.Clamp(Vector2.Distance(centre, point), 0, size / 2f);
                float value = 2 * distance / (float)size;
                map[i, j] = Evaluate(value, falloffStartFactor);
            }
        }

        return map;
    }

    private static float Evaluate(float value, float falloffStartFactor)
    {
        float a = 3;
        float b = falloffStartFactor;
        return Mathf.Pow(value, a) / (Mathf.Pow(value, a) + Mathf.Pow(b * (1 - value), a));
    }
}
