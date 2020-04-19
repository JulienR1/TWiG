using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInteractable : MonoBehaviour
{
    public int Layer { get; private set; }

    private void Start()
    {
        Layer = Mathf.Clamp(Mathf.RoundToInt(transform.position.z), 0, Game.instance.world.GetLayerCount());
    }

    public void Interact()
    {

    }
}