using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public GameObject[] roomPrefabs;
    public Transform roomSpawnPoint;

    private GameObject currentRoom;
    private int floor = 0;

    public void LoadNextRoom()
    {
        if (currentRoom != null)
            Destroy(currentRoom);

        int index = Random.Range(0, roomPrefabs.Length);
        currentRoom = Instantiate(roomPrefabs[index], roomSpawnPoint.position, Quaternion.identity);
        floor++;
        Debug.Log("Floor " + floor);
    }

    void Start()
    {
        LoadNextRoom();
    }
}
