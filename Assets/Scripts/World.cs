using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour, IManager
{
    public void Initialize()
    {
        
    }

    private float GetLayerScaleFactor(int layerNumber)
    {
        return Mathf.Exp(2.5f - layerNumber);
    }
}
