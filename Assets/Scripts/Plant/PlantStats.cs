﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlantStats", menuName = "TWiG/Plant/Stats", order = 0)]
public class PlantStats : ScriptableObject
{
    [SerializeField] new private string name = "Plant";
    [SerializeField] private float health = 20;

    [Header("Effect stats")]
    [SerializeField] private Vector2 minMaxWater = Vector2.zero;
    [SerializeField] private Vector2 minMaxLight = Vector2.zero;
    [SerializeField] private Vector2 minMaxTemperature = Vector2.zero;
    [SerializeField] private float minNutrients = 0;

    [Header("Growth stats")]
    [SerializeField] private PlantStage[] growthStages = null;
    [SerializeField] private int growthPerDay = 0;

    public string Name { get => name; }
    public float Health { get => health; }
    public Vector2 MinMaxWater { get => minMaxWater; }
    public Vector2 MinMaxLight { get => minMaxLight; }
    public Vector2 MinMaxTemperature { get => minMaxTemperature; }
    public float MinNutrients { get => minNutrients; }

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