using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenDevice: MonoBehaviour
{
    [SerializeField] Vector3 dPos;
    private bool open = false;
    public void Activate()
    {
        if (!open)
        {
            dPos.y = -dPos.y;
            iTween.MoveTo(this.gameObject, iTween.Hash("position", dPos, "time", 3f, "easetype", iTween.EaseType.linear));
            open = true;
        }
    }
    public void Deactivate()
    {
        if (open)
        {
            dPos.y = -dPos.y;
            iTween.MoveTo(this.gameObject, iTween.Hash("position", dPos, "time", 3f, "easetype", iTween.EaseType.linear));
            open = false;
        }
    }
}
