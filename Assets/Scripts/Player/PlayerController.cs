using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float interactionMinDistance = 0.5f;

    private int currentLayer;
    private const int DIRECTION_TO_CLOSEST_LAYER = -1;
    private const int DIRECTION_TO_FURTHEST_LAYER = -1;

    public bool IsMoving { get; private set; }

    private Vector3 direction, velocity;
    private WorldInteractable currentTarget;
    private Func<bool> OnTargetReached;

    private void Start()
    {
        currentLayer = Mathf.Clamp(Mathf.RoundToInt(transform.position.z), 0, Game.instance.world.GetLayerCount());
        IsMoving = false;
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
                IsMoving = false;
                OnTargetReached();
            }
        }
    }    

    public void MoveToTarget(WorldInteractable target, Func<bool> callback)
    {        
        direction = (target.transform.position - transform.position).normalized;
        velocity = direction * moveSpeed / World.GetLayerScaleFactor(currentLayer);

        IsMoving = true;
        currentTarget = target;
        OnTargetReached = callback;
    }

    private void WalkToLayer(int direction)
    {

    }
    
    private void WalkAtTarget()
    {
        transform.position += velocity * Time.deltaTime;
    }

    private bool ReachedTarget()
    {
        if (currentTarget.Layer == currentLayer)
        {
            float deltaX = (transform.position.x - currentTarget.transform.position.x) / World.GetLayerScaleFactor(currentLayer);
            float deltaY = (transform.position.y - currentTarget.transform.position.y) / World.GetLayerScaleFactor(currentLayer);
            float squaredDistanceToTarget = Mathf.Pow(deltaX, 2) + Mathf.Pow(deltaY, 2);
            float minDistanceToTargetSquared = Mathf.Pow(interactionMinDistance, 2);
            if (squaredDistanceToTarget <= minDistanceToTargetSquared)
            {
                return true;
            }
        }            
        return false;
    }
}
