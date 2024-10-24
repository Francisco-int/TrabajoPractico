using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactorySpawner : MonoBehaviour
{
    public Factory shrubFactory;
    public Factory rockFactory;
    public Factory treeFactory;

    public Transform spawnPointShrub;
    public Transform spawnPointRock;
    public Transform spawnPointTree;

    public void SpawnShrub()
    {
        shrubFactory.CreateObject(spawnPointShrub.position);
    }

    public void SpawnRock()
    {
        rockFactory.CreateObject(spawnPointRock.position);
    }

    public void SpawnTree()
    {
        treeFactory.CreateObject(spawnPointTree.position);
    }
}