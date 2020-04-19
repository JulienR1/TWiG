using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController)),RequireComponent(typeof(PlayerAnimator))]
public class Player : MonoBehaviour, IManager
{
    private PlayerController controller;
    private PlayerAnimator animator;

    public WorldInteractable target;

    public void Initialize()
    {
        controller = GetComponent<PlayerController>();
        animator = GetComponent<PlayerAnimator>();
    }
}
