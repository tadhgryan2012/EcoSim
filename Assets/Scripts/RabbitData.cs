using System.Collections;
using UnityEngine;

public static class RabbitData {
    public static float maxHunger = 1.0f;
    public static float hungerDrainRate = 0.01f;
    public static float hungerRefilAmount = 0.2f;
    public static float anglerSpeed = 45.0f;
    public static float animationNormalSpeed = 1.0f;
    public static float animationRunSpeed = 2.0f;
    public static float ageOfDeath = 300.0f;
    public static float ageOfMaturity = 120.0f;
    public static float mateCooldown = 120.0f;

    public static float maxSize = 1.0f;
    public static float minSize = 0.3f;
    public static Vector3 minVector = new Vector3(minSize, minSize, minSize);
    public static Vector3 maxVector = new Vector3(maxSize, maxSize, maxSize);
}
