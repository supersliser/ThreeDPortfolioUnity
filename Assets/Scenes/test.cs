using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    Map map;
    // Start is called before the first frame update
    void Start()
    {
        map = gameObject.AddComponent<Map1>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) // Check for left mouse click
        {
            Ray ray = map.Player.Camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) // Cast a ray from camera to mouse position
            {
                // Access the point of contact
                Vector3 clickLocation = hit.point;

                // Do something with the click location (e.g., print it)
                map.Player.StartMove(new Vector2(clickLocation.x, clickLocation.z));
            }
        }
    }
}
