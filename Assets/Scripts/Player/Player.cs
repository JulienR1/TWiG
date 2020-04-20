using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController)),RequireComponent(typeof(PlayerAnimator))]
public class Player : MonoBehaviour, IManager
{
    private PlayerController controller;
    private PlayerAnimator animator;    

    private Task currentTask;
    private HandItem handItem;
    private int taskProgress;
    private float ticksToWait = 0;

    [SerializeField] private int ticksPerInteraction = 1;

    public void Initialize()
    {
        controller = GetComponent<PlayerController>();
        animator = GetComponent<PlayerAnimator>();
        animator.Initialize();
        handItem = new HandItem();

        TimeManager.OnTick += UpdateBehaviour;

        animator.ToggleAnimation(PlayerAnimator.PlayerState.IDLE);
    }

    private void UpdateBehaviour()
    {
        if (ticksToWait-- > 0)
            return;

        if (!HasTask())
        {
            if (!Game.instance.world.board.IsEmpty())
            {
                controller.MoveToTarget(Game.instance.world.board, GetTaskFromBoard);
                animator.ToggleAnimation(PlayerAnimator.PlayerState.WALKING);
            }
        }
        else
        {
            ExecuteTask();
        }
    }

    private bool GetTaskFromBoard()
    {
        currentTask = Game.instance.world.board.Interact<Task>();
        animator.ToggleAnimation(PlayerAnimator.PlayerState.IDLE);
        animator.ShowInteraction(PlayerAnimator.Interaction.INTERACTING);
        ticksToWait = ticksPerInteraction;
        return true;
    }

    private void ExecuteTask()
    {
        switch (currentTask.type)
        {
            case Task.TaskType.WATER:
                ExecuteWaterSequence();
                break;
            case Task.TaskType.NUTRIENT: break;
            case Task.TaskType.APPLE: break;
            case Task.TaskType.TEMPERATURE: break;
            case Task.TaskType.LIGHT: break;
            default:
                animator.ClearAnimation();
                break;
        }
    }

    private void ExecuteWaterSequence()
    {
        if (taskProgress == 0)
        {
            controller.MoveToTarget(Game.instance.world.well, ReachedTarget);
            animator.ToggleAnimation(PlayerAnimator.PlayerState.WALKING);
            animator.ShowInteraction(PlayerAnimator.Interaction.WATER);
        }
        else if (taskProgress == 1)
        {
            ticksToWait = Mathf.Clamp(0, Mathf.RoundToInt(Game.instance.world.well.Interact<int>() * currentTask.value), 30);
            animator.ShowInteraction(PlayerAnimator.Interaction.INTERACTING);
            handItem.quantity = Game.instance.world.well.GetMaxWater() * currentTask.value;
            handItem.isHeld = Game.instance.world.well.TakeItem();
            taskProgress++;
        }
        else if (taskProgress == 2)
        {
            controller.MoveToTarget(Game.instance.world.flower, ReachedTarget);
            animator.ToggleAnimation(PlayerAnimator.PlayerState.WALKING_WATER);
        }
        else if (taskProgress == 3)
        {
            animator.ShowInteraction(PlayerAnimator.Interaction.INTERACTING);
            Game.instance.world.flower.Water(handItem.quantity);            
            ticksToWait = ticksPerInteraction;
            taskProgress++;            
        }
        else if (taskProgress == 4)
        {
            controller.MoveToTarget(Game.instance.world.well, ReachedTarget);
            animator.ToggleAnimation(PlayerAnimator.PlayerState.WALKING_WATER);
        }
        else if (taskProgress == 5)
        {
            Game.instance.world.well.GiveItem();
            handItem.isHeld = false;
            CompleteTask();
        }
    }

    private bool ReachedTarget()
    {
        animator.ToggleAnimation(PlayerAnimator.PlayerState.IDLE);
        taskProgress++;
        return true;
    }

    private void CompleteTask()
    {
        currentTask = null;
    }

    public bool HasTask()
    {
        return currentTask != null;
    }
}

public class HandItem
{
    public bool isHeld;
    public float quantity;

    public HandItem()
    {
        isHeld = false;
        quantity = 0;
    }
}
