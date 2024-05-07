using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHoverOutline : MonoBehaviour
{
    private bool hovered = false;

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        var objOutline = GetComponent<Outline>();


        if (Physics.Raycast(ray, out hit)) // Cast a ray from camera to mouse position
        {
            if (!hovered && hit.collider.gameObject == gameObject)
            {
                hovered = true;
                StartCoroutine(GrowOutline(objOutline));
            }
            if (hovered && hit.collider.gameObject != gameObject)
            {
                hovered = false;
                StopCoroutine(GrowOutline(objOutline));
                StartCoroutine(ShrinkOutline(objOutline));
            }
        } 
        else if (hovered)
        {
            hovered = false;
            StopCoroutine(GrowOutline(objOutline));
            StartCoroutine(ShrinkOutline(objOutline));
        }
    }

    private IEnumerator GrowOutline(Outline outline)
    {
        for (float i = 0; i < 10; i += 1)
        {
            outline.OutlineWidth = i;
            yield return null;
        }
    }

    private IEnumerator ShrinkOutline(Outline outline)
    {
        for (float i = 10; i > 0; i -= 1)
        {
            outline.OutlineWidth = i;
            yield return null;
        }
    }
}
