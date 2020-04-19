using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskMenu : Menu
{
    [SerializeField] private ToggleGroup toggleGroup;
    [SerializeField] private Slider valueSlider = null;

    private TaskSlot currentTaskSlot;
    private Task currentTask;

    public void BindToTaskSlot(TaskSlot slot)
    {
        currentTaskSlot = slot;
        currentTask = slot.GetTask();

        if (currentTask == null)
        {
            currentTask = new Task();
            valueSlider.enabled = false;
        }
        else
        {
            int currentToggleIndex = 0;
            foreach (Toggle t in toggleGroup.ActiveToggles())
            {
                if (currentToggleIndex == (int)currentTask.type)
                    t.isOn = true;
                currentToggleIndex++;
            }
            valueSlider.value = currentTask.value;
        }
    }

    public void Save()
    {
        currentTaskSlot.AssignTask(currentTask);
        currentTaskSlot = null;
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
    }

    public void SetValue()
    {
        currentTask.value = valueSlider.value;
    }
}
