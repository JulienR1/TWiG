using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlantStats", menuName = "TWiG/Plant/Stats", order = 0)]
public class PlantStats : ScriptableObject
{
    public float Health;
    public Vector2 minMaxWater;
    public Vector2 minMaxLight;
    public Vector2 minMaxTemperature;
    public float minNutrients;

}