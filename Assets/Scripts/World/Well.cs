using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Well : WorldInteractable
{
    [SerializeField] private float maxWater = 5;
    [SerializeField] private int baseTicksToFill = 3;

    private int savedTicks = 0;

    protected override void Start()
    {
        base.Start();
        HasItem = true;
        savedTicks = 0;

        // TODO Subscribe to OnRainStart
    }

    public override T Interact<T>()
    {        
        T output = (T)(object)(Mathf.Clamp(baseTicksToFill - savedTicks, 1, baseTicksToFill));
        savedTicks = 0;
        return output;
    }

    private void OnRainStart()
    {
        savedTicks++;
    }

    public float GetMaxWater()
    {
        return maxWater;
    }    
}
