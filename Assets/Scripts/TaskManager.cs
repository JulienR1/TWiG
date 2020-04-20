using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour, IManager
{
    [SerializeField] private TaskIcon[] taskIcons = null;    

    public void Initialize() { }

    public Sprite GetIcon(Task.TaskType type)
    {
        foreach(TaskIcon icon in taskIcons)
        {
            if (icon.type == type)
                return icon.icon;
        }
        return null;
    }

    [System.Serializable]
    public struct TaskIcon
    {
        public Task.TaskType type;
        public Sprite icon;
    }
}