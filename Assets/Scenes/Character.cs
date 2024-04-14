using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Character : MonoBehaviour
{
    protected static Vector3 _pos;
    protected static GameObject _obj;
    protected static Material _mat;
    protected static Camera _cam;
    protected static double _dir;

    private void Start()
    {
        _obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        _obj.transform.localScale = new Vector3(2, 1, 1);
        _mat = _obj.GetComponent<Renderer>().material;
        _mat.color = UnityEngine.Color.yellow;
        _cam = Camera.allCameras.First();
        _cam.transform.position = new Vector3(8, 10, -8);
        _cam.transform.Rotate(new Vector3(45, -45, 0));
        Position = new Vector3(0, 0, 0);
    }

    public Vector3 Position
    {
        set { 
            _pos = value;
            _obj.transform.position = new Vector3(value.x, value.y + _obj.transform.localScale.y / 2, value.z);
            _cam.transform.position = new Vector3(value.x + 8, value.y + 10, value.z - 8);
        }
        get { return _pos; }
    }
    public GameObject Object
    {
        set
        {
            _obj = value;
        }
        get { return _obj; }
    }
    public Camera Camera { get { return _cam; } }
    public double Direction
    {
        set
        {
            _dir = value * -1;
            _obj.transform.rotation = Quaternion.AngleAxis((float)value, Vector3.down);
        }
        get
        {
            return _dir;
        }
    }

    public void StartMove(Vector2 target)
    {
        StopAllCoroutines();
        Direction = Math.Atan2(target.y - Position.z, target.x - Position.x) * (180 / Math.PI);
        //Direction = 0;
        StartCoroutine(Move(target, 0.005f, new Vector2(Position.x, Position.z)));
    }

    protected IEnumerator Move(Vector2 target, float speed, Vector2 start)
    {
        //double dist = Math.Sqrt(Math.Pow(target.x - Position.x, 2) + Math.Pow(target.y - Position.z, 2));
        Vector2 dist = new Vector2(target.x - start.x, target.y - start.y);
        for (Vector2 i = new Vector2(0, 0); i != new Vector2(target.x - Position.x, target.y - Position.y); i.x += dist.normalized.x * speed, i.y += dist.normalized.y * speed)
        {
            Position = new Vector3((float)(start.x + i.x), 0f, (float)(start.y + i.y));
            yield return null;
        }
    }
}
