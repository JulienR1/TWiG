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

    [SerializeField] private SpriteRenderer interactionSprite = null;
    [SerializeField] private InteractionImage[] interactions;
    [SerializeField] private int ticksToShowInteraction = 1;

    private Interaction currentInteraction;
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
        if (currentInteraction == interactionToRender)
            return;

        foreach (InteractionImage img in interactions)
        {
            if (img.interaction == interactionToRender)
            {
                interactionSprite.sprite = img.image;
                interactionSprite.gameObject.SetActive(true);
                ticksLeftForInteraction = ticksToShowInteraction;
                currentInteraction = interactionToRender;
                break;
            }
        }
    }

    public void HideInteraction()
    {
        interactionSprite.gameObject.SetActive(false);
    }

    private void OnTick()
    {
        if (ticksLeftForInteraction <= 0)
            HideInteraction();
        ticksLeftForInteraction--;
    }

    [System.Serializable]
    public struct InteractionImage
    {
        public Interaction interaction;
        public Sprite image;
    }

}