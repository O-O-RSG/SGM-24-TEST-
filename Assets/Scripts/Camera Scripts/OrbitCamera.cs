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
    //Значение для сохранения смещения между камерой и целью(объект)
    private Vector3 offset;
    /// <summary>
    /// Сохраняем начальное смещение между камерой и целью
    /// </summary>
    void Start()
    {
        rotY = transform.eulerAngles.y;
        offset = target.position - transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float horInput = Input.GetAxis("Horizontal");
        if (!Mathf.Approximately(horInput, 0))
        {
            //Медленный поворот камеры при помощи клавиш со стрелками...
            rotY += horInput * rotSpeed;
        }
        else
        {
            //... или быстрый поворот с помощью мыши
            rotY += Input.GetAxis("Mouse X") * rotSpeed * 3;
        }

        // Здесь создаётся кватернион, который представляет собой поворот вокруг оси Y на угол
        Quaternion rotation = Quaternion.Euler(0,rotY,0);
        // Устанавливает позицию текущего объекта так, чтобы он находился на определённом
        // расстоянии и угле относительно цели, используя поворот и смещение.
        transform.position = target.position - (rotation * offset);
        //Где не находилась камера, она всегда смотрит на цель
        transform.LookAt(target);
    }
}
