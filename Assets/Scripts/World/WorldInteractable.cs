using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInteractable : MonoBehaviour
{
    [SerializeField] private Transform heldItem = null;
    public int Layer { get; private set; }
    public bool HasItem { get; protected set; }

    private Vector3 heldItemOriginalPosition;

    protected virtual void Start()
    {
        Layer = Mathf.Clamp(Mathf.RoundToInt(transform.position.z), 0, Game.instance.world.GetLayerCount());
        HasItem = false;
    }

    public virtual T Interact<T>() { return default(T); }

    public Transform TakeItem()
    {
        if (HasItem)
        {
            heldItemOriginalPosition = heldItem.position;

            Transform t = heldItem;
            HasItem = false;
            heldItem = null;
            return t;
        }
        else
        {
            return null;
        }
    }

    public void GiveItem(Transform t)
    {
        HasItem = true;
        heldItem = t;
        heldItem.position = heldItemOriginalPosition;
    }
}