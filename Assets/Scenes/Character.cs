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
    protected Quaternion _camOriginalRot;
    protected static double _dir;
    protected bool _mov;

    private void Start()
    {
        _obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        _obj.transform.localScale = new Vector3(2, 1, 1);
        _obj.name = "Character";
        _mat = _obj.GetComponent<Renderer>().material;
        _mat.color = UnityEngine.Color.yellow;
        _cam = Camera.allCameras.First();
        _cam.transform.position = new Vector3(8, 10, -8);
        _cam.transform.Rotate(new Vector3(45, -45, 0));
        Position = new Vector3(0, 0, 0);
        _mov = true;
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
    public bool MoveEnabled
    {
        set
        {
            _mov = value;
        }
        get
        {
            return _mov;
        }
    }
    public double Direction
    {
        set
        {
            _dir = value * -1;
            _obj.transform.localRotation = Quaternion.AngleAxis((float)value, Vector3.down);
        }
        get
        {
            return _dir;
        }
    }

    public void StartMove(Vector2 target)
    {
        if (MoveEnabled)
        {
            StopAllCoroutines();
            Direction = Math.Atan2(target.y - Position.z, target.x - Position.x) * (180 / Math.PI);
            //Direction = 0;
            StartCoroutine(Move(new Vector3(target.x, 0, target.y), 10f));
        }
    }

    protected IEnumerator Move(Vector3 target, float speed)
    {
        while (Position != target)
        {
            Position = Vector3.MoveTowards(Position, target, speed * Time.deltaTime);
            yield return null;
        }
    }
}
