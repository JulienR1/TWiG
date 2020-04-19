using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskMenu : Menu
{
    [SerializeField] private TMP_Text title = null;
    [SerializeField] private ToggleGroup toggleGroup = null;
    [SerializeField] private Slider valueSlider = null;
    [SerializeField] private Button saveButton = null;
    [SerializeField] private Button deleteButton = null;

    private TaskSlot currentTaskSlot;
    private Task currentTask;

    public void BindToTaskSlot(TaskSlot slot)
    {
        currentTaskSlot = slot;
        currentTask = slot.GetTask();

        if (currentTask == null)
        {
            currentTask = new Task();
            toggleGroup.SetAllTogglesOff();
            valueSlider.enabled = false;
            valueSlider.value = 0;
            deleteButton.gameObject.SetActive(false);

            title.text = "Create Task";
        }
        else
        {
            toggleGroup.GetComponentsInChildren<Toggle>()[(int)currentTask.type].isOn = true;
            valueSlider.value = currentTask.value;
            deleteButton.gameObject.SetActive(true);

            title.text = "Edit Task";
        }
        SetSaveState();
    }

    public void Save()
    {
        currentTaskSlot.AssignTask(currentTask);
        currentTaskSlot = null;
        Close();
    }

    public void Delete()
    {
        currentTaskSlot.RemoveTask();
        currentTask = null;
        Close();
    }

    public void SelectTask(int taskTypeID)
    {
        Task.TaskType taskType = (Task.TaskType)taskTypeID;
        if (currentTask.type == taskType)
        {
            currentTask.type = Task.TaskType.NONE;
            valueSlider.enabled = false;
        }
        else
        {
            currentTask.type = taskType;
            valueSlider.enabled = true;
        }
        SetSaveState();
    }

    public void SetValue()
    {
        currentTask.value = valueSlider.value;
        SetSaveState();
    }

    private void SetSaveState()
    {
        saveButton.enabled = toggleGroup.AnyTogglesOn() && valueSlider.enabled;        
    }
}
