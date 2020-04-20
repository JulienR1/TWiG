﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour, IManager
{
    [SerializeField] private float worldLimit = 12f;
    [SerializeField] private float[] layerHeights = null;

    public static Plant flower { get; private set; }
    public static Board board { get; private set; }
    public static Well well { get; private set; }
    public static Composter composter { get; private set; }
    public static AppleTree appleTree { get; private set; }
    public static Fire fire { get; private set; }

    public void Initialize()
    {
        // Detect world limits automatically?

        flower = FindObjectOfType<Plant>();
        board = FindObjectOfType<Board>();
        well = FindObjectOfType<Well>();
        composter = FindObjectOfType<Composter>();
        appleTree = FindObjectOfType<AppleTree>();
        fire = FindObjectOfType<Fire>();
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
        return layerHeights.Length;
    }

    public float GetLayerFloor(int layer)
    {
        return layerHeights[layer];
    }
}
