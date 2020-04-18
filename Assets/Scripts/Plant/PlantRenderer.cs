using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantRenderer : MonoBehaviour
{
    [SerializeField] private Image image = null;

    private PlantStage[] stages;
    private float nextAnimationTime;

    private int currentGrowthIndex;
    private int currentAnimationIndex;

    public void Initialize(PlantStage[] stages)
    {
        this.stages = stages;
        currentGrowthIndex = 0;
    }

    public void Render(int growthStage)
    {
        currentGrowthIndex = GetCurrentGrowthIndex(growthStage);
        currentAnimationIndex = 0;
        Animate();
    }

    private void Update()
    {
        if (Time.time >= nextAnimationTime)
            Animate();
    }

    private void Animate()
    {
        nextAnimationTime = Time.time + stages[currentGrowthIndex].animationTime;
        image.sprite = stages[currentGrowthIndex].sprites[currentAnimationIndex];
        currentAnimationIndex = (currentAnimationIndex + 1) % stages[currentGrowthIndex].sprites.Length;
    }

    private int GetCurrentGrowthIndex(int growthStage)
    {
        int growthIndex = 0;
        for (; growthIndex < stages.Length; growthIndex++)
            if (growthStage > stages[growthIndex].minMaxGrowth.x && growthStage < stages[growthIndex].minMaxGrowth.y)
                break;
        return --growthIndex;
    }
}
