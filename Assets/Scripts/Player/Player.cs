using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController)),RequireComponent(typeof(PlayerAnimator))]
public class Player : MonoBehaviour, IManager
{
    private PlayerController controller;
    private PlayerAnimator animator;    

    private Task currentTask;

    [SerializeField] private int ticksPerInteraction = 1;

    private bool waiting = false;
    private float ticksToWait = 0;

    public void Initialize()
    {
        controller = GetComponent<PlayerController>();
        animator = GetComponent<PlayerAnimator>();

        TimeManager.OnTick += UpdateBehaviour;
        waiting = true;
    }

    private void UpdateBehaviour()
    {
        if (ticksToWait-- > 0)
            return;

        if (!HasTask())
        {
            if (!Game.instance.world.board.IsEmpty())
            {
                controller.MoveToTarget(Game.instance.world.board, GetTask);
            }
        }
        else
        {
            ExecuteTask();
        }
    }

    private void ExecuteTask()
    {

    }

    private bool GetTask()
    {
        currentTask = Game.instance.world.board.Interact<Task>();
        ticksToWait = ticksPerInteraction;
        return true;
    }

    public bool HasTask()
    {
        return currentTask != null;
    }
}
