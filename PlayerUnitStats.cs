using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitStats : MonoBehaviour {

    private static float[] damage = { 10f, 8f, 15f, 0f };
    private static float[] health = { 100f, 70f, 120f };

    public static float swordsmenDamage = damage[0];
    public static float archerDamage = damage[1];
    public static float cavalryDamage = damage[2];
    public static float wallDamage = damage[3];

    public static float swordsmenHealth = health[0];
    public static float archerHealth = health[1];
    public static float cavalryHealth = health[2];

    public static void ReceiveDamageIncrease(int unitDamage, float amount)
    {
        damage[unitDamage] += amount;
    }

    public static void ReceiveHealthIncrease(int unitHealth, float amount)
    {
        health[unitHealth] += amount;
    }
}
