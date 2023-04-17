using System.Collections;
using UnityEngine;

public static class PlantData {
    public static float maxSize = 100.0f;
    public static float minSize = 30.0f;
    public static float growthTime = 30.0f;
    public static float reproduceTime = 120.0f;

    public static Vector3 minVector = new Vector3(minSize, minSize, minSize);
    public static Vector3 maxVector = new Vector3(maxSize, maxSize, maxSize);
}
