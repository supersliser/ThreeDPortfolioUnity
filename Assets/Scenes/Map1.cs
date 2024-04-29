using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Map1 : Map
{
    protected List<GameObject> _prtcs;
    protected List<GameObject> _plls;
    protected override void Start()
    {
        base.Start();
        _flr = gameObject.AddComponent<Floor>();
        _flr.Object = Floor.defaultObject(1);
        _flr.Size = new Vector3(20, 10, 100);
        _flr.generateParticles();
        _chr = gameObject.AddComponent<Character>();
        _plls = new List<GameObject>();
        for (float x = (-_flr.Size.x / 2) + 1; x <= (_flr.Size.x / 2) - 1; x+=18)
        {
            for (float y = -_flr.Size.z / 2 + 1; y <= _flr.Size.z / 2 - 1; y+=15)
            {
                _plls.Add(Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Scenes/Pillar.prefab"), new Vector3(x, 0, y), Quaternion.identity));
                _plls.Last().transform.parent = _flr.transform;
            }
        }
        _prtcs = new List<GameObject>();
        for (int i = 0; i < 100; i++)
        {
            var temp = GameObject.CreatePrimitive(PrimitiveType.Cube);
            temp.GetComponent<Renderer>().material = AssetDatabase.LoadAssetAtPath<Material>("Assets/Scenes/ParticleMat.mat");
            temp.transform.position = new Vector3(Random.Range(-_flr.Size.x, _flr.Size.x), Random.Range(-_flr.Size.y, _flr.Size.y), Random.Range(-_flr.Size.z, _flr.Size.z));
            temp.transform.transform.localScale = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            temp.transform.parent = _flr.transform;
            _prtcs.Add(temp);
        }

        _pdms = new List<Podium>();
        {
            var temp = gameObject.AddComponent<Podium>();
            temp.Floor = Podium.getDefaultPodiumFloor(1, _flr);
            temp.PodiumObject = Podium.getDefaultPodium(1);
            temp.EText = Podium.getDefaultPressEText(1);
            temp.Title = Podium.getTitle(PodiumTypes.Websites, 1);
            temp.Position = new Vector3((_flr.Size.x / 2) + (temp.Size.x / 2), 0, 20);
            _pdms.Add(temp);
        }
        
    }
}
