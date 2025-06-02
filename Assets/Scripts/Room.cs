using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject exitPortal;

    void Update()
    {
        if (!exitPortal.activeSelf && GameObject.FindGameObjectsWithTag("Monster").Length == 0)
        {
            exitPortal.SetActive(true);  // ���� ������ ��Ż ����
        }
    }
}
