using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlantStats", menuName = "TWiG/Plant/Stats", order = 0)]
public class PlantStats : ScriptableObject
{
    [SerializeField] new private string name = "Plant";
    [SerializeField] private int health = 20;
    [SerializeField] private int minHealthToGrow = 40;
    [SerializeField] private int maxHealthGain = 5;

    [Header("Effect stats")]
    [SerializeField] private Vector2 minMaxWater = Vector2.zero;
    [SerializeField] private Vector2 minMaxLight = Vector2.zero;
    [SerializeField] private Vector2 minMaxTemperature = Vector2.zero;
    [SerializeField] private float minNutrients = 0;
    [SerializeField] private float decayRate = 0.5f;

    [Header("Growth stats")]
    [SerializeField] private PlantStage[] growthStages = null;
    [SerializeField] private int growthPerDay = 0;

    public string Name { get => name; }
    public int Health { get => health; }
    public int MinHealthToGrow { get => minHealthToGrow; }
    public int MaxHealthGain { get => maxHealthGain; }
    public Vector2 MinMaxWater { get => minMaxWater; }
    public Vector2 MinMaxLight { get => minMaxLight; }
    public Vector2 MinMaxTemperature { get => minMaxTemperature; }
    public float MinNutrients { get => minNutrients; }
    public float DecayRate { get => decayRate; }

    public PlantStage[] GrowthStages { get => growthStages; }
    public int GrowthPerDay { get => growthPerDay; }
}

[System.Serializable]
public class PlantStage
{
    public Sprite[] sprites;
    public float animationTime;
    public Vector2Int minMaxGrowth;
}