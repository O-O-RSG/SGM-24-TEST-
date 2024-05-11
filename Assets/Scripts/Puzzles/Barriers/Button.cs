using UnityEngine;

public class Button : MonoBehaviour
{
    public Barrier barrier;
    public KeyCode keyToPress = KeyCode.E;
    public float activationDistance = 1.5f;
    public Transform playerTransform;
    public Transform ghostTransform;


    [SerializeField] AudioSource buttonSound;

    void Update()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) <= activationDistance ||
            Vector3.Distance(transform.position, ghostTransform.position) <= activationDistance)
        {
            if (Input.GetKeyDown(keyToPress))
            {
                
                buttonSound.Play();

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
