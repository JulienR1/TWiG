using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TimeManager))]
public class Game : MonoBehaviour
{
    public static Game instance;

    public TimeManager timeManager { get; private set; }
    public UIManager uiManager { get; private set; }
    public TaskManager taskManager { get; private set; }
    public World world { get; private set; }
    public Player player { get; private set; }

    private void Awake()
    {
        instance = this;

        List<IManager> managers = new List<IManager>();
        managers.Add(timeManager = GetComponent<TimeManager>());
        managers.Add(uiManager = GetComponent<UIManager>());
        managers.Add(taskManager = GetComponent<TaskManager>());
        managers.Add(world = FindObjectOfType<World>());
        managers.Add(player = FindObjectOfType<Player>());

        foreach (IManager manager in managers)
            manager.Initialize();
    }

    public void GameOver()
    {

    }
}