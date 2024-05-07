using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    //��������������� ������ �� ������, ������ �������� ������������ ��������(��� ���������� � �������� player)
    [SerializeField] Transform target;
    //�������� ��������
    public float rotSpeed = 1.5f;
    //������� �� ��� �
    private float rotY;
    private float rotX;
    //�������� ��� ���������� �������� ����� ������� � �����(������)
    private Vector3 offset;
    /// <summary>
    /// ��������� ��������� �������� ����� ������� � �����
    /// </summary>
    void Start()
    {
        rotY = transform.eulerAngles.y;
        offset = target.position - transform.position;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        rotY += Input.GetAxis("Mouse X") * rotSpeed * 3 * Time.deltaTime;
        if (Input.GetButtonDown("Fire3"))
        {
            rotX =0 ;
        }
        rotX += Input.GetAxis("Mouse Y") * rotSpeed * 3 * Time.deltaTime;


        // ����� �������� ����������, ������� ������������ ����� ������� ������ ��� Y �� ����
        Quaternion rotation = Quaternion.Euler(rotX,rotY,0);
        // ������������� ������� �������� ������� ���, ����� �� ��������� �� �����������
        // ���������� � ���� ������������ ����, ��������� ������� � ��������.
        transform.position = target.position - (rotation * offset);
        //��� �� ���������� ������, ��� ������ ������� �� ����
        transform.LookAt(target);
    }
}
