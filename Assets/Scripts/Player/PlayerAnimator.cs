using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    public enum PlayerState { IDLE, WALKING, WALKING_WATER, WALKING_APPLE, WALKING_COMPOST };
    public enum Interaction { INTERACTING, SUCCESS, FAIL, WATER, APPLE, COMPOST, FIRE, PINWHEEL, LIGHT };

    private Animator animator = null;
    private PlayerState currentState = PlayerState.IDLE;

    [SerializeField] private InteractionImage[] interactions;
    [SerializeField] private int ticksToShowInteraction = 1;

    private int ticksLeftForInteraction = 0;

    public void Initialize()
    {
        animator = GetComponent<Animator>();
        ticksLeftForInteraction = 0;

        TimeManager.OnTick += OnTick;
    }

    private void Animate()
    {
        animator.SetInteger("animationState", (int)currentState);
    }

    public void ToggleAnimation(PlayerState state)
    {
        currentState = state;
        Animate();
    }

    public void ClearAnimation()
    {
        currentState = PlayerState.IDLE;
        Animate();
    }

    public void ShowInteraction(Interaction interactionToRender)
    {
        foreach (InteractionImage img in interactions)
        {
            if (img.interaction == interactionToRender)
            {
                // TODO Set image to img.image;
                // TODO Activate image interaction renderer
                ticksLeftForInteraction = ticksToShowInteraction;
                break;
            }
        }
    }

    public void HideInteraction()
    {
        // TODO Disable image interaction renderer
    }

    private void OnTick()
    {
        if (ticksLeftForInteraction <= 0)
            HideInteraction();
    }

    [System.Serializable]
    public struct InteractionImage
    {
        public Interaction interaction;
        public Sprite image;
    }

}