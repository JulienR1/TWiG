using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInteractable : MonoBehaviour
{
    public int Layer { get; private set; }
    public bool HasItem { get; protected set; }

    protected virtual void Start()
    {
        Layer = Mathf.Clamp(Mathf.RoundToInt(transform.position.z), 0, Game.instance.world.GetLayerCount());
        HasItem = false;
    }

    public virtual T Interact<T>() { return default(T); }

    public bool TakeItem()
    {
        if (HasItem)
        {
            HasItem = false;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void GiveItem()
    {
        HasItem = true;
    }
}