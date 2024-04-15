using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Map : MonoBehaviour
{
    protected Floor _flr;
    protected Character _chr;
    protected List<GameObject> _objs;

    protected abstract void Start();
}

public class Map1 : Map
{
    protected override void Start()
    {
        _flr = gameObject.AddComponent<Floor>();
    }
}
