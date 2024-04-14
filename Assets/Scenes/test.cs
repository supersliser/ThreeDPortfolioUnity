using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    Floor ground;
    Character player;
    // Start is called before the first frame update
    void Start()
    {
        ground = new Floor(new Vector3(100, 10, 10), Color.white);
        player = gameObject.AddComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) // Check for left mouse click
        {
            Ray ray = player.Camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) // Cast a ray from camera to mouse position
            {
                // Access the point of contact
                Vector3 clickLocation = hit.point;

                // Do something with the click location (e.g., print it)
                player.StartMove(new Vector2(clickLocation.x, clickLocation.z));
            }
        }
    }
}
