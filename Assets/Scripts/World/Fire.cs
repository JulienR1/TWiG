using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : WorldInteractable
{
    [SerializeField] private float maxHeathEmission = 5;
    [SerializeField] private float baseEmissionDecreaseRate = 1;
    private float currentHeathEmission;

    protected override void Start()
    {
        base.Start();
        TurnOff();

        TimeManager.OnTick += DecreaseEmission;
    }

    private void DecreaseEmission()
    {
        if (currentHeathEmission > 0)
        {
            currentHeathEmission -= baseEmissionDecreaseRate; // TODO be affected by season
            currentHeathEmission = Mathf.Clamp(currentHeathEmission, 0, maxHeathEmission);
        }

        HeatFlower();
    }

    private void HeatFlower()
    {
        World.flower.Heat(currentHeathEmission);
    }

    public void TurnOn(float intensityPercent)
    {
        currentHeathEmission = maxHeathEmission * intensityPercent;
    }

    private void TurnOff()
    {
        currentHeathEmission = 0;
    }

}
