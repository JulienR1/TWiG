using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskMenu : Menu
{
    private TaskSlot currentTaskSlot;

    public void BindToTaskSlot(TaskSlot slot)
    {
        currentTaskSlot = slot;
    }

    public override void Close()
    {
        // TODO: Set task instead of null
        currentTaskSlot.AssignTask(null);
        currentTaskSlot = null;
        base.Close();
    }
}
