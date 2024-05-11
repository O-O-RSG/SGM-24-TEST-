using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public LevelCompleteButton button1; // Ссылки на две кнопки
    public LevelCompleteButton button2;

    void Update()
    {
        // Проверяем, нажаты ли обе кнопки
        if (button1.IsPressed() && button2.IsPressed())
        {
            // Если обе кнопки нажаты, перезагружаем уровень
            SceneManager.LoadScene("PlayMenu");
        }
    }
}
