using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CamData : MonoBehaviour
{
    public bool CamMoving;
    public Camera me;

    public void moveCamToPodium(Podium p)
    {
        if (!CamMoving)
        {
            CamMoving = true;
            StartCoroutine(MoveCamToPodiumCoroutine(p.CameraPosition, p.CameraRotation));
        }
    }

    protected IEnumerator MoveCamToPodiumCoroutine(Vector3 targetPos, Quaternion targetAngle)
    {
        while (me.transform.position != targetPos || me.transform.rotation != targetAngle)
        {
            me.transform.position = Vector3.MoveTowards(me.transform.position, targetPos, ((me.transform.position.magnitude * targetPos.magnitude) / 30) * Time.deltaTime);
            me.transform.rotation = Quaternion.RotateTowards(me.transform.rotation, targetAngle, ((me.transform.position.magnitude * targetPos.magnitude) / 30) * 8 * Time.deltaTime);
            yield return null;
        }
        CamMoving = false;
    }

    public void StartResetCamera(Character c)
    {
        if (!CamMoving)
        {
            CamMoving = true;
            var temp = Quaternion.LookRotation(Vector3.forward, Vector3.up);
            temp.eulerAngles = new Vector3(45, -45, 0);
            StartCoroutine(ResetCamCoroutine(new Vector3(c.Position.x + 8, c.Position.y + 10, c.Position.z - 8), temp));
        }
    }

    protected IEnumerator ResetCamCoroutine(Vector3 targetPos, Quaternion targetAngle)
    {
        while (me.transform.position != targetPos || me.transform.rotation != targetAngle)
        {
            me.transform.position = Vector3.MoveTowards(me.transform.position, targetPos, ((me.transform.position.magnitude * targetPos.magnitude) / 30) * Time.deltaTime);
            me.transform.rotation = Quaternion.RotateTowards(me.transform.rotation, targetAngle, ((me.transform.position.magnitude * targetPos.magnitude) / 30) * 8 * Time.deltaTime);
            yield return null;
        }
        CamMoving = false;
    }
}
