using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlantRenderer))]
public class Plant : MonoBehaviour
{
    private PlantRenderer plantRenderer;

    [SerializeField] private PlantStats stats = null;

    private int growthStage = 0;

    private void Start()
    {
        plantRenderer = GetComponent<PlantRenderer>();
        plantRenderer.Initialize(stats.GrowthStages);
        TimeManager.OnNextDay += OnGrow;
    }

    private void OnGrow()
    {
        if (CanGrow())
            growthStage++;
        plantRenderer.Render(growthStage);
    }

    private bool CanGrow()
    {
        // Check if the plant is healthy enough to get to next stage
        // If not apply penalty ?
        return true;
    }
}
