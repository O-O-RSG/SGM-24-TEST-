using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    //Сериализованная ссылка на объект, вокруг которого производится вращение(для связывание с объетком player)
    [SerializeField] Transform target;
    //Скорость поворота
    public float rotSpeed = 1.5f;
    //Поворот по оси у
    private float rotY;
    private float rotX;
    //Значение для сохранения смещения между камерой и целью(объект)
    private Vector3 offset;
    /// <summary>
    /// Сохраняем начальное смещение между камерой и целью
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


        // Здесь создаётся кватернион, который представляет собой поворот вокруг оси Y на угол
        Quaternion rotation = Quaternion.Euler(rotX,rotY,0);
        // Устанавливает позицию текущего объекта так, чтобы он находился на определённом
        // расстоянии и угле относительно цели, используя поворот и смещение.
        transform.position = target.position - (rotation * offset);
        //Где не находилась камера, она всегда смотрит на цель
        transform.LookAt(target);
    }
}
