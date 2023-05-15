using UnityEngine;

public static class RandomUtils
{
    public static bool RandomBool()
    {
        return Random.value <= 0.5f;
    }
}