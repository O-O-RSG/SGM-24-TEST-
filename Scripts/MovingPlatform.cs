using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Vector3 dPos;
    public float moveSpeed = 1f;
    // Update is called once per frame
    void Start()
    {
        iTween.MoveTo(this.gameObject, 
            iTween.Hash("position", dPos, "speed", moveSpeed, "easetype", iTween.EaseType.linear,"looptype", iTween.LoopType.pingPong));
    }
}
