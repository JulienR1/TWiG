using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    public enum PlayerState { IDLE, WALKING };

    private Animator animator = null;
    private PlayerState currentState = PlayerState.IDLE;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Animate()
    {
        animator.SetInteger("animationState", (int)currentState);
    }

    private void ToggleAnimation(PlayerState state)
    {
        currentState = state;
        Animate();
    }

    private void ClearAnimation()
    {
        currentState = PlayerState.IDLE;
        Animate();
    }

}
