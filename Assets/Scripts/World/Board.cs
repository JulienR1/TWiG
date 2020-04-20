using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : WorldInteractable
{
    [SerializeField] private Menu boardMenu = null;
    [SerializeField] private TaskSlot[] boardSlots = null;

    public bool IsEmpty()
    {
        foreach (TaskSlot slot in boardSlots)
            if (slot.GetTask() != null && slot.GetTask().type != Task.TaskType.NONE)
                return false;
        return true;
    }

    public override T Interact<T>()
    {
        foreach (TaskSlot slot in boardSlots)
        {
            if (slot.GetTask() != null && slot.GetTask().type != Task.TaskType.NONE)
            {
                Task task = slot.GetTask();
                slot.RemoveTask();
                UpdateUI();
                return (T)(object)task;
            }
        }
        return default(T);
    }

    private void UpdateUI()
    {
        print("Update board sprite");
    }

    private void OnMouseDown()
    {
        Open();
    }

    public void Open()
    {
        Game.instance.uiManager.OpenMenu(boardMenu);
    }
}