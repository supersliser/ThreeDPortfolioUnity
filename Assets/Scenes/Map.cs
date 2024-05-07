using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Map : MonoBehaviour
{
    protected Floor _flr;
    protected Character _chr;
    protected List<Podium> _pdms;
    protected CamData _camD;

    protected virtual void Start()
    {
        _camD = Camera.main.GetComponent<CamData>();
    }

    public Character Player
    {
        get { return _chr; }
    }
    public Floor Floor { get { return _flr; } }

    public void Update()
    {
        foreach (var pdm in _pdms)
        {
            if (pdm.checkCharacterInRange(_chr))
            {
                if (!pdm.ETextRaised)
                {
                    pdm.ETextRaised = true;
                    pdm.startMoveEText(1);
                }


                if (Input.GetKeyDown(KeyCode.E) && pdm.Focussed == false)
                {
                    _chr.MoveEnabled = false;
                    pdm.Focussed = true;
                    _camD.moveCamToPodium(pdm);
                } 
                else if (Input.GetKeyDown(KeyCode.Escape) || (Input.GetKeyDown(KeyCode.E) && pdm.Focussed == true)) 
                {
                    _chr.MoveEnabled = true;
                    pdm.Focussed = false;
                    _camD.StartResetCamera(_chr);
                }
            } else
            {
                if (pdm.ETextRaised)
                {
                    pdm.ETextRaised = false;
                    pdm.startMoveEText(-1);
                }
            }
        }
    }
}


