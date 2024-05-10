using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClicked : MonoBehaviour
{
    public Action clicked;
    private void OnMouseDown()
    {
        clicked();
    }
}
