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

    private float waterAverage, lightAverage, temperatureAverage;

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

        // TODO Subscribe to meteo event and apply effects
    }

    private void SetAverageStats()
    {
        waterAverage = (stats.MinMaxWater.x + stats.MinMaxWater.y) / 2f;
        lightAverage = (stats.MinMaxLight.x + stats.MinMaxLight.y) / 2f;
        temperatureAverage = (stats.MinMaxTemperature.x + stats.MinMaxTemperature.y) / 2f;

        water = waterAverage;
        lumination = lightAverage;
        temperature = temperatureAverage;
        nutrients = stats.MinNutrients;
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
        float deltaTime = 1 / Game.instance.timeManager.RevolutionTime();
        temperature += (temperature - temperatureAverage) / temperatureAverage * stats.DecayRate * deltaTime;
        water += (water - waterAverage) / waterAverage * stats.DecayRate * deltaTime;
        nutrients += (nutrients - stats.MinNutrients) / stats.MinNutrients * stats.DecayRate * deltaTime;

        switch (Game.instance.timeManager.GetSeason())
        {
            case TimeManager.Season.SUMMER:
                temperature += Mathf.Abs(temperature - temperatureAverage) * stats.DecayRate * deltaTime * 2;
                water -= Mathf.Abs(water - waterAverage) * stats.DecayRate * deltaTime * 2;
                break;
            case TimeManager.Season.AUTUMN:
                nutrients -= Mathf.Abs(nutrients - stats.MinNutrients) * stats.DecayRate * deltaTime * 2;
                break;
            case TimeManager.Season.WINTER:
                temperature -= Mathf.Abs(temperature - temperatureAverage) * stats.DecayRate * deltaTime * 2;
                water -= Mathf.Abs(water - waterAverage) * stats.DecayRate * deltaTime * 2;
                break;
        }
    }

    public void Water(float quantity)
    {
        water += quantity;
    }

    public void Fertilize(int amount)
    {
        nutrients += amount;
    }

    public void Heat(float amount)
    {
        temperature += amount;
    }

    public void Cool(float amount)
    {
        temperature -= amount;
        if (temperature < 0)
            temperature = 0;
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