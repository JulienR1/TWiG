using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TimeManager))]
public class Game : MonoBehaviour
{
    public static Game instance;

    public TimeManager timeManager { get; private set; }
    public UIManager uiManager { get; private set; }

    public Menu menu;

    private void Awake()
    {
        instance = this;

        List<IManager> managers = new List<IManager>();
        managers.Add(timeManager = GetComponent<TimeManager>());
        managers.Add(uiManager = GetComponent<UIManager>());

        foreach (IManager manager in managers)
            manager.Initialize();
    }

    private void Start()
    {
        uiManager.OpenMenu(menu);
    }

    public void GameOver()
    {

    }
}