using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    public enum TaskType { WATER, NUTRIENT, TEMPERATURE, LIGHT, NONE };
    public TaskType type;
    public float value;
}
