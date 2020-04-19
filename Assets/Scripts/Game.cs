using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TimeManager))]
public class Game : MonoBehaviour
{
    public static Game instance;

    public TimeManager timeManager { get; private set; }
    public MapBuilder mapBuilder { get; private set; }

    private void Awake()
    {
        instance = this;

        List<IManager> managers = new List<IManager>();
        managers.Add(timeManager = GetComponent<TimeManager>());
        managers.Add(mapBuilder = FindObjectOfType<MapBuilder>());

        foreach (IManager manager in managers)
            manager.Initialize();
    }

    public void GameOver()
    {

    }
}