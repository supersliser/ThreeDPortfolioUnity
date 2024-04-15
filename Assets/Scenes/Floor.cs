using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Floor : MonoBehaviour
{
    protected Vector3 _size;
    protected GameObject _item;
    protected Material _mat;

    protected void Start()
    {
        _item = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Size = Vector3.zero;
        _mat = _item.GetComponent<Renderer>().material;
    }
    public GameObject Object { get { return _item; } }
    public Vector3 Size
    {
        set
        {
            _size = value;
            _item.transform.localScale = value;
            _item.transform.position = new Vector3(0, -(value.y / 2), 0);
        }
        get { return _size; }
    }
    public Color Colour
    {
        set
        {
            _mat.color = value;
        }
        get
        {
            return _mat.color;
        }
    }
}
