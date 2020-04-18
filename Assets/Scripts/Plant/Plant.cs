using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlantRenderer))]
public class Plant : MonoBehaviour
{
    private PlantRenderer plantRenderer;

    [SerializeField] private PlantStats stats = null;

    private const int MIN_VALID_STAT_COUNT = 3;
    private int health;
    private float water;
    private float lumination;
    private float temperature;
    private float nutrients;

    private int growthStage = 0;

    private void Start()
    {
        plantRenderer = GetComponent<PlantRenderer>();
        plantRenderer.Initialize(stats.GrowthStages);
        TimeManager.OnTick += ApplyEffects;
        TimeManager.OnNextDay += Grow;

        this.health = stats.Health;
    }

    private void ApplyEffects()
    {
        // Modifier les stats selon ce qui vient de se passer
        // Set la vie en fonction des stats (+ ou -)
        // Creve si pas assez de vie
    }

    private void Grow()
    {
        if (CanGrow())
            growthStage++;
        plantRenderer.Render(growthStage);
    }

    // Pour grandir il faut:
    // - Avoir un minimum de vie
    // - Avoir au moins 3 des 4 stats de valides
    private bool CanGrow()
    {
        if (health < stats.MinHealthToGrow)
            return false;

        int validStatCount = 0;
        if (water >= stats.MinMaxWater.x && water <= stats.MinMaxWater.y)
            validStatCount++;
        if (lumination >= stats.MinMaxLight.x && lumination <= stats.MinMaxLight.y)
            validStatCount++;
        if (temperature >= stats.MinMaxTemperature.x && temperature <= stats.MinMaxTemperature.y)
            validStatCount++;
        if (nutrients >= stats.MinNutrients)
            validStatCount++;

        return validStatCount >= MIN_VALID_STAT_COUNT;
    }
}