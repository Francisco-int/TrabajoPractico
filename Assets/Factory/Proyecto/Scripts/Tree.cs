using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Tree : Factory
{
    public GameObject treePrefab;

    public override GameObject CreateObject(Vector3 position)
    {
        return Instantiate(treePrefab, position, Quaternion.identity);
    }
}
