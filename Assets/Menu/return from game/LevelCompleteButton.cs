using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteButton : MonoBehaviour
{
    public KeyCode keyToPress = KeyCode.E;
    public float activationDistance = 3f;
    public Transform playerTransform;
    public Transform ghostTransform;
    private bool isPressed = false; // Добавляем переменную, чтобы отслеживать, нажата ли кнопка

    [SerializeField] AudioSource buttonSound;

    void Update()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) <= activationDistance ||
            Vector3.Distance(transform.position, ghostTransform.position) <= activationDistance)
        {
            if (Input.GetKeyDown(keyToPress))
            {
                buttonSound.Play();
                isPressed = true; // Устанавливаем isPressed в true, когда кнопка нажата
                gameObject.SetActive(false); // Отключаем gameObject кнопки
            }
        }
    }

    public bool IsPressed() // Добавляем метод, чтобы другие скрипты могли проверить, нажата ли кнопка
    {
        return isPressed;
    }
}