using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskSlot : MonoBehaviour
{
    [SerializeField] private RectTransform emptySection = null;
    [SerializeField] private RectTransform taskSection = null;
    private Task task;

    private void Start()
    {
        task = null;
    }

    public void EditTask()
    {
        Game.instance.uiManager.BindTask(this);
    }

    public void AssignTask(Task task)
    {
        this.task = task;
        ToggleUI();
    }

    public void RemoveTask()
    {
        this.task = null;
        ToggleUI();
    }

    public Task GetTask()
    {
        return task;
    }

    private void ToggleUI()
    {
        emptySection.gameObject.SetActive(task == null);
        taskSection.gameObject.SetActive(task != null);
    }
}
