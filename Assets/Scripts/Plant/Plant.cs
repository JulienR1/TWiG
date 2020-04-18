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
    }

    private void OnGrow()
    {
        plantRenderer.Render(growthStage);
    }
}
