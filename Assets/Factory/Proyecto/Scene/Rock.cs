using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Factory
{
    public GameObject rockPrefab;
    public override GameObject CreateObject(Vector3 position)
    {
        return Instantiate(rockPrefab, position, Quaternion.identity);
    }
}
