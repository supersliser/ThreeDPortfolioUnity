using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Podium : MonoBehaviour
{
    GameObject _flr;
    GameObject _pdm;
    Vector3 _pos;
    GameObject _etxt;

    public static GameObject getDefaultPodium(int item)
    {
        switch (item)
        {
            case 1:
                return Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Scenes/Podium.prefab"));
            default:
                return Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Scenes/Podium.prefab"));
        }
    }

    public static GameObject getDefaultPressEText(int item)
    {
        switch (item)
        {
            case 1:
                {
                    var temp = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Scenes/PressEText.prefab"));
                    temp.transform.GetChild(0).transform.GetChild(1).GetComponent<Renderer>().material.color = Color.grey;
                    return temp;
                }
            default:
                {
                    var temp = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Scenes/PressEText.prefab"));
                    temp.transform.GetChild(0).transform.GetChild(1).GetComponent<Renderer>().material.color = Color.grey;
                    return temp;
                }
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

    public bool checkCharacterInRange(Character c)
    {
        return Position.x - (Size.x / 2) < c.Position.x && c.Position.x < Position.x + (Size.x / 2) && Position.y - (Size.y / 2) < c.Position.y && c.Position.y < Position.y + (Size.y / 2);
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
    public GameObject EText
    {
        set { _etxt = value; }
        get { return _etxt; }
    }
    public Vector3 Position
    {
        set
        {
            _pos = value;
            _flr.transform.localPosition = new Vector3(value.x, value.y - _flr.transform.localScale.y / 2, value.z);
            _pdm.transform.localPosition = new Vector3(value.x, 0, value.z);
            _etxt.transform.localPosition = new Vector3(value.x < 0 ? value.x + 5 : value.x - 5, 0, value.z);
            _etxt.transform.Rotate(Vector3.up, value.x < 0 ? 90 : -90);
            if (value.x < 0)
            {
                _pdm.transform.Rotate(Vector3.up, 90);
            } else if (value.x > 0)
            {
                _pdm.transform.Rotate(Vector3.up, -90);
            }
        }
        get { return _pos; }
    }

    public Vector3 Size
    {
        get
        {
            return _flr.transform.localScale;
        }
    }
}
