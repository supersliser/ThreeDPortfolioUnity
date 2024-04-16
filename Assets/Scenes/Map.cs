using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Map : MonoBehaviour
{
    protected Floor _flr;
    protected Character _chr;
    protected List<Podium> _pdms;

    protected abstract void Start();

    public Character Player
    {
        get { return _chr; }
    }
    public Floor Floor { get { return _flr; } }
}


