using UnityEngine;

public class Barrier : MonoBehaviour
{
    public void DisableBarrier()
    {
        gameObject.SetActive(false);
    }

    // ��������� ����� ����� ��� ��������� �������
    public void EnableBarrier()
    {
        gameObject.SetActive(true);
    }
}