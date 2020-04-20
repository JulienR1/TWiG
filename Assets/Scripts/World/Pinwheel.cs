using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinwheel : WorldInteractable
{
    [SerializeField] private float maxCoolingEmission = 4f;
    [SerializeField] private float coolingEmissionDecreaseRate = 1f;

    private float currentCoolingEmission = 0;

    protected override void Start()
    {
        base.Start();

        TimeManager.OnTick += Spin;
        Stop();
    }

    private void Spin()
    {
        if (currentCoolingEmission > 0)
        {
            currentCoolingEmission -= coolingEmissionDecreaseRate; // TODO be affected by season
            currentCoolingEmission = Mathf.Clamp(currentCoolingEmission, 0, maxCoolingEmission);
        }

        CoolFlower();
    }

    private void CoolFlower()
    {
        World.flower.Cool(currentCoolingEmission);
    }

    public void StartSpin(float spinPercent)
    {
        currentCoolingEmission = maxCoolingEmission * spinPercent;
    }

    private void Stop()
    {
        currentCoolingEmission = 0;
    }

}
