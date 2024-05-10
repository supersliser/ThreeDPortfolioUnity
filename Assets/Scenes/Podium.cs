using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public enum PodiumTypes
{
    Websites,
    ThreeDModels,
    DesktopApps,
    MobileApps,
}
public class Podium : MonoBehaviour
{
    GameObject _flr;
    GameObject _pdm;
    Vector3 _pos;
    GameObject _etxt;
    Vector3 _camPos;
    Quaternion _camRot;
    bool _foc;
    GameObject _tit;
    bool _etxtrsd;
    GameObject _nextBtn;
    GameObject _prevBtn;
    List<MonoBehaviour> _prtfItms;

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

    public static GameObject getTitle(PodiumTypes title, int item)
    {
        GameObject output;
        switch (title)
        {
            case PodiumTypes.Websites:
                {
                    output = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Scenes/WebsitesText.prefab"));
                    break;
                }
            default:
                {
                    output = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Scenes/WebsitesText.prefab"));
                    break;
                }
        }
        switch (item)
        {
            case 1:
                output.transform.GetChild(0).transform.GetChild(1).GetComponent<Renderer>().material.color = Color.blue;
                break;
            default:
                output.transform.GetChild(0).transform.GetChild(1).GetComponent<Renderer>().material.color = Color.blue;
                break;
        }
        return output;
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

    public void startMoveEText(int multiplier)
    {
        if (multiplier > 0)
        {
            StopCoroutine(MoveETextDown());
            StartCoroutine(MoveETextUp());
        }
        else
        {
            StopCoroutine(MoveETextUp());
            StartCoroutine(MoveETextDown());
        }
    }

    public void LoadPortfolioItems(PodiumTypes type)
    {
        _prtfItms = new List<MonoBehaviour>();
        switch (type)
        {
            case PodiumTypes.Websites:
                {
                    int count = WebsitePortfolioItem.WebsiteCount;
                    for (int i = 0; i < count; i++)
                    {
                        var temp = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Scenes/WebsitesData/WebsitePortfolioItem.prefab"));
                        temp.transform.position = new Vector3(Position.x < 0 ? Position.x - 15 : Position.x + 15, 8, Position.z);
                        temp.transform.Rotate(Vector3.up, 90);
                        _prtfItms.Add(temp.GetComponent<WebsitePortfolioItem>());
                        ((WebsitePortfolioItem)_prtfItms[i]).Generate(i);
                    }
                    break;
                }
            default: break;
        }
    }

    protected IEnumerator MoveETextUp()
    {
        for (float i = -1; i < 0.5f; i += 0.1f)
        {
            _etxt.transform.position = new Vector3(_etxt.transform.position.x, i, _etxt.transform.position.z);
            yield return null;
        }
    }

    protected IEnumerator MoveETextDown()
    {
        for (float i = 0.5f; i > -1; i -= 0.1f)
        {
            _etxt.transform.position = new Vector3(_etxt.transform.position.x, i, _etxt.transform.position.z);
            yield return null;
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
    public GameObject EText
    {
        set { _etxt = value; }
        get { return _etxt; }
    }
    public bool ETextRaised
    {
        set
        {
            _etxtrsd = value;
        }
        get
        {
            return _etxtrsd;
        }
    }
    public Vector3 Position
    {
        set
        {
            _pos = value;
            _flr.transform.localPosition = new Vector3(value.x, value.y - _flr.transform.localScale.y / 2, value.z);
            _pdm.transform.localPosition = new Vector3(value.x, 0, value.z);
            _etxt.transform.localPosition = new Vector3(value.x < 0 ? value.x + 5 : value.x - 5, -1, value.z);
            _tit.transform.localPosition = new Vector3(value.x < 0 ? value.x + 3 : value.x - 3, 0, value.z);
            _camPos = new Vector3(value.x < 0 ? value.x + 2 : value.x - 2, 5, value.z);
            _camRot = Quaternion.LookRotation(value.x < 0 ? Vector3.left : Vector3.right, Vector3.up);
            _etxt.transform.Rotate(Vector3.up, value.x < 0 ? 90 : -90);
            _tit.transform.Rotate(Vector3.up, value.x < 0 ? 90 : -90);
            if (value.x < 0)
            {
                _pdm.transform.Rotate(Vector3.up, 90);
            }
            else if (value.x > 0)
            {
                _pdm.transform.Rotate(Vector3.up, -90);
            }
        }
        get { return _pos; }
    }
    public Vector3 CameraPosition
    {
        get { return _camPos; }
    }
    public Quaternion CameraRotation
    {
        get { return _camRot; }
    }
    public bool Focussed
    {
        set
        {
            _foc = value;
            _nextBtn.GetComponent<ButtonHoverOutline>().enabled = value;
            _prevBtn.GetComponent<ButtonHoverOutline>().enabled = value;
        }
        get { return _foc; }
    }
    public Vector3 Size
    {
        get
        {
            return _flr.transform.localScale;
        }
    }
    public GameObject Title
    {
        set { _tit = value; }
        get { return _tit; }
    }
    public GameObject NextButton
    {
        set
        {
            _nextBtn = value;
        }
        get
        {
            return _nextBtn;
        }
    }
    public GameObject PrevButton
    {
        set
        {
            _prevBtn = value;
        }
        get
        {
            return _prevBtn;
        }
    }
}
