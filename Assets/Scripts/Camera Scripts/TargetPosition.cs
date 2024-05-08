using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPosition : MonoBehaviour
{
    private Transform target;
    [SerializeField] private float CameraSensY;
    [SerializeField] private float CameraSensX;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * CameraSensX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * CameraSensY * Time.deltaTime;
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 0.32f, target.transform.position.z);
        //transform.Rotate(mouseY, 0f, 0f);
        transform.Rotate(mouseY, mouseX, 0f, Space.World);
    }
}
