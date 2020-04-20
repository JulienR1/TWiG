using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Composter : WorldInteractable
{
    [SerializeField] private int maxFertilizer = 10;
    [SerializeField] private int ticksToCreateFertilizer = 3;
    private int appleCount = 0;
    private int currentFertilizerCount = 0;

    private int ticksUntilNextCreation = 0;

    protected override void Start()
    {
        base.Start();
        HasItem = true;

        appleCount = 0;
        currentFertilizerCount = 2;

        TimeManager.OnTick += CreateFertilizer;
    }

    private void CreateFertilizer()
    {
        if (appleCount > 0)
        {
            if (ticksUntilNextCreation <= 0)
            {
                appleCount--;
                currentFertilizerCount++;
                ticksUntilNextCreation = ticksToCreateFertilizer;
            }
        }
    }

    public void AddApples(int amount)
    {
        appleCount += amount;
        appleCount = Mathf.Clamp(appleCount, 0, maxFertilizer - currentFertilizerCount);
    }

    public override T Interact<T>()
    {
        return (T)(object)currentFertilizerCount;
    }

    public int TakeFertilizer(float percentAmount)
    {
        int amount = Mathf.RoundToInt(currentFertilizerCount * percentAmount);
        currentFertilizerCount -= amount;
        return amount;
    }
}
