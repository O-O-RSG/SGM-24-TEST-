using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Проверка на то,что обьект имеет компонент CharacterController
[RequireComponent(typeof(CharacterController))]
public class RelativeMovement : MonoBehaviour
{
    private Animator animator;
    //ссылка на объект, относительно которого происходит перемещение (Камера)
    [SerializeField] Transform target;
    public float moveSpeed = 6.0f;
    //Скорость прыжка 
    public float jumpSpeed = 15.0f;
    //Гравитация
    public float gravity = -9.8f;
    //Предельная скорость 
    public float terminalVelocity = -10.0f;
    //Минимальная скорость падения
    public float minFall = -1.5f;

    private float vertSpeed;

    private CharacterController charController;

    public float rotSpeed = 15.0f;
    //Нужно для хранения данных о столкновении между функциями
    private ControllerColliderHit contact;

    private void Start()
    {
        //Инициализируем переменную вертикальной скорости
        vertSpeed = minFall;
        //Для доступа к другим присоединенным компонентам
        charController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        //Начинаем с вектора (0, 0, 0), постепенно добавляя компоненты движения
        Vector3 movement = Vector3.zero;
        
        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        //Движение обрабатывается только при нажатии клавиш со стрелками
        if (horInput != 0 || vertInput != 0)
        {
            Vector3 right = target.right;
            //Раccчитываем направление игрока
            Vector3 forward = Vector3.Cross(right, Vector3.up);
            //Складываем входные данные в каждом направлении, чтобы получить комбинированный вектор движения
            movement = (right * horInput) + (forward * vertInput);
            movement *= moveSpeed;
            //Ограничивает модуль вектора moveSpeed 
            movement = Vector3.ClampMagnitude(movement, moveSpeed);

            //Создание нового кватерниона Методом LookRotation(), который вычисляет кватернион, смотрящий в этом направлении
            Quaternion direction = Quaternion.LookRotation(movement);
            //Quaternion.Lerp() Выполняет плавный переход из текущего положения в целевое и называется интерполяцией
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed*Time.deltaTime);
        }
        bool hitGround = false;
        RaycastHit hit;
        //Проверяем, падает ли персонаж
        
        if (vertSpeed < 0 && Physics.Raycast(transform.position,Vector3.down,out hit))
        {
            //Расстояние, с которым производится сравнение(слегка выходит занижнюю часть капсулы)
            float check = (charController.height + charController.radius) / 1.9f;
            hitGround = hit.distance <= check;
        }
        // Задаем анимации Speed,которая описана в контроллере
        animator.SetFloat("Speed",movement.sqrMagnitude);
        //Свойство isGrounded компонента CharacterController проверяет,соприкасается ли контроллер с поверхностью?
        //Изменено. Вместо проверки свойства isGrounded смотрим результат рейкастинга
        if (hitGround) 
        {
            if(Input.GetButtonDown("Jump")) 
            {
                vertSpeed = jumpSpeed;
            }
            else
            {
                vertSpeed = minFall;
                //Прерываем анимацию
                animator.SetBool("Jumping", false);
            }
        }
        else
        {
            vertSpeed += gravity * 5 * Time.deltaTime;
            //Если персонаж не стоит на поверхности, применяем гравитацию, пока не будет достигнута предельная скорость
            if (vertSpeed < terminalVelocity)
            {
                vertSpeed = terminalVelocity;
            }
            //Если не располагается на воздухе 
            if (contact != null)
            {
                //Проигрываем анимацию
                animator.SetBool("Jumping", true);
            }
            //Луч не обнаруживает поверхность, но капсула с ней соприкасается
            if (charController.isGrounded)
            {
                //Реакция меняется в зависимости от того, смотрит ли персонаж в точку контакта
                if (Vector3.Dot(movement, contact.normal) < 0)
                {
                    movement = contact.normal * moveSpeed;
                }
                else
                {
                    movement += contact.normal * moveSpeed;
                }
            }
        }
        //Присваиваем значение по вертикали 
        movement.y = vertSpeed;
        //Чтобы не зависить от частоты кадров умножаем на deltaTime
        movement *= Time.deltaTime;
        //Придаем движение персонажу
        charController.Move(movement);
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        contact = hit;
    }
}
