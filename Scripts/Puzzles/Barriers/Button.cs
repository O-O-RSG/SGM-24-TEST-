using UnityEngine;

public class Button : MonoBehaviour
{
    public Barrier barrier;
    public KeyCode keyToPress = KeyCode.Space;
    public float activationDistance = 3f;
    public Transform playerTransform;
    public Transform ghostTransform;

    void Update()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) <= activationDistance ||
            Vector3.Distance(transform.position, ghostTransform.position) <= activationDistance)
        {
            if (Input.GetKeyDown(keyToPress))
            {
                // Проверяем, включен ли барьер
                if (barrier.gameObject.activeSelf)
                {
                    // Если барьер включен, выключаем его
                    barrier.DisableBarrier();
                }
                else
                {
                    // Если барьер выключен, включаем его
                    barrier.EnableBarrier();
                }
            }
        }
    }
}
