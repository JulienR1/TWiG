using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : WorldInteractable
{
    [SerializeField] private Transform[] apples = null;
    [SerializeField] private int minTicksToGrowApple = 5;
    [SerializeField, Range(0, 1)] private float chanceToGenerateApple = 0.35f;

    private int ticksBeforeAppleGrow;
    private bool treeIsFull = false;

    protected override void Start()
    {
        base.Start();
        TimeManager.OnTick += GrowApple;
        ticksBeforeAppleGrow = minTicksToGrowApple;
        treeIsFull = false;
        HasItem = true;

        RemoveApples();
    }

    public override T Interact<T>()
    {
        int appleCount = 0;
        foreach (Transform t in apples)
            if (t.gameObject.activeSelf)
                appleCount++;
        RemoveApples();
        return (T)(object)appleCount;
    }

    private void GrowApple()
    {
        if (ticksBeforeAppleGrow-- > 0 || treeIsFull)
            return;

        if (Random.value <= chanceToGenerateApple)
            GenerateApple();
        ticksBeforeAppleGrow = minTicksToGrowApple;
    }

    private void GenerateApple()
    {
        foreach (Transform apple in apples)
        {
            if (!apple.gameObject.activeSelf)
            {
                apple.gameObject.SetActive(true);
                break;
            }
        }
        if (ticksBeforeAppleGrow != minTicksToGrowApple)
            treeIsFull = true;
    }

    private void RemoveApples()
    {
        foreach (Transform t in apples)
        {
            t.gameObject.SetActive(false);
        }
        treeIsFull = false;
    }
}
