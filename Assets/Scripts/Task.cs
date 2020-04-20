using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
{
    public enum TaskType { WATER, NUTRIENT, TEMPERATURE, LIGHT, NONE };
    public TaskType type;
    public float value;

    public bool IsCompleted { private set; get; }

    public Task() {
        type = TaskType.NONE;
        value = 0;
    }

    public override string ToString()
    {
        return "Type: " + type.ToString() + " value: " + value + " Completed: " + IsCompleted;
    }
}
