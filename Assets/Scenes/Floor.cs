using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Floor : MonoBehaviour
{
    protected Vector3 _size;
    protected GameObject _item;
    protected Material _mat;
    protected List<GameObject> _prtcs;

    public static GameObject defaultObject(int item)
    {
        switch (item)
        {
            case 1:
                return GameObject.CreatePrimitive(PrimitiveType.Cube);
            default:
                return GameObject.CreatePrimitive(PrimitiveType.Cube);
        }
    }

    public void generateParticles()
    {
        if (_prtcs != null)
        {
            foreach (var prtcs in _prtcs)
            {
                Destroy(prtcs);
            }
        }
        _prtcs = new List<GameObject>();
        for (float i = -Size.z / 2; i < Size.z / 2; i++)
        {
            var temp = GameObject.CreatePrimitive(PrimitiveType.Cube);
            temp.GetComponent<Renderer>().material = AssetDatabase.LoadAssetAtPath<Material>("Assets/Scenes/ParticleMat.mat");
            float height = Random.Range(0, 100);
            temp.transform.position = new Vector3(Random.Range(-Size.x / 2, Size.x / 2), (-Size.y / 2) - (height / 2), i);
            temp.transform.localScale = new Vector3(Random.Range(0.5f, 2f), height, Random.Range(0.5f, 2f));
            temp.transform.parent = _item.transform;
            _prtcs.Add(temp);
        }
    }
    protected void Start()
    {
        _mat = _item.GetComponent<Material>();
    }
    public GameObject Object
    {
        set
        {
            _item = value;
        }
        get
        {
            return _item;
        }
    }
    public Vector3 Size
    {
        set
        {
            _size = value;
            _item.transform.localScale = value;
            _item.transform.position = new Vector3(0, -(value.y / 2), 0);
        }
        get
        {
            return _size;
        }
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

