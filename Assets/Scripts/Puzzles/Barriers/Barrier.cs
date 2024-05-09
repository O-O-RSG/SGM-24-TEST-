using UnityEngine;

public class Barrier : MonoBehaviour
{
    public void DisableBarrier()
    {
        gameObject.SetActive(false);
    }

    // Добавляем новый метод для включения барьера
    public void EnableBarrier()
    {
        gameObject.SetActive(true);
    }
}