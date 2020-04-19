using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MapGenerator))]
public class MapBuilder : MonoBehaviour, IManager
{
    [SerializeField] private MapLayer[] mapLayers = null;

    public void Initialize()
    {
        MapGenerator mapGenerator = GetComponent<MapGenerator>();
        Build(mapGenerator.GetMap());
    }

    private void Build(float[,] noiseMap)
    {
        for (int x = 0; x < noiseMap.GetLength(0); x++)
        {
            for (int z = 0; z < noiseMap.GetLength(1); z++)
            {
                for (int i = mapLayers.Length-1;i >= 0;i--)
                {
                    if (noiseMap[x, z] <= mapLayers[i].height)
                    {
                        Transform tile = Instantiate(mapLayers[i].prefab, transform);
                        tile.position = new Vector3(x, tile.GetComponent<BoxCollider>().size.y / 2f, z);
                        break;
                    }
                }                
            }
        }
    }

    [System.Serializable]
    public struct MapLayer
    {
        public string name;
        public Transform prefab;
        [Range(0, 1)] public float height;
    }
}
