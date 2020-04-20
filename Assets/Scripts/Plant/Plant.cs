using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlantRenderer))]
public class Plant : WorldInteractable
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

    protected override void Start()
    {
        base.Start();

        plantRenderer = GetComponent<PlantRenderer>();
        plantRenderer.Initialize(stats.GrowthStages);
        TimeManager.OnTick += ApplyEffects;
        TimeManager.OnNextDay += Grow;

        this.health = stats.Health;
        SetAverageStats();
    }

    private void SetAverageStats()
    {
        water = (stats.MinMaxWater.x + stats.MinMaxWater.y) / 2f;
        lumination = (stats.MinMaxLight.x + stats.MinMaxLight.y) / 2f;
        temperature = (stats.MinMaxTemperature.x + stats.MinMaxTemperature.y) / 2f;
        nutrients = (1 - stats.MinNutrients) / 2f;
    }

    private void ApplyEffects()
    {
        DisruptStats();
        UpdateHealth();
        if (health <= 0)
            Die();
    }

    private void DisruptStats()
    {

    }

    public void Water(float quantity)
    {

    }

    public void Fertilize(int amount)
    {

    }

    private void UpdateHealth()
    {
        float healthDiff = 0;
        healthDiff += GetStatEffectOnHealth(water, stats.MinMaxWater);
        healthDiff += GetStatEffectOnHealth(lumination, stats.MinMaxLight);
        healthDiff += GetStatEffectOnHealth(temperature, stats.MinMaxTemperature);
        healthDiff += ((nutrients >= stats.MinNutrients) ? 1 : (stats.MinNutrients - nutrients) / stats.MinNutrients) * stats.MaxHealthGain / 4f;

        this.health += Mathf.FloorToInt(healthDiff);
    }

    private float GetStatEffectOnHealth(float value, Vector2 limits)
    {
        return ((value > limits.x && value < limits.y) ? 1 : 0 - DistanceFromLimits(value, limits) / 4f) * stats.MaxHealthGain;
    }

    private void Die()
    {
        Game.instance.GameOver();
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

    private float DistanceFromLimits(float value, Vector2 limits)
    {
        float center = (limits.x + limits.y) / 2f;
        return Mathf.Abs((value - center) / center);
    }
}