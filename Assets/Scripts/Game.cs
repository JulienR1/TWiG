using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TimeManager))]
public class Game : MonoBehaviour
{
    public static Game instance;

    private TimeManager timeManager;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        List<IManager> managers = new List<IManager>();
        managers.Add(timeManager = GetComponent<TimeManager>());

        foreach (IManager manager in managers)
            manager.Initialize();
    }

}
