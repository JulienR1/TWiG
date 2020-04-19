using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform playerSprite;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float interactionMinDistance = 0.5f;

    private int currentLayer;
    private const int DIRECTION_TO_CLOSEST_LAYER = 1;
    private const int DIRECTION_TO_FURTHEST_LAYER = -1;

    public bool IsMoving { get; private set; }

    private Vector3 direction, velocity;
    private WorldInteractable currentTarget;
    private Func<bool> OnTargetReached;

    private void Start()
    {
        currentLayer = Mathf.Clamp(Mathf.RoundToInt(transform.position.z), 0, Game.instance.world.GetLayerCount());        
        IsMoving = false;

        UpdateScaleAndPosition();
    }

    private void Update()
    {
        if (IsMoving)
        {
            if (currentLayer == currentTarget.Layer)
                WalkAtTarget();
            else
                WalkToLayer(currentLayer > currentTarget.Layer ? DIRECTION_TO_CLOSEST_LAYER : DIRECTION_TO_FURTHEST_LAYER);

            if (ReachedTarget())
            {
                velocity = Vector3.zero;
                IsMoving = false;
                OnTargetReached();
            }
        }
    }    

    public void MoveToTarget(WorldInteractable target, Func<bool> callback)
    {
        velocity = Vector3.zero;
        IsMoving = true;
        currentTarget = target;
        OnTargetReached = callback;
    }

    private void WalkAtTarget()
    {
        if (velocity == Vector3.zero)
            CalculateVelocity(currentTarget.transform.position);
        transform.position += velocity * Time.deltaTime;
    }

    private void WalkToLayer(int direction)
    {
        Vector3 limitPos = new Vector3(direction * Game.instance.world.GetWorldLimit() * GetLayerSign(), transform.position.y, transform.position.z);
        if (velocity == Vector3.zero)
            CalculateVelocity(limitPos);

        transform.position += velocity * Time.deltaTime;

        if (Math.Abs(transform.position.x - limitPos.x) <= interactionMinDistance)
            ChangeLayer(direction);
    }

    private void CalculateVelocity(Vector3 targetPos)
    {
        direction = Vector3.right * Mathf.Sign((targetPos - transform.position).x);
        velocity = direction * moveSpeed * World.GetLayerScaleFactor(currentLayer);
    }

    private void ChangeLayer(int direction)
    {
        currentLayer -= direction;
        transform.position -= Vector3.forward * direction;                
        velocity = Vector3.zero;

        UpdateScaleAndPosition();
    }

    private bool ReachedTarget()
    {
        if (currentTarget.Layer == currentLayer)
            if (Mathf.Abs(transform.position.x - currentTarget.transform.position.x) <= interactionMinDistance)
                return true;
        return false;
    }

    private void UpdateScaleAndPosition()
    {
        transform.position = new Vector3(transform.position.x, Game.instance.world.GetLayerFloor(currentLayer), transform.position.z);
        playerSprite.localScale = Vector3.one * World.GetLayerScaleFactor(currentLayer);
    }

    private int GetLayerSign()
    {
        return currentLayer % 2 == 0 ? 1 : -1;
    }
}
