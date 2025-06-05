using UnityEngine;

public class Exitportal : MonoBehaviour
{
    private DungeonManager dungeonManager;

    void Start()
    {
        dungeonManager = FindFirstObjectByType<DungeonManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dungeonManager.LoadNextRoom();
            Destroy(gameObject); // 포탈은 일회용
        }
    }
}
