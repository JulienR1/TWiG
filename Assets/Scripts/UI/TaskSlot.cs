using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskSlot : MonoBehaviour
{
    [SerializeField] private RectTransform emptySection = null;
    [SerializeField] private RectTransform taskSection = null;
    [SerializeField] private Image taskIcon = null;
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
        taskIcon.sprite = Game.instance.taskManager.GetIcon(task.type);
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
