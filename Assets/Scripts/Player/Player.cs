using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController)),RequireComponent(typeof(PlayerAnimator))]
public class Player : MonoBehaviour, IManager
{
    [SerializeField] private Transform handPosition = null;

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

    private void Update()
    {
        if (handItem.IsHeld)
        {
            handItem.item.position = handPosition.position;
        }
    }

    private void UpdateBehaviour()
    {
        if (ticksToWait-- > 0)
            return;

        if (!HasTask())
        {
            if (!World.board.IsEmpty())
            {
                controller.MoveToTarget(World.board, GetTaskFromBoard);
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
        currentTask = World.board.Interact<Task>();
        animator.ToggleAnimation(PlayerAnimator.PlayerState.IDLE);
        animator.ShowInteraction(PlayerAnimator.Interaction.INTERACTING);
        ticksToWait = ticksPerInteraction;
        taskProgress = 0;
        return true;
    }

    private void ExecuteTask()
    {
        switch (currentTask.type)
        {
            case Task.TaskType.WATER:
                ExecuteWaterSequence();
                break;
            case Task.TaskType.NUTRIENT:
                ExecuteNutrientSequence();
                break;
            case Task.TaskType.APPLE:
                ExecuteAppleSequence();
                break;
            case Task.TaskType.FIRE:
                ExecuteFireSequence();
                break;
            case Task.TaskType.PINWHEEL:
                ExecutePinwheelSequence();
                break;
            case Task.TaskType.LIGHT: break;
            default:
                animator.ClearAnimation();
                break;
        }
    }

    private void ExecuteWaterSequence()
    {
        switch (taskProgress)
        {
            case 0:
                controller.MoveToTarget(World.well, ReachedTarget);
                animator.ToggleAnimation(PlayerAnimator.PlayerState.WALKING);
                animator.ShowInteraction(PlayerAnimator.Interaction.WATER);
                break;
            case 1:
                ticksToWait = Mathf.Clamp(0, Mathf.RoundToInt(World.well.Interact<int>() * currentTask.value), 30);
                animator.ShowInteraction(PlayerAnimator.Interaction.INTERACTING);
                handItem.quantity = World.well.GetMaxWater() * currentTask.value;
                handItem.item = World.well.TakeItem();
                taskProgress++;
                break;
            case 2:
                controller.MoveToTarget(World.flower, ReachedTarget);
                animator.ToggleAnimation(PlayerAnimator.PlayerState.WALKING_WATER);
                break;
            case 3:
                animator.ShowInteraction(PlayerAnimator.Interaction.INTERACTING);
                World.flower.Water(handItem.quantity);
                ticksToWait = ticksPerInteraction;
                taskProgress++;
                break;
            case 4:
                controller.MoveToTarget(World.well, ReachedTarget);
                animator.ToggleAnimation(PlayerAnimator.PlayerState.WALKING_WATER);
                break;
            case 5:
                World.well.GiveItem(handItem.item);
                CompleteTask();
                break;
        }
    }

    private void ExecuteNutrientSequence()
    {
        switch (taskProgress)
        {
            case 0:
                controller.MoveToTarget(World.composter, ReachedTarget);
                animator.ToggleAnimation(PlayerAnimator.PlayerState.WALKING);
                animator.ShowInteraction(PlayerAnimator.Interaction.COMPOST);
                break;
            case 1:
                int fertilizerAmount = World.composter.Interact<int>();
                animator.ShowInteraction(PlayerAnimator.Interaction.INTERACTING);
                if (fertilizerAmount == 0)
                {
                    animator.ShowInteraction(PlayerAnimator.Interaction.FAIL);
                    CompleteTask();
                }
                else
                {
                    handItem.quantity = World.composter.TakeFertilizer(currentTask.value);
                    handItem.item = World.composter.TakeItem();
                    taskProgress++;
                }
                break;
            case 2:
                controller.MoveToTarget(World.flower, ReachedTarget);
                animator.ToggleAnimation(PlayerAnimator.PlayerState.WALKING_COMPOST);
                break;
            case 3:
                animator.ShowInteraction(PlayerAnimator.Interaction.INTERACTING);
                World.flower.Fertilize((int)handItem.quantity);
                ticksToWait = ticksPerInteraction;
                taskProgress++;
                break;
            case 4:
                controller.MoveToTarget(World.composter, ReachedTarget);
                animator.ToggleAnimation(PlayerAnimator.PlayerState.WALKING_COMPOST);
                break;
            case 5:
                World.composter.GiveItem(handItem.item);
                CompleteTask();
                break;
        }
    }

    private void ExecuteAppleSequence()
    {
        switch (taskProgress)
        {
            case 0:
                controller.MoveToTarget(World.appleTree, ReachedTarget);
                animator.ToggleAnimation(PlayerAnimator.PlayerState.WALKING);
                animator.ShowInteraction(PlayerAnimator.Interaction.APPLE);
                break;
            case 1:
                int appleCount = World.appleTree.Interact<int>();
                if (appleCount == 0)
                {
                    CompleteTask();
                }
                else
                {
                    handItem.quantity = appleCount;
                    handItem.item = World.appleTree.TakeItem();
                    animator.ShowInteraction(PlayerAnimator.Interaction.INTERACTING);
                    ticksToWait = ticksPerInteraction * handItem.quantity;
                    taskProgress++;
                }
                break;
            case 2:
                controller.MoveToTarget(World.composter, ReachedTarget);
                animator.ToggleAnimation(PlayerAnimator.PlayerState.WALKING_APPLE);
                break;
            case 3:
                World.composter.AddApples((int)handItem.quantity);
                animator.ShowInteraction(PlayerAnimator.Interaction.INTERACTING);
                ticksToWait = ticksPerInteraction;
                taskProgress++;
                break;
            case 4:
                controller.MoveToTarget(World.appleTree, ReachedTarget);
                animator.ToggleAnimation(PlayerAnimator.PlayerState.WALKING);
                break;
            case 5:
                World.appleTree.GiveItem(handItem.item);
                CompleteTask();
                break;
        }
    }

    private void ExecuteFireSequence()
    {
        switch (taskProgress)
        {
            case 0:
                controller.MoveToTarget(World.fire, ReachedTarget);
                animator.ToggleAnimation(PlayerAnimator.PlayerState.WALKING);
                animator.ShowInteraction(PlayerAnimator.Interaction.FIRE);
                break;
            case 1:
                World.fire.TurnOn(currentTask.value);
                animator.ShowInteraction(PlayerAnimator.Interaction.INTERACTING);
                ticksToWait = ticksPerInteraction;
                taskProgress++;
                break;
            case 2:
                CompleteTask();
                break;
        }
    }

    private void ExecutePinwheelSequence()
    {
        switch (taskProgress)
        {
            case 0:
                controller.MoveToTarget(World.pinwheel, ReachedTarget);
                animator.ToggleAnimation(PlayerAnimator.PlayerState.WALKING);
                animator.ShowInteraction(PlayerAnimator.Interaction.PINWHEEL);
                break;
            case 1:
                World.pinwheel.StartSpin(currentTask.value);
                animator.ShowInteraction(PlayerAnimator.Interaction.INTERACTING);
                ticksToWait = ticksPerInteraction;
                taskProgress++;
                break;
            case 2:
                CompleteTask();
                break;
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
        taskProgress = 0;
        handItem.item = null;
        handItem.quantity = 0;
    }

    public bool HasTask()
    {
        return currentTask != null;
    }
}

public class HandItem
{
    public Transform item;
    public float quantity;

    public bool IsHeld { get => item != null; }

    public HandItem()
    {
        item = null;
        quantity = 0;
    }
}
