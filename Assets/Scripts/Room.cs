using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject exitPortal;

    void Update()
    {
        if (!exitPortal.activeSelf && GameObject.FindGameObjectsWithTag("Monster").Length == 0)
        {
            exitPortal.SetActive(true);  // 몬스터 없으면 포탈 열기
        }
    }
}
