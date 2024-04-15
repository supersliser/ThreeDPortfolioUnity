using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Podium : MonoBehaviour
{
    GameObject _flr;
    GameObject _pdm;
    Vector3 _pos;

    public static GameObject getDefaultPodium(int item)
    {
        switch (item)
        {
            case 1:
                return AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Scenes/Podium.prefab");
            default:
                return AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Scenes/Podium.prefab");
        }
    }

    public static GameObject getDefaultPodiumFloor(int item, Floor floor)
    {
        switch (item)
        {
            case 1:
                {
                    var temp = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    temp.transform.localScale = new Vector3(floor.Size.y, floor.Size.y, floor.Size.y);
                    return temp;
                }
            default:
                {
                    var temp = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    temp.transform.localScale = new Vector3(floor.Size.y, floor.Size.y, floor.Size.y);
                    return temp;
                }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject Floor
    {
        set { _flr = value; }
        get { return _flr; }
    }
    public GameObject PodiumObject
    {
        set { _pdm = value; }
        get { return _pdm; }
    }
    public Vector3 Position
    {
        set
        {
            _pos = value;
            _flr.transform.localPosition = new Vector3(value.x, value.y - _flr.transform.localScale.y / 2, value.z);
            _pdm.transform.localPosition = new Vector3(value.x, 0, value.z);
        }
        get { return _pos; }
    }
}
