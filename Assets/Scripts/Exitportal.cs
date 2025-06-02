using UnityEngine;

public class ExitPortal : MonoBehaviour
{
    private DungeonGenerator dungeon;

    void Start()
    {
        dungeon = Object.FindFirstObjectByType<DungeonGenerator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && dungeon != null)
        {
            dungeon.LoadNextRoom();
        }
    }
}
