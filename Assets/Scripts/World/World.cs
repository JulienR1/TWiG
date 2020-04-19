using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour, IManager
{
    [SerializeField] private float worldLimit = 12f;
    [SerializeField] private int layerCount = 4;

    public void Initialize()
    {
        // Detect world limits automatically?
        // Get elements that populate it, sort by layer
    }

    public static float GetLayerScaleFactor(int layerNumber)
    {
        return Mathf.Exp(2.5f - layerNumber);
    }

    public float GetWorldLimit()
    {
        return worldLimit;
    }

    public int GetLayerCount()
    {
        return layerCount;
    }
}
