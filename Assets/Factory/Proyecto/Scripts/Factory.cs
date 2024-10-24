using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Factory : MonoBehaviour, IFactory
{
    public abstract GameObject CreateObject(Vector3 position);

}