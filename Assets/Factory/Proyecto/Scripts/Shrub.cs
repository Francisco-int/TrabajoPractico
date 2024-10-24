using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrub : Factory
{
    public GameObject bushPrefab;
    public Transform spawnPointShrub;
    public override GameObject CreateObject(Vector3 position)
    {
        return Instantiate(bushPrefab, position, Quaternion.identity);
    }
}
